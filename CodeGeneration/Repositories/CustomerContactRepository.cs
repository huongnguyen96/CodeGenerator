
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
    public interface ICustomerContactRepository
    {
        Task<int> Count(CustomerContactFilter CustomerContactFilter);
        Task<List<CustomerContact>> List(CustomerContactFilter CustomerContactFilter);
        Task<CustomerContact> Get(Guid Id);
        Task<bool> Create(CustomerContact CustomerContact);
        Task<bool> Update(CustomerContact CustomerContact);
        Task<bool> Delete(Guid Id);
        
    }
    public class CustomerContactRepository : ICustomerContactRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CustomerContactRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerContactDAO> DynamicFilter(IQueryable<CustomerContactDAO> query, CustomerContactFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerDetailId != null)
                query = query.Where(q => q.CustomerDetailId, filter.CustomerDetailId);
            if (filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue && q.ProvinceId.Value == filter.ProvinceId.Value);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.Email != null)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.FullName != null)
                query = query.Where(q => q.FullName, filter.FullName);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<CustomerContactDAO> DynamicOrder(IQueryable<CustomerContactDAO> query,  CustomerContactFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerContactOrder.ProvinceId:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case CustomerContactOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case CustomerContactOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CustomerContactOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CustomerContactOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case CustomerContactOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerContactOrder.ProvinceId:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case CustomerContactOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case CustomerContactOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CustomerContactOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CustomerContactOrder.FullName:
                            query = query.OrderByDescending(q => q.FullName);
                            break;
                        case CustomerContactOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
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

        private async Task<List<CustomerContact>> DynamicSelect(IQueryable<CustomerContactDAO> query, CustomerContactFilter filter)
        {
            List <CustomerContact> CustomerContacts = await query.Select(q => new CustomerContact()
            {
                
                Id = filter.Selects.Contains(CustomerContactSelect.Id) ? q.Id : default(Guid),
                CustomerDetailId = filter.Selects.Contains(CustomerContactSelect.CustomerDetail) ? q.CustomerDetailId : default(Guid),
                ProvinceId = filter.Selects.Contains(CustomerContactSelect.Province) ? q.ProvinceId : default(Guid?),
                Phone = filter.Selects.Contains(CustomerContactSelect.Phone) ? q.Phone : default(string),
                Email = filter.Selects.Contains(CustomerContactSelect.Email) ? q.Email : default(string),
                Address = filter.Selects.Contains(CustomerContactSelect.Address) ? q.Address : default(string),
                FullName = filter.Selects.Contains(CustomerContactSelect.FullName) ? q.FullName : default(string),
                Description = filter.Selects.Contains(CustomerContactSelect.Description) ? q.Description : default(string),
                BusinessGroupId = filter.Selects.Contains(CustomerContactSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return CustomerContacts;
        }

        public async Task<int> Count(CustomerContactFilter filter)
        {
            IQueryable <CustomerContactDAO> CustomerContactDAOs = ERPContext.CustomerContact;
            CustomerContactDAOs = DynamicFilter(CustomerContactDAOs, filter);
            return await CustomerContactDAOs.CountAsync();
        }

        public async Task<List<CustomerContact>> List(CustomerContactFilter filter)
        {
            if (filter == null) return new List<CustomerContact>();
            IQueryable<CustomerContactDAO> CustomerContactDAOs = ERPContext.CustomerContact;
            CustomerContactDAOs = DynamicFilter(CustomerContactDAOs, filter);
            CustomerContactDAOs = DynamicOrder(CustomerContactDAOs, filter);
            var CustomerContacts = await DynamicSelect(CustomerContactDAOs, filter);
            return CustomerContacts;
        }

        public async Task<CustomerContact> Get(Guid Id)
        {
            CustomerContact CustomerContact = await ERPContext.CustomerContact.Where(l => l.Id == Id).Select(CustomerContactDAO => new CustomerContact()
            {
                 
                Id = CustomerContactDAO.Id,
                CustomerDetailId = CustomerContactDAO.CustomerDetailId,
                ProvinceId = CustomerContactDAO.ProvinceId,
                Phone = CustomerContactDAO.Phone,
                Email = CustomerContactDAO.Email,
                Address = CustomerContactDAO.Address,
                FullName = CustomerContactDAO.FullName,
                Description = CustomerContactDAO.Description,
                BusinessGroupId = CustomerContactDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return CustomerContact;
        }

        public async Task<bool> Create(CustomerContact CustomerContact)
        {
            CustomerContactDAO CustomerContactDAO = new CustomerContactDAO();
            
            CustomerContactDAO.Id = CustomerContact.Id;
            CustomerContactDAO.CustomerDetailId = CustomerContact.CustomerDetailId;
            CustomerContactDAO.ProvinceId = CustomerContact.ProvinceId;
            CustomerContactDAO.Phone = CustomerContact.Phone;
            CustomerContactDAO.Email = CustomerContact.Email;
            CustomerContactDAO.Address = CustomerContact.Address;
            CustomerContactDAO.FullName = CustomerContact.FullName;
            CustomerContactDAO.Description = CustomerContact.Description;
            CustomerContactDAO.BusinessGroupId = CustomerContact.BusinessGroupId;
            CustomerContactDAO.Disabled = false;
            
            await ERPContext.CustomerContact.AddAsync(CustomerContactDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CustomerContact CustomerContact)
        {
            CustomerContactDAO CustomerContactDAO = ERPContext.CustomerContact.Where(b => b.Id == CustomerContact.Id).FirstOrDefault();
            
            CustomerContactDAO.Id = CustomerContact.Id;
            CustomerContactDAO.CustomerDetailId = CustomerContact.CustomerDetailId;
            CustomerContactDAO.ProvinceId = CustomerContact.ProvinceId;
            CustomerContactDAO.Phone = CustomerContact.Phone;
            CustomerContactDAO.Email = CustomerContact.Email;
            CustomerContactDAO.Address = CustomerContact.Address;
            CustomerContactDAO.FullName = CustomerContact.FullName;
            CustomerContactDAO.Description = CustomerContact.Description;
            CustomerContactDAO.BusinessGroupId = CustomerContact.BusinessGroupId;
            CustomerContactDAO.Disabled = false;
            ERPContext.CustomerContact.Update(CustomerContactDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CustomerContactDAO CustomerContactDAO = await ERPContext.CustomerContact.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CustomerContactDAO.Disabled = true;
            ERPContext.CustomerContact.Update(CustomerContactDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
