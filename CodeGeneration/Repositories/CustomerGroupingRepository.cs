
using Common;
using ERP.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    public interface ICustomerGroupingRepository
    {
        Task<int> Count(CustomerGroupingFilter CustomerGroupingFilter);
        Task<List<CustomerGrouping>> List(CustomerGroupingFilter CustomerGroupingFilter);
        Task<CustomerGrouping> Get(Guid Id);
        Task<bool> Create(CustomerGrouping CustomerGrouping);
        Task<bool> Update(CustomerGrouping CustomerGrouping);
        Task<bool> Delete(Guid Id);
        
    }
    public class CustomerGroupingRepository : ICustomerGroupingRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CustomerGroupingRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerGroupingDAO> DynamicFilter(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<CustomerGroupingDAO> DynamicOrder(IQueryable<CustomerGroupingDAO> query,  CustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerGrouping>> DynamicSelect(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            List <CustomerGrouping> CustomerGroupings = await query.Select(q => new CustomerGrouping()
            {
                
                Id = filter.Selects.Contains(CustomerGroupingSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(CustomerGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerGroupingSelect.Name) ? q.Name : default(string),
                LegalEntityId = filter.Selects.Contains(CustomerGroupingSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CustomerGroupingSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return CustomerGroupings;
        }

        public async Task<int> Count(CustomerGroupingFilter filter)
        {
            IQueryable <CustomerGroupingDAO> CustomerGroupingDAOs = ERPContext.CustomerGrouping;
            CustomerGroupingDAOs = DynamicFilter(CustomerGroupingDAOs, filter);
            return await CustomerGroupingDAOs.CountAsync();
        }

        public async Task<List<CustomerGrouping>> List(CustomerGroupingFilter filter)
        {
            if (filter == null) return new List<CustomerGrouping>();
            IQueryable<CustomerGroupingDAO> CustomerGroupingDAOs = ERPContext.CustomerGrouping;
            CustomerGroupingDAOs = DynamicFilter(CustomerGroupingDAOs, filter);
            CustomerGroupingDAOs = DynamicOrder(CustomerGroupingDAOs, filter);
            var CustomerGroupings = await DynamicSelect(CustomerGroupingDAOs, filter);
            return CustomerGroupings;
        }

        public async Task<CustomerGrouping> Get(Guid Id)
        {
            CustomerGrouping CustomerGrouping = await ERPContext.CustomerGrouping.Where(l => l.Id == Id).Select(CustomerGroupingDAO => new CustomerGrouping()
            {
                 
                Id = CustomerGroupingDAO.Id,
                Code = CustomerGroupingDAO.Code,
                Name = CustomerGroupingDAO.Name,
                LegalEntityId = CustomerGroupingDAO.LegalEntityId,
                BusinessGroupId = CustomerGroupingDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return CustomerGrouping;
        }

        public async Task<bool> Create(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = new CustomerGroupingDAO();
            
            CustomerGroupingDAO.Id = CustomerGrouping.Id;
            CustomerGroupingDAO.Code = CustomerGrouping.Code;
            CustomerGroupingDAO.Name = CustomerGrouping.Name;
            CustomerGroupingDAO.LegalEntityId = CustomerGrouping.LegalEntityId;
            CustomerGroupingDAO.BusinessGroupId = CustomerGrouping.BusinessGroupId;
            CustomerGroupingDAO.Disabled = false;
            
            await ERPContext.CustomerGrouping.AddAsync(CustomerGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = ERPContext.CustomerGrouping.Where(b => b.Id == CustomerGrouping.Id).FirstOrDefault();
            
            CustomerGroupingDAO.Id = CustomerGrouping.Id;
            CustomerGroupingDAO.Code = CustomerGrouping.Code;
            CustomerGroupingDAO.Name = CustomerGrouping.Name;
            CustomerGroupingDAO.LegalEntityId = CustomerGrouping.LegalEntityId;
            CustomerGroupingDAO.BusinessGroupId = CustomerGrouping.BusinessGroupId;
            CustomerGroupingDAO.Disabled = false;
            ERPContext.CustomerGrouping.Update(CustomerGroupingDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CustomerGroupingDAO CustomerGroupingDAO = await ERPContext.CustomerGrouping.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CustomerGroupingDAO.Disabled = true;
            ERPContext.CustomerGrouping.Update(CustomerGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
