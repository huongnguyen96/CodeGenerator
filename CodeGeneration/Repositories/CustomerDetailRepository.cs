
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
    public interface ICustomerDetailRepository
    {
        Task<int> Count(CustomerDetailFilter CustomerDetailFilter);
        Task<List<CustomerDetail>> List(CustomerDetailFilter CustomerDetailFilter);
        Task<CustomerDetail> Get(Guid Id);
        Task<bool> Create(CustomerDetail CustomerDetail);
        Task<bool> Update(CustomerDetail CustomerDetail);
        Task<bool> Delete(Guid Id);
        
    }
    public class CustomerDetailRepository : ICustomerDetailRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CustomerDetailRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerDetailDAO> DynamicFilter(IQueryable<CustomerDetailDAO> query, CustomerDetailFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.CustomerId != null)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.PaymentTermId.HasValue)
                query = query.Where(q => q.PaymentTermId.HasValue && q.PaymentTermId.Value == filter.PaymentTermId.Value);
            if (filter.PaymentTermId != null)
                query = query.Where(q => q.PaymentTermId, filter.PaymentTermId);
            if (filter.DueInDays.HasValue)
                query = query.Where(q => q.DueInDays.HasValue && q.DueInDays.Value == filter.DueInDays.Value);
            if (filter.DueInDays != null)
                query = query.Where(q => q.DueInDays, filter.DueInDays);
            if (filter.DebtLoad.HasValue)
                query = query.Where(q => q.DebtLoad.HasValue && q.DebtLoad.Value == filter.DebtLoad.Value);
            if (filter.DebtLoad != null)
                query = query.Where(q => q.DebtLoad, filter.DebtLoad);
            if (filter.StaffInChargeId.HasValue)
                query = query.Where(q => q.StaffInChargeId.HasValue && q.StaffInChargeId.Value == filter.StaffInChargeId.Value);
            if (filter.StaffInChargeId != null)
                query = query.Where(q => q.StaffInChargeId, filter.StaffInChargeId);
            return query;
        }
        private IQueryable<CustomerDetailDAO> DynamicOrder(IQueryable<CustomerDetailDAO> query,  CustomerDetailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerDetailOrder.PaymentTermId:
                            query = query.OrderBy(q => q.PaymentTermId);
                            break;
                        case CustomerDetailOrder.DueInDays:
                            query = query.OrderBy(q => q.DueInDays);
                            break;
                        case CustomerDetailOrder.DebtLoad:
                            query = query.OrderBy(q => q.DebtLoad);
                            break;
                        case CustomerDetailOrder.StaffInChargeId:
                            query = query.OrderBy(q => q.StaffInChargeId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerDetailOrder.PaymentTermId:
                            query = query.OrderByDescending(q => q.PaymentTermId);
                            break;
                        case CustomerDetailOrder.DueInDays:
                            query = query.OrderByDescending(q => q.DueInDays);
                            break;
                        case CustomerDetailOrder.DebtLoad:
                            query = query.OrderByDescending(q => q.DebtLoad);
                            break;
                        case CustomerDetailOrder.StaffInChargeId:
                            query = query.OrderByDescending(q => q.StaffInChargeId);
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

        private async Task<List<CustomerDetail>> DynamicSelect(IQueryable<CustomerDetailDAO> query, CustomerDetailFilter filter)
        {
            List <CustomerDetail> CustomerDetails = await query.Select(q => new CustomerDetail()
            {
                
                Id = filter.Selects.Contains(CustomerDetailSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CustomerDetailSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                CustomerId = filter.Selects.Contains(CustomerDetailSelect.Customer) ? q.CustomerId : default(Guid),
                LegalEntityId = filter.Selects.Contains(CustomerDetailSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                PaymentTermId = filter.Selects.Contains(CustomerDetailSelect.PaymentTerm) ? q.PaymentTermId : default(Guid?),
                DueInDays = filter.Selects.Contains(CustomerDetailSelect.DueInDays) ? q.DueInDays : default(Guid?),
                DebtLoad = filter.Selects.Contains(CustomerDetailSelect.DebtLoad) ? q.DebtLoad : default(Guid?),
                StaffInChargeId = filter.Selects.Contains(CustomerDetailSelect.StaffInCharge) ? q.StaffInChargeId : default(Guid?),
            }).ToListAsync();
            return CustomerDetails;
        }

        public async Task<int> Count(CustomerDetailFilter filter)
        {
            IQueryable <CustomerDetailDAO> CustomerDetailDAOs = ERPContext.CustomerDetail;
            CustomerDetailDAOs = DynamicFilter(CustomerDetailDAOs, filter);
            return await CustomerDetailDAOs.CountAsync();
        }

        public async Task<List<CustomerDetail>> List(CustomerDetailFilter filter)
        {
            if (filter == null) return new List<CustomerDetail>();
            IQueryable<CustomerDetailDAO> CustomerDetailDAOs = ERPContext.CustomerDetail;
            CustomerDetailDAOs = DynamicFilter(CustomerDetailDAOs, filter);
            CustomerDetailDAOs = DynamicOrder(CustomerDetailDAOs, filter);
            var CustomerDetails = await DynamicSelect(CustomerDetailDAOs, filter);
            return CustomerDetails;
        }

        public async Task<CustomerDetail> Get(Guid Id)
        {
            CustomerDetail CustomerDetail = await ERPContext.CustomerDetail.Where(l => l.Id == Id).Select(CustomerDetailDAO => new CustomerDetail()
            {
                 
                Id = CustomerDetailDAO.Id,
                BusinessGroupId = CustomerDetailDAO.BusinessGroupId,
                CustomerId = CustomerDetailDAO.CustomerId,
                LegalEntityId = CustomerDetailDAO.LegalEntityId,
                PaymentTermId = CustomerDetailDAO.PaymentTermId,
                DueInDays = CustomerDetailDAO.DueInDays,
                DebtLoad = CustomerDetailDAO.DebtLoad,
                StaffInChargeId = CustomerDetailDAO.StaffInChargeId,
            }).FirstOrDefaultAsync();
            return CustomerDetail;
        }

        public async Task<bool> Create(CustomerDetail CustomerDetail)
        {
            CustomerDetailDAO CustomerDetailDAO = new CustomerDetailDAO();
            
            CustomerDetailDAO.Id = CustomerDetail.Id;
            CustomerDetailDAO.BusinessGroupId = CustomerDetail.BusinessGroupId;
            CustomerDetailDAO.CustomerId = CustomerDetail.CustomerId;
            CustomerDetailDAO.LegalEntityId = CustomerDetail.LegalEntityId;
            CustomerDetailDAO.PaymentTermId = CustomerDetail.PaymentTermId;
            CustomerDetailDAO.DueInDays = CustomerDetail.DueInDays;
            CustomerDetailDAO.DebtLoad = CustomerDetail.DebtLoad;
            CustomerDetailDAO.StaffInChargeId = CustomerDetail.StaffInChargeId;
            CustomerDetailDAO.Disabled = false;
            
            await ERPContext.CustomerDetail.AddAsync(CustomerDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CustomerDetail CustomerDetail)
        {
            CustomerDetailDAO CustomerDetailDAO = ERPContext.CustomerDetail.Where(b => b.Id == CustomerDetail.Id).FirstOrDefault();
            
            CustomerDetailDAO.Id = CustomerDetail.Id;
            CustomerDetailDAO.BusinessGroupId = CustomerDetail.BusinessGroupId;
            CustomerDetailDAO.CustomerId = CustomerDetail.CustomerId;
            CustomerDetailDAO.LegalEntityId = CustomerDetail.LegalEntityId;
            CustomerDetailDAO.PaymentTermId = CustomerDetail.PaymentTermId;
            CustomerDetailDAO.DueInDays = CustomerDetail.DueInDays;
            CustomerDetailDAO.DebtLoad = CustomerDetail.DebtLoad;
            CustomerDetailDAO.StaffInChargeId = CustomerDetail.StaffInChargeId;
            CustomerDetailDAO.Disabled = false;
            ERPContext.CustomerDetail.Update(CustomerDetailDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CustomerDetailDAO CustomerDetailDAO = await ERPContext.CustomerDetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CustomerDetailDAO.Disabled = true;
            ERPContext.CustomerDetail.Update(CustomerDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
