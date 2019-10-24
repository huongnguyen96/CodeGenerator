
using Common;
using WG.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> Count(CustomerFilter CustomerFilter);
        Task<List<Customer>> List(CustomerFilter CustomerFilter);
        Task<Customer> Get(long Id);
        Task<bool> Create(Customer Customer);
        Task<bool> Update(Customer Customer);
        Task<bool> Delete(Customer Customer);
        
    }
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public CustomerRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerDAO> DynamicFilter(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Username != null)
                query = query.Where(q => q.Username, filter.Username);
            if (filter.DisplayName != null)
                query = query.Where(q => q.DisplayName, filter.DisplayName);
            if (filter.PhoneNumber != null)
                query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            if (filter.Email != null)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<CustomerDAO> DynamicOrder(IQueryable<CustomerDAO> query,  CustomerFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerOrder.Username:
                            query = query.OrderBy(q => q.Username);
                            break;
                        case CustomerOrder.DisplayName:
                            query = query.OrderBy(q => q.DisplayName);
                            break;
                        case CustomerOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case CustomerOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerOrder.Username:
                            query = query.OrderByDescending(q => q.Username);
                            break;
                        case CustomerOrder.DisplayName:
                            query = query.OrderByDescending(q => q.DisplayName);
                            break;
                        case CustomerOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                        case CustomerOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Customer>> DynamicSelect(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            List <Customer> Customers = await query.Select(q => new Customer()
            {
                
                Id = filter.Selects.Contains(CustomerSelect.Id) ? q.Id : default(long),
                Username = filter.Selects.Contains(CustomerSelect.Username) ? q.Username : default(string),
                DisplayName = filter.Selects.Contains(CustomerSelect.DisplayName) ? q.DisplayName : default(string),
                PhoneNumber = filter.Selects.Contains(CustomerSelect.PhoneNumber) ? q.PhoneNumber : default(string),
                Email = filter.Selects.Contains(CustomerSelect.Email) ? q.Email : default(string),
            }).ToListAsync();
            return Customers;
        }

        public async Task<int> Count(CustomerFilter filter)
        {
            IQueryable <CustomerDAO> CustomerDAOs = DataContext.Customer;
            CustomerDAOs = DynamicFilter(CustomerDAOs, filter);
            return await CustomerDAOs.CountAsync();
        }

        public async Task<List<Customer>> List(CustomerFilter filter)
        {
            if (filter == null) return new List<Customer>();
            IQueryable<CustomerDAO> CustomerDAOs = DataContext.Customer;
            CustomerDAOs = DynamicFilter(CustomerDAOs, filter);
            CustomerDAOs = DynamicOrder(CustomerDAOs, filter);
            var Customers = await DynamicSelect(CustomerDAOs, filter);
            return Customers;
        }

        
        public async Task<Customer> Get(long Id)
        {
            Customer Customer = await DataContext.Customer.Where(x => x.Id == Id).Select(CustomerDAO => new Customer()
            {
                 
                Id = CustomerDAO.Id,
                Username = CustomerDAO.Username,
                DisplayName = CustomerDAO.DisplayName,
                PhoneNumber = CustomerDAO.PhoneNumber,
                Email = CustomerDAO.Email,
            }).FirstOrDefaultAsync();
            return Customer;
        }

        public async Task<bool> Create(Customer Customer)
        {
            CustomerDAO CustomerDAO = new CustomerDAO();
            
            CustomerDAO.Id = Customer.Id;
            CustomerDAO.Username = Customer.Username;
            CustomerDAO.DisplayName = Customer.DisplayName;
            CustomerDAO.PhoneNumber = Customer.PhoneNumber;
            CustomerDAO.Email = Customer.Email;
            
            await DataContext.Customer.AddAsync(CustomerDAO);
            await DataContext.SaveChangesAsync();
            Customer.Id = CustomerDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Customer Customer)
        {
            CustomerDAO CustomerDAO = DataContext.Customer.Where(x => x.Id == Customer.Id).FirstOrDefault();
            
            CustomerDAO.Id = Customer.Id;
            CustomerDAO.Username = Customer.Username;
            CustomerDAO.DisplayName = Customer.DisplayName;
            CustomerDAO.PhoneNumber = Customer.PhoneNumber;
            CustomerDAO.Email = Customer.Email;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Customer Customer)
        {
            CustomerDAO CustomerDAO = await DataContext.Customer.Where(x => x.Id == Customer.Id).FirstOrDefaultAsync();
            DataContext.Customer.Remove(CustomerDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
