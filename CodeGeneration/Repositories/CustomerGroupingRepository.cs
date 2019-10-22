
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
    public interface ICustomerGroupingRepository
    {
        Task<int> Count(CustomerGroupingFilter CustomerGroupingFilter);
        Task<List<CustomerGrouping>> List(CustomerGroupingFilter CustomerGroupingFilter);
        Task<CustomerGrouping> Get(long Id);
        Task<bool> Create(CustomerGrouping CustomerGrouping);
        Task<bool> Update(CustomerGrouping CustomerGrouping);
        Task<bool> Delete(CustomerGrouping CustomerGrouping);
        
    }
    public class CustomerGroupingRepository : ICustomerGroupingRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public CustomerGroupingRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerGroupingDAO> DynamicFilter(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<CustomerGroupingDAO> DynamicOrder(IQueryable<CustomerGroupingDAO> query,  CustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerGrouping>> DynamicSelect(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            List <CustomerGrouping> CustomerGroupings = await query.Select(q => new CustomerGrouping()
            {
                
                Id = filter.Selects.Contains(CustomerGroupingSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(CustomerGroupingSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CustomerGroupings;
        }

        public async Task<int> Count(CustomerGroupingFilter filter)
        {
            IQueryable <CustomerGroupingDAO> CustomerGroupingDAOs = DataContext.CustomerGrouping;
            CustomerGroupingDAOs = DynamicFilter(CustomerGroupingDAOs, filter);
            return await CustomerGroupingDAOs.CountAsync();
        }

        public async Task<List<CustomerGrouping>> List(CustomerGroupingFilter filter)
        {
            if (filter == null) return new List<CustomerGrouping>();
            IQueryable<CustomerGroupingDAO> CustomerGroupingDAOs = DataContext.CustomerGrouping;
            CustomerGroupingDAOs = DynamicFilter(CustomerGroupingDAOs, filter);
            CustomerGroupingDAOs = DynamicOrder(CustomerGroupingDAOs, filter);
            var CustomerGroupings = await DynamicSelect(CustomerGroupingDAOs, filter);
            return CustomerGroupings;
        }

        
        public async Task<CustomerGrouping> Get(long Id)
        {
            CustomerGrouping CustomerGrouping = await DataContext.CustomerGrouping.Where(x => x.Id == Id).Select(CustomerGroupingDAO => new CustomerGrouping()
            {
                 
                Id = CustomerGroupingDAO.Id,
                Name = CustomerGroupingDAO.Name,
            }).FirstOrDefaultAsync();
            return CustomerGrouping;
        }

        public async Task<bool> Create(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = new CustomerGroupingDAO();
            
            CustomerGroupingDAO.Id = CustomerGrouping.Id;
            CustomerGroupingDAO.Name = CustomerGrouping.Name;
            
            await DataContext.CustomerGrouping.AddAsync(CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            CustomerGrouping.Id = CustomerGroupingDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = DataContext.CustomerGrouping.Where(x => x.Id == CustomerGrouping.Id).FirstOrDefault();
            
            CustomerGroupingDAO.Id = CustomerGrouping.Id;
            CustomerGroupingDAO.Name = CustomerGrouping.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = await DataContext.CustomerGrouping.Where(x => x.Id == CustomerGrouping.Id).FirstOrDefaultAsync();
            DataContext.CustomerGrouping.Remove(CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
