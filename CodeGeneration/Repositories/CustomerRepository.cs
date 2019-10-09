
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
    public interface ICustomerRepository
    {
        Task<int> Count(CustomerFilter CustomerFilter);
        Task<List<Customer>> List(CustomerFilter CustomerFilter);
        Task<Customer> Get(Guid Id);
        Task<bool> Create(Customer Customer);
        Task<bool> Update(Customer Customer);
        Task<bool> Delete(Guid Id);
        
    }
    public class CustomerRepository : ICustomerRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CustomerRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerDAO> DynamicFilter(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.TaxCode != null)
                query = query.Where(q => q.TaxCode, filter.TaxCode);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Note != null)
                query = query.Where(q => q.Note, filter.Note);
            return query;
        }
        private IQueryable<CustomerDAO> DynamicOrder(IQueryable<CustomerDAO> query,  CustomerFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CustomerOrder.TaxCode:
                            query = query.OrderBy(q => q.TaxCode);
                            break;
                        case CustomerOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CustomerOrder.TaxCode:
                            query = query.OrderByDescending(q => q.TaxCode);
                            break;
                        case CustomerOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
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

        private async Task<List<Customer>> DynamicSelect(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            List <Customer> Customers = await query.Select(q => new Customer()
            {
                
                Id = filter.Selects.Contains(CustomerSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CustomerSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Code = filter.Selects.Contains(CustomerSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerSelect.Name) ? q.Name : default(string),
                Address = filter.Selects.Contains(CustomerSelect.Address) ? q.Address : default(string),
                TaxCode = filter.Selects.Contains(CustomerSelect.TaxCode) ? q.TaxCode : default(string),
                Description = filter.Selects.Contains(CustomerSelect.Description) ? q.Description : default(string),
                StatusId = filter.Selects.Contains(CustomerSelect.Status) ? q.StatusId : default(Guid),
                Note = filter.Selects.Contains(CustomerSelect.Note) ? q.Note : default(string),
            }).ToListAsync();
            return Customers;
        }

        public async Task<int> Count(CustomerFilter filter)
        {
            IQueryable <CustomerDAO> CustomerDAOs = ERPContext.Customer;
            CustomerDAOs = DynamicFilter(CustomerDAOs, filter);
            return await CustomerDAOs.CountAsync();
        }

        public async Task<List<Customer>> List(CustomerFilter filter)
        {
            if (filter == null) return new List<Customer>();
            IQueryable<CustomerDAO> CustomerDAOs = ERPContext.Customer;
            CustomerDAOs = DynamicFilter(CustomerDAOs, filter);
            CustomerDAOs = DynamicOrder(CustomerDAOs, filter);
            var Customers = await DynamicSelect(CustomerDAOs, filter);
            return Customers;
        }

        public async Task<Customer> Get(Guid Id)
        {
            Customer Customer = await ERPContext.Customer.Where(l => l.Id == Id).Select(CustomerDAO => new Customer()
            {
                 
                Id = CustomerDAO.Id,
                BusinessGroupId = CustomerDAO.BusinessGroupId,
                Code = CustomerDAO.Code,
                Name = CustomerDAO.Name,
                Address = CustomerDAO.Address,
                TaxCode = CustomerDAO.TaxCode,
                Description = CustomerDAO.Description,
                StatusId = CustomerDAO.StatusId,
                Note = CustomerDAO.Note,
            }).FirstOrDefaultAsync();
            return Customer;
        }

        public async Task<bool> Create(Customer Customer)
        {
            CustomerDAO CustomerDAO = new CustomerDAO();
            
            CustomerDAO.Id = Customer.Id;
            CustomerDAO.BusinessGroupId = Customer.BusinessGroupId;
            CustomerDAO.Code = Customer.Code;
            CustomerDAO.Name = Customer.Name;
            CustomerDAO.Address = Customer.Address;
            CustomerDAO.TaxCode = Customer.TaxCode;
            CustomerDAO.Description = Customer.Description;
            CustomerDAO.StatusId = Customer.StatusId;
            CustomerDAO.Note = Customer.Note;
            CustomerDAO.Disabled = false;
            
            await ERPContext.Customer.AddAsync(CustomerDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Customer Customer)
        {
            CustomerDAO CustomerDAO = ERPContext.Customer.Where(b => b.Id == Customer.Id).FirstOrDefault();
            
            CustomerDAO.Id = Customer.Id;
            CustomerDAO.BusinessGroupId = Customer.BusinessGroupId;
            CustomerDAO.Code = Customer.Code;
            CustomerDAO.Name = Customer.Name;
            CustomerDAO.Address = Customer.Address;
            CustomerDAO.TaxCode = Customer.TaxCode;
            CustomerDAO.Description = Customer.Description;
            CustomerDAO.StatusId = Customer.StatusId;
            CustomerDAO.Note = Customer.Note;
            CustomerDAO.Disabled = false;
            ERPContext.Customer.Update(CustomerDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CustomerDAO CustomerDAO = await ERPContext.Customer.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CustomerDAO.Disabled = true;
            ERPContext.Customer.Update(CustomerDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
