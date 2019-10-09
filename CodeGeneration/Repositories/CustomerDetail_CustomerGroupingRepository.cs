
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
    public interface ICustomerDetail_CustomerGroupingRepository
    {
        Task<int> Count(CustomerDetail_CustomerGroupingFilter CustomerDetail_CustomerGroupingFilter);
        Task<List<CustomerDetail_CustomerGrouping>> List(CustomerDetail_CustomerGroupingFilter CustomerDetail_CustomerGroupingFilter);
        Task<CustomerDetail_CustomerGrouping> Get(Guid Id);
        Task<bool> Create(CustomerDetail_CustomerGrouping CustomerDetail_CustomerGrouping);
        Task<bool> Update(CustomerDetail_CustomerGrouping CustomerDetail_CustomerGrouping);
        Task<bool> Delete(Guid Id);
        
    }
    public class CustomerDetail_CustomerGroupingRepository : ICustomerDetail_CustomerGroupingRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CustomerDetail_CustomerGroupingRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerDetail_CustomerGroupingDAO> DynamicFilter(IQueryable<CustomerDetail_CustomerGroupingDAO> query, CustomerDetail_CustomerGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerDetailId != null)
                query = query.Where(q => q.CustomerDetailId, filter.CustomerDetailId);
            if (filter.CustomerGroupingId != null)
                query = query.Where(q => q.CustomerGroupingId, filter.CustomerGroupingId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<CustomerDetail_CustomerGroupingDAO> DynamicOrder(IQueryable<CustomerDetail_CustomerGroupingDAO> query,  CustomerDetail_CustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
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

        private async Task<List<CustomerDetail_CustomerGrouping>> DynamicSelect(IQueryable<CustomerDetail_CustomerGroupingDAO> query, CustomerDetail_CustomerGroupingFilter filter)
        {
            List <CustomerDetail_CustomerGrouping> CustomerDetail_CustomerGroupings = await query.Select(q => new CustomerDetail_CustomerGrouping()
            {
                
                Id = filter.Selects.Contains(CustomerDetail_CustomerGroupingSelect.Id) ? q.Id : default(Guid),
                CustomerDetailId = filter.Selects.Contains(CustomerDetail_CustomerGroupingSelect.CustomerDetail) ? q.CustomerDetailId : default(Guid),
                CustomerGroupingId = filter.Selects.Contains(CustomerDetail_CustomerGroupingSelect.CustomerGrouping) ? q.CustomerGroupingId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CustomerDetail_CustomerGroupingSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return CustomerDetail_CustomerGroupings;
        }

        public async Task<int> Count(CustomerDetail_CustomerGroupingFilter filter)
        {
            IQueryable <CustomerDetail_CustomerGroupingDAO> CustomerDetail_CustomerGroupingDAOs = ERPContext.CustomerDetail_CustomerGrouping;
            CustomerDetail_CustomerGroupingDAOs = DynamicFilter(CustomerDetail_CustomerGroupingDAOs, filter);
            return await CustomerDetail_CustomerGroupingDAOs.CountAsync();
        }

        public async Task<List<CustomerDetail_CustomerGrouping>> List(CustomerDetail_CustomerGroupingFilter filter)
        {
            if (filter == null) return new List<CustomerDetail_CustomerGrouping>();
            IQueryable<CustomerDetail_CustomerGroupingDAO> CustomerDetail_CustomerGroupingDAOs = ERPContext.CustomerDetail_CustomerGrouping;
            CustomerDetail_CustomerGroupingDAOs = DynamicFilter(CustomerDetail_CustomerGroupingDAOs, filter);
            CustomerDetail_CustomerGroupingDAOs = DynamicOrder(CustomerDetail_CustomerGroupingDAOs, filter);
            var CustomerDetail_CustomerGroupings = await DynamicSelect(CustomerDetail_CustomerGroupingDAOs, filter);
            return CustomerDetail_CustomerGroupings;
        }

        public async Task<CustomerDetail_CustomerGrouping> Get(Guid Id)
        {
            CustomerDetail_CustomerGrouping CustomerDetail_CustomerGrouping = await ERPContext.CustomerDetail_CustomerGrouping.Where(l => l.Id == Id).Select(CustomerDetail_CustomerGroupingDAO => new CustomerDetail_CustomerGrouping()
            {
                 
                Id = CustomerDetail_CustomerGroupingDAO.Id,
                CustomerDetailId = CustomerDetail_CustomerGroupingDAO.CustomerDetailId,
                CustomerGroupingId = CustomerDetail_CustomerGroupingDAO.CustomerGroupingId,
                BusinessGroupId = CustomerDetail_CustomerGroupingDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return CustomerDetail_CustomerGrouping;
        }

        public async Task<bool> Create(CustomerDetail_CustomerGrouping CustomerDetail_CustomerGrouping)
        {
            CustomerDetail_CustomerGroupingDAO CustomerDetail_CustomerGroupingDAO = new CustomerDetail_CustomerGroupingDAO();
            
            CustomerDetail_CustomerGroupingDAO.Id = CustomerDetail_CustomerGrouping.Id;
            CustomerDetail_CustomerGroupingDAO.CustomerDetailId = CustomerDetail_CustomerGrouping.CustomerDetailId;
            CustomerDetail_CustomerGroupingDAO.CustomerGroupingId = CustomerDetail_CustomerGrouping.CustomerGroupingId;
            CustomerDetail_CustomerGroupingDAO.BusinessGroupId = CustomerDetail_CustomerGrouping.BusinessGroupId;
            CustomerDetail_CustomerGroupingDAO.Disabled = false;
            
            await ERPContext.CustomerDetail_CustomerGrouping.AddAsync(CustomerDetail_CustomerGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CustomerDetail_CustomerGrouping CustomerDetail_CustomerGrouping)
        {
            CustomerDetail_CustomerGroupingDAO CustomerDetail_CustomerGroupingDAO = ERPContext.CustomerDetail_CustomerGrouping.Where(b => b.Id == CustomerDetail_CustomerGrouping.Id).FirstOrDefault();
            
            CustomerDetail_CustomerGroupingDAO.Id = CustomerDetail_CustomerGrouping.Id;
            CustomerDetail_CustomerGroupingDAO.CustomerDetailId = CustomerDetail_CustomerGrouping.CustomerDetailId;
            CustomerDetail_CustomerGroupingDAO.CustomerGroupingId = CustomerDetail_CustomerGrouping.CustomerGroupingId;
            CustomerDetail_CustomerGroupingDAO.BusinessGroupId = CustomerDetail_CustomerGrouping.BusinessGroupId;
            CustomerDetail_CustomerGroupingDAO.Disabled = false;
            ERPContext.CustomerDetail_CustomerGrouping.Update(CustomerDetail_CustomerGroupingDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CustomerDetail_CustomerGroupingDAO CustomerDetail_CustomerGroupingDAO = await ERPContext.CustomerDetail_CustomerGrouping.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CustomerDetail_CustomerGroupingDAO.Disabled = true;
            ERPContext.CustomerDetail_CustomerGrouping.Update(CustomerDetail_CustomerGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
