
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
    public interface IOrderStatusRepository
    {
        Task<int> Count(OrderStatusFilter OrderStatusFilter);
        Task<List<OrderStatus>> List(OrderStatusFilter OrderStatusFilter);
        Task<OrderStatus> Get(long Id);
        Task<bool> Create(OrderStatus OrderStatus);
        Task<bool> Update(OrderStatus OrderStatus);
        Task<bool> Delete(OrderStatus OrderStatus);
        
    }
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public OrderStatusRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<OrderStatusDAO> DynamicFilter(IQueryable<OrderStatusDAO> query, OrderStatusFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<OrderStatusDAO> DynamicOrder(IQueryable<OrderStatusDAO> query,  OrderStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case OrderStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case OrderStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case OrderStatusOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case OrderStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case OrderStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case OrderStatusOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderStatus>> DynamicSelect(IQueryable<OrderStatusDAO> query, OrderStatusFilter filter)
        {
            List <OrderStatus> OrderStatuss = await query.Select(q => new OrderStatus()
            {
                
                Id = filter.Selects.Contains(OrderStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(OrderStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(OrderStatusSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(OrderStatusSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return OrderStatuss;
        }

        public async Task<int> Count(OrderStatusFilter filter)
        {
            IQueryable <OrderStatusDAO> OrderStatusDAOs = DataContext.OrderStatus;
            OrderStatusDAOs = DynamicFilter(OrderStatusDAOs, filter);
            return await OrderStatusDAOs.CountAsync();
        }

        public async Task<List<OrderStatus>> List(OrderStatusFilter filter)
        {
            if (filter == null) return new List<OrderStatus>();
            IQueryable<OrderStatusDAO> OrderStatusDAOs = DataContext.OrderStatus;
            OrderStatusDAOs = DynamicFilter(OrderStatusDAOs, filter);
            OrderStatusDAOs = DynamicOrder(OrderStatusDAOs, filter);
            var OrderStatuss = await DynamicSelect(OrderStatusDAOs, filter);
            return OrderStatuss;
        }

        
        public async Task<OrderStatus> Get(long Id)
        {
            OrderStatus OrderStatus = await DataContext.OrderStatus.Where(x => x.Id == Id).Select(OrderStatusDAO => new OrderStatus()
            {
                 
                Id = OrderStatusDAO.Id,
                Code = OrderStatusDAO.Code,
                Name = OrderStatusDAO.Name,
                Description = OrderStatusDAO.Description,
            }).FirstOrDefaultAsync();
            return OrderStatus;
        }

        public async Task<bool> Create(OrderStatus OrderStatus)
        {
            OrderStatusDAO OrderStatusDAO = new OrderStatusDAO();
            
            OrderStatusDAO.Id = OrderStatus.Id;
            OrderStatusDAO.Code = OrderStatus.Code;
            OrderStatusDAO.Name = OrderStatus.Name;
            OrderStatusDAO.Description = OrderStatus.Description;
            
            await DataContext.OrderStatus.AddAsync(OrderStatusDAO);
            await DataContext.SaveChangesAsync();
            OrderStatus.Id = OrderStatusDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(OrderStatus OrderStatus)
        {
            OrderStatusDAO OrderStatusDAO = DataContext.OrderStatus.Where(x => x.Id == OrderStatus.Id).FirstOrDefault();
            
            OrderStatusDAO.Id = OrderStatus.Id;
            OrderStatusDAO.Code = OrderStatus.Code;
            OrderStatusDAO.Name = OrderStatus.Name;
            OrderStatusDAO.Description = OrderStatus.Description;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(OrderStatus OrderStatus)
        {
            OrderStatusDAO OrderStatusDAO = await DataContext.OrderStatus.Where(x => x.Id == OrderStatus.Id).FirstOrDefaultAsync();
            DataContext.OrderStatus.Remove(OrderStatusDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
