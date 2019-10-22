
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
    public interface ICustomer_CustomerGroupingRepository
    {
        Task<int> Count(Customer_CustomerGroupingFilter Customer_CustomerGroupingFilter);
        Task<List<Customer_CustomerGrouping>> List(Customer_CustomerGroupingFilter Customer_CustomerGroupingFilter);
        Task<Customer_CustomerGrouping> Get(long CustomerId, long CustomerGroupingId);
        Task<bool> Create(Customer_CustomerGrouping Customer_CustomerGrouping);
        Task<bool> Update(Customer_CustomerGrouping Customer_CustomerGrouping);
        Task<bool> Delete(Customer_CustomerGrouping Customer_CustomerGrouping);
        
    }
    public class Customer_CustomerGroupingRepository : ICustomer_CustomerGroupingRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public Customer_CustomerGroupingRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Customer_CustomerGroupingDAO> DynamicFilter(IQueryable<Customer_CustomerGroupingDAO> query, Customer_CustomerGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.CustomerId != null)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.CustomerGroupingId != null)
                query = query.Where(q => q.CustomerGroupingId, filter.CustomerGroupingId);
            return query;
        }
        private IQueryable<Customer_CustomerGroupingDAO> DynamicOrder(IQueryable<Customer_CustomerGroupingDAO> query,  Customer_CustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case Customer_CustomerGroupingOrder.Customer:
                            query = query.OrderBy(q => q.Customer.Id);
                            break;
                        case Customer_CustomerGroupingOrder.CustomerGrouping:
                            query = query.OrderBy(q => q.CustomerGrouping.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case Customer_CustomerGroupingOrder.Customer:
                            query = query.OrderByDescending(q => q.Customer.Id);
                            break;
                        case Customer_CustomerGroupingOrder.CustomerGrouping:
                            query = query.OrderByDescending(q => q.CustomerGrouping.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Customer_CustomerGrouping>> DynamicSelect(IQueryable<Customer_CustomerGroupingDAO> query, Customer_CustomerGroupingFilter filter)
        {
            List <Customer_CustomerGrouping> Customer_CustomerGroupings = await query.Select(q => new Customer_CustomerGrouping()
            {
                
                CustomerId = filter.Selects.Contains(Customer_CustomerGroupingSelect.Customer) ? q.CustomerId : default(long),
                CustomerGroupingId = filter.Selects.Contains(Customer_CustomerGroupingSelect.CustomerGrouping) ? q.CustomerGroupingId : default(long),
                Customer = filter.Selects.Contains(Customer_CustomerGroupingSelect.Customer) && q.Customer != null ? new Customer
                {
                    
                    Id = q.Customer.Id,
                    Username = q.Customer.Username,
                    DisplayName = q.Customer.DisplayName,
                } : null,
                CustomerGrouping = filter.Selects.Contains(Customer_CustomerGroupingSelect.CustomerGrouping) && q.CustomerGrouping != null ? new CustomerGrouping
                {
                    
                    Id = q.CustomerGrouping.Id,
                    Name = q.CustomerGrouping.Name,
                } : null,
            }).ToListAsync();
            return Customer_CustomerGroupings;
        }

        public async Task<int> Count(Customer_CustomerGroupingFilter filter)
        {
            IQueryable <Customer_CustomerGroupingDAO> Customer_CustomerGroupingDAOs = DataContext.Customer_CustomerGrouping;
            Customer_CustomerGroupingDAOs = DynamicFilter(Customer_CustomerGroupingDAOs, filter);
            return await Customer_CustomerGroupingDAOs.CountAsync();
        }

        public async Task<List<Customer_CustomerGrouping>> List(Customer_CustomerGroupingFilter filter)
        {
            if (filter == null) return new List<Customer_CustomerGrouping>();
            IQueryable<Customer_CustomerGroupingDAO> Customer_CustomerGroupingDAOs = DataContext.Customer_CustomerGrouping;
            Customer_CustomerGroupingDAOs = DynamicFilter(Customer_CustomerGroupingDAOs, filter);
            Customer_CustomerGroupingDAOs = DynamicOrder(Customer_CustomerGroupingDAOs, filter);
            var Customer_CustomerGroupings = await DynamicSelect(Customer_CustomerGroupingDAOs, filter);
            return Customer_CustomerGroupings;
        }

        
        public async Task<Customer_CustomerGrouping> Get(long CustomerId, long CustomerGroupingId)
        {
            Customer_CustomerGrouping Customer_CustomerGrouping = await DataContext.Customer_CustomerGrouping.Where(x => x.CustomerId == CustomerId && x.CustomerGroupingId == CustomerGroupingId).Select(Customer_CustomerGroupingDAO => new Customer_CustomerGrouping()
            {
                 
                CustomerId = Customer_CustomerGroupingDAO.CustomerId,
                CustomerGroupingId = Customer_CustomerGroupingDAO.CustomerGroupingId,
                Customer = Customer_CustomerGroupingDAO.Customer == null ? null : new Customer
                {
                    
                    Id = Customer_CustomerGroupingDAO.Customer.Id,
                    Username = Customer_CustomerGroupingDAO.Customer.Username,
                    DisplayName = Customer_CustomerGroupingDAO.Customer.DisplayName,
                },
                CustomerGrouping = Customer_CustomerGroupingDAO.CustomerGrouping == null ? null : new CustomerGrouping
                {
                    
                    Id = Customer_CustomerGroupingDAO.CustomerGrouping.Id,
                    Name = Customer_CustomerGroupingDAO.CustomerGrouping.Name,
                },
            }).FirstOrDefaultAsync();
            return Customer_CustomerGrouping;
        }

        public async Task<bool> Create(Customer_CustomerGrouping Customer_CustomerGrouping)
        {
            Customer_CustomerGroupingDAO Customer_CustomerGroupingDAO = new Customer_CustomerGroupingDAO();
            
            Customer_CustomerGroupingDAO.CustomerId = Customer_CustomerGrouping.CustomerId;
            Customer_CustomerGroupingDAO.CustomerGroupingId = Customer_CustomerGrouping.CustomerGroupingId;
            
            await DataContext.Customer_CustomerGrouping.AddAsync(Customer_CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            
            return true;
        }

        
        
        public async Task<bool> Update(Customer_CustomerGrouping Customer_CustomerGrouping)
        {
            Customer_CustomerGroupingDAO Customer_CustomerGroupingDAO = DataContext.Customer_CustomerGrouping.Where(x => x.CustomerId == Customer_CustomerGrouping.CustomerId && x.CustomerGroupingId == Customer_CustomerGrouping.CustomerGroupingId).FirstOrDefault();
            
            Customer_CustomerGroupingDAO.CustomerId = Customer_CustomerGrouping.CustomerId;
            Customer_CustomerGroupingDAO.CustomerGroupingId = Customer_CustomerGrouping.CustomerGroupingId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Customer_CustomerGrouping Customer_CustomerGrouping)
        {
            Customer_CustomerGroupingDAO Customer_CustomerGroupingDAO = await DataContext.Customer_CustomerGrouping.Where(x => x.CustomerId == Customer_CustomerGrouping.CustomerId && x.CustomerGroupingId == Customer_CustomerGrouping.CustomerGroupingId).FirstOrDefaultAsync();
            DataContext.Customer_CustomerGrouping.Remove(Customer_CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
