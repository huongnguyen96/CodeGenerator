
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
    public interface IOrderRepository
    {
        Task<int> Count(OrderFilter OrderFilter);
        Task<List<Order>> List(OrderFilter OrderFilter);
        Task<Order> Get(long Id);
        Task<bool> Create(Order Order);
        Task<bool> Update(Order Order);
        Task<bool> Delete(Order Order);
        
    }
    public class OrderRepository : IOrderRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public OrderRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<OrderDAO> DynamicFilter(IQueryable<OrderDAO> query, OrderFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerId != null)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.CreatedDate != null)
                query = query.Where(q => q.CreatedDate, filter.CreatedDate);
            if (filter.VoucherCode != null)
                query = query.Where(q => q.VoucherCode, filter.VoucherCode);
            if (filter.Total != null)
                query = query.Where(q => q.Total, filter.Total);
            if (filter.VoucherDiscount != null)
                query = query.Where(q => q.VoucherDiscount, filter.VoucherDiscount);
            if (filter.CampaignDiscount != null)
                query = query.Where(q => q.CampaignDiscount, filter.CampaignDiscount);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<OrderDAO> DynamicOrder(IQueryable<OrderDAO> query,  OrderFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case OrderOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderOrder.Customer:
                            query = query.OrderBy(q => q.Customer.Id);
                            break;
                        case OrderOrder.CreatedDate:
                            query = query.OrderBy(q => q.CreatedDate);
                            break;
                        case OrderOrder.VoucherCode:
                            query = query.OrderBy(q => q.VoucherCode);
                            break;
                        case OrderOrder.Total:
                            query = query.OrderBy(q => q.Total);
                            break;
                        case OrderOrder.VoucherDiscount:
                            query = query.OrderBy(q => q.VoucherDiscount);
                            break;
                        case OrderOrder.CampaignDiscount:
                            query = query.OrderBy(q => q.CampaignDiscount);
                            break;
                        case OrderOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case OrderOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderOrder.Customer:
                            query = query.OrderByDescending(q => q.Customer.Id);
                            break;
                        case OrderOrder.CreatedDate:
                            query = query.OrderByDescending(q => q.CreatedDate);
                            break;
                        case OrderOrder.VoucherCode:
                            query = query.OrderByDescending(q => q.VoucherCode);
                            break;
                        case OrderOrder.Total:
                            query = query.OrderByDescending(q => q.Total);
                            break;
                        case OrderOrder.VoucherDiscount:
                            query = query.OrderByDescending(q => q.VoucherDiscount);
                            break;
                        case OrderOrder.CampaignDiscount:
                            query = query.OrderByDescending(q => q.CampaignDiscount);
                            break;
                        case OrderOrder.Status:
                            query = query.OrderByDescending(q => q.Status.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Order>> DynamicSelect(IQueryable<OrderDAO> query, OrderFilter filter)
        {
            List <Order> Orders = await query.Select(q => new Order()
            {
                
                Id = filter.Selects.Contains(OrderSelect.Id) ? q.Id : default(long),
                CustomerId = filter.Selects.Contains(OrderSelect.Customer) ? q.CustomerId : default(long),
                CreatedDate = filter.Selects.Contains(OrderSelect.CreatedDate) ? q.CreatedDate : default(DateTime),
                VoucherCode = filter.Selects.Contains(OrderSelect.VoucherCode) ? q.VoucherCode : default(string),
                Total = filter.Selects.Contains(OrderSelect.Total) ? q.Total : default(long),
                VoucherDiscount = filter.Selects.Contains(OrderSelect.VoucherDiscount) ? q.VoucherDiscount : default(long),
                CampaignDiscount = filter.Selects.Contains(OrderSelect.CampaignDiscount) ? q.CampaignDiscount : default(long),
                StatusId = filter.Selects.Contains(OrderSelect.Status) ? q.StatusId : default(long),
                Customer = filter.Selects.Contains(OrderSelect.Customer) && q.Customer != null ? new Customer
                {
                    
                    Id = q.Customer.Id,
                    Username = q.Customer.Username,
                    DisplayName = q.Customer.DisplayName,
                    PhoneNumber = q.Customer.PhoneNumber,
                    Email = q.Customer.Email,
                } : null,
                Status = filter.Selects.Contains(OrderSelect.Status) && q.Status != null ? new OrderStatus
                {
                    
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                    Description = q.Status.Description,
                } : null,
            }).ToListAsync();
            return Orders;
        }

        public async Task<int> Count(OrderFilter filter)
        {
            IQueryable <OrderDAO> OrderDAOs = DataContext.Order;
            OrderDAOs = DynamicFilter(OrderDAOs, filter);
            return await OrderDAOs.CountAsync();
        }

        public async Task<List<Order>> List(OrderFilter filter)
        {
            if (filter == null) return new List<Order>();
            IQueryable<OrderDAO> OrderDAOs = DataContext.Order;
            OrderDAOs = DynamicFilter(OrderDAOs, filter);
            OrderDAOs = DynamicOrder(OrderDAOs, filter);
            var Orders = await DynamicSelect(OrderDAOs, filter);
            return Orders;
        }

        
        public async Task<Order> Get(long Id)
        {
            Order Order = await DataContext.Order.Where(x => x.Id == Id).Select(OrderDAO => new Order()
            {
                 
                Id = OrderDAO.Id,
                CustomerId = OrderDAO.CustomerId,
                CreatedDate = OrderDAO.CreatedDate,
                VoucherCode = OrderDAO.VoucherCode,
                Total = OrderDAO.Total,
                VoucherDiscount = OrderDAO.VoucherDiscount,
                CampaignDiscount = OrderDAO.CampaignDiscount,
                StatusId = OrderDAO.StatusId,
                Customer = OrderDAO.Customer == null ? null : new Customer
                {
                    
                    Id = OrderDAO.Customer.Id,
                    Username = OrderDAO.Customer.Username,
                    DisplayName = OrderDAO.Customer.DisplayName,
                    PhoneNumber = OrderDAO.Customer.PhoneNumber,
                    Email = OrderDAO.Customer.Email,
                },
                Status = OrderDAO.Status == null ? null : new OrderStatus
                {
                    
                    Id = OrderDAO.Status.Id,
                    Code = OrderDAO.Status.Code,
                    Name = OrderDAO.Status.Name,
                    Description = OrderDAO.Status.Description,
                },
            }).FirstOrDefaultAsync();
            return Order;
        }

        public async Task<bool> Create(Order Order)
        {
            OrderDAO OrderDAO = new OrderDAO();
            
            OrderDAO.Id = Order.Id;
            OrderDAO.CustomerId = Order.CustomerId;
            OrderDAO.CreatedDate = Order.CreatedDate;
            OrderDAO.VoucherCode = Order.VoucherCode;
            OrderDAO.Total = Order.Total;
            OrderDAO.VoucherDiscount = Order.VoucherDiscount;
            OrderDAO.CampaignDiscount = Order.CampaignDiscount;
            OrderDAO.StatusId = Order.StatusId;
            
            await DataContext.Order.AddAsync(OrderDAO);
            await DataContext.SaveChangesAsync();
            Order.Id = OrderDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Order Order)
        {
            OrderDAO OrderDAO = DataContext.Order.Where(x => x.Id == Order.Id).FirstOrDefault();
            
            OrderDAO.Id = Order.Id;
            OrderDAO.CustomerId = Order.CustomerId;
            OrderDAO.CreatedDate = Order.CreatedDate;
            OrderDAO.VoucherCode = Order.VoucherCode;
            OrderDAO.Total = Order.Total;
            OrderDAO.VoucherDiscount = Order.VoucherDiscount;
            OrderDAO.CampaignDiscount = Order.CampaignDiscount;
            OrderDAO.StatusId = Order.StatusId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Order Order)
        {
            OrderDAO OrderDAO = await DataContext.Order.Where(x => x.Id == Order.Id).FirstOrDefaultAsync();
            DataContext.Order.Remove(OrderDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
