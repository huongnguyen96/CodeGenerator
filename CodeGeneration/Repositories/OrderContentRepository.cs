
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
    public interface IOrderContentRepository
    {
        Task<int> Count(OrderContentFilter OrderContentFilter);
        Task<List<OrderContent>> List(OrderContentFilter OrderContentFilter);
        Task<OrderContent> Get(long Id);
        Task<bool> Create(OrderContent OrderContent);
        Task<bool> Update(OrderContent OrderContent);
        Task<bool> Delete(OrderContent OrderContent);
        
    }
    public class OrderContentRepository : IOrderContentRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public OrderContentRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<OrderContentDAO> DynamicFilter(IQueryable<OrderContentDAO> query, OrderContentFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.OrderId != null)
                query = query.Where(q => q.OrderId, filter.OrderId);
            if (filter.ItemName != null)
                query = query.Where(q => q.ItemName, filter.ItemName);
            if (filter.FirstVersion != null)
                query = query.Where(q => q.FirstVersion, filter.FirstVersion);
            if (filter.SecondVersion != null)
                query = query.Where(q => q.SecondVersion, filter.SecondVersion);
            if (filter.ThirdVersion != null)
                query = query.Where(q => q.ThirdVersion, filter.ThirdVersion);
            if (filter.Price != null)
                query = query.Where(q => q.Price, filter.Price);
            if (filter.DiscountPrice != null)
                query = query.Where(q => q.DiscountPrice, filter.DiscountPrice);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<OrderContentDAO> DynamicOrder(IQueryable<OrderContentDAO> query,  OrderContentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case OrderContentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderContentOrder.Order:
                            query = query.OrderBy(q => q.Order.Id);
                            break;
                        case OrderContentOrder.ItemName:
                            query = query.OrderBy(q => q.ItemName);
                            break;
                        case OrderContentOrder.FirstVersion:
                            query = query.OrderBy(q => q.FirstVersion);
                            break;
                        case OrderContentOrder.SecondVersion:
                            query = query.OrderBy(q => q.SecondVersion);
                            break;
                        case OrderContentOrder.ThirdVersion:
                            query = query.OrderBy(q => q.ThirdVersion);
                            break;
                        case OrderContentOrder.Price:
                            query = query.OrderBy(q => q.Price);
                            break;
                        case OrderContentOrder.DiscountPrice:
                            query = query.OrderBy(q => q.DiscountPrice);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case OrderContentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderContentOrder.Order:
                            query = query.OrderByDescending(q => q.Order.Id);
                            break;
                        case OrderContentOrder.ItemName:
                            query = query.OrderByDescending(q => q.ItemName);
                            break;
                        case OrderContentOrder.FirstVersion:
                            query = query.OrderByDescending(q => q.FirstVersion);
                            break;
                        case OrderContentOrder.SecondVersion:
                            query = query.OrderByDescending(q => q.SecondVersion);
                            break;
                        case OrderContentOrder.ThirdVersion:
                            query = query.OrderByDescending(q => q.ThirdVersion);
                            break;
                        case OrderContentOrder.Price:
                            query = query.OrderByDescending(q => q.Price);
                            break;
                        case OrderContentOrder.DiscountPrice:
                            query = query.OrderByDescending(q => q.DiscountPrice);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderContent>> DynamicSelect(IQueryable<OrderContentDAO> query, OrderContentFilter filter)
        {
            List <OrderContent> OrderContents = await query.Select(q => new OrderContent()
            {
                
                Id = filter.Selects.Contains(OrderContentSelect.Id) ? q.Id : default(long),
                OrderId = filter.Selects.Contains(OrderContentSelect.Order) ? q.OrderId : default(long),
                ItemName = filter.Selects.Contains(OrderContentSelect.ItemName) ? q.ItemName : default(string),
                FirstVersion = filter.Selects.Contains(OrderContentSelect.FirstVersion) ? q.FirstVersion : default(string),
                SecondVersion = filter.Selects.Contains(OrderContentSelect.SecondVersion) ? q.SecondVersion : default(string),
                ThirdVersion = filter.Selects.Contains(OrderContentSelect.ThirdVersion) ? q.ThirdVersion : default(string),
                Price = filter.Selects.Contains(OrderContentSelect.Price) ? q.Price : default(long),
                DiscountPrice = filter.Selects.Contains(OrderContentSelect.DiscountPrice) ? q.DiscountPrice : default(long),
                Order = filter.Selects.Contains(OrderContentSelect.Order) && q.Order != null ? new Order
                {
                    
                    Id = q.Order.Id,
                    CustomerId = q.Order.CustomerId,
                    CreatedDate = q.Order.CreatedDate,
                    VoucherCode = q.Order.VoucherCode,
                    Total = q.Order.Total,
                    VoucherDiscount = q.Order.VoucherDiscount,
                    CampaignDiscount = q.Order.CampaignDiscount,
                } : null,
            }).ToListAsync();
            return OrderContents;
        }

        public async Task<int> Count(OrderContentFilter filter)
        {
            IQueryable <OrderContentDAO> OrderContentDAOs = DataContext.OrderContent;
            OrderContentDAOs = DynamicFilter(OrderContentDAOs, filter);
            return await OrderContentDAOs.CountAsync();
        }

        public async Task<List<OrderContent>> List(OrderContentFilter filter)
        {
            if (filter == null) return new List<OrderContent>();
            IQueryable<OrderContentDAO> OrderContentDAOs = DataContext.OrderContent;
            OrderContentDAOs = DynamicFilter(OrderContentDAOs, filter);
            OrderContentDAOs = DynamicOrder(OrderContentDAOs, filter);
            var OrderContents = await DynamicSelect(OrderContentDAOs, filter);
            return OrderContents;
        }

        
        public async Task<OrderContent> Get(long Id)
        {
            OrderContent OrderContent = await DataContext.OrderContent.Where(x => x.Id == Id).Select(OrderContentDAO => new OrderContent()
            {
                 
                Id = OrderContentDAO.Id,
                OrderId = OrderContentDAO.OrderId,
                ItemName = OrderContentDAO.ItemName,
                FirstVersion = OrderContentDAO.FirstVersion,
                SecondVersion = OrderContentDAO.SecondVersion,
                ThirdVersion = OrderContentDAO.ThirdVersion,
                Price = OrderContentDAO.Price,
                DiscountPrice = OrderContentDAO.DiscountPrice,
                Order = OrderContentDAO.Order == null ? null : new Order
                {
                    
                    Id = OrderContentDAO.Order.Id,
                    CustomerId = OrderContentDAO.Order.CustomerId,
                    CreatedDate = OrderContentDAO.Order.CreatedDate,
                    VoucherCode = OrderContentDAO.Order.VoucherCode,
                    Total = OrderContentDAO.Order.Total,
                    VoucherDiscount = OrderContentDAO.Order.VoucherDiscount,
                    CampaignDiscount = OrderContentDAO.Order.CampaignDiscount,
                },
            }).FirstOrDefaultAsync();
            return OrderContent;
        }

        public async Task<bool> Create(OrderContent OrderContent)
        {
            OrderContentDAO OrderContentDAO = new OrderContentDAO();
            
            OrderContentDAO.Id = OrderContent.Id;
            OrderContentDAO.OrderId = OrderContent.OrderId;
            OrderContentDAO.ItemName = OrderContent.ItemName;
            OrderContentDAO.FirstVersion = OrderContent.FirstVersion;
            OrderContentDAO.SecondVersion = OrderContent.SecondVersion;
            OrderContentDAO.ThirdVersion = OrderContent.ThirdVersion;
            OrderContentDAO.Price = OrderContent.Price;
            OrderContentDAO.DiscountPrice = OrderContent.DiscountPrice;
            
            await DataContext.OrderContent.AddAsync(OrderContentDAO);
            await DataContext.SaveChangesAsync();
            OrderContent.Id = OrderContentDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(OrderContent OrderContent)
        {
            OrderContentDAO OrderContentDAO = DataContext.OrderContent.Where(x => x.Id == OrderContent.Id).FirstOrDefault();
            
            OrderContentDAO.Id = OrderContent.Id;
            OrderContentDAO.OrderId = OrderContent.OrderId;
            OrderContentDAO.ItemName = OrderContent.ItemName;
            OrderContentDAO.FirstVersion = OrderContent.FirstVersion;
            OrderContentDAO.SecondVersion = OrderContent.SecondVersion;
            OrderContentDAO.ThirdVersion = OrderContent.ThirdVersion;
            OrderContentDAO.Price = OrderContent.Price;
            OrderContentDAO.DiscountPrice = OrderContent.DiscountPrice;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(OrderContent OrderContent)
        {
            OrderContentDAO OrderContentDAO = await DataContext.OrderContent.Where(x => x.Id == OrderContent.Id).FirstOrDefaultAsync();
            DataContext.OrderContent.Remove(OrderContentDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
