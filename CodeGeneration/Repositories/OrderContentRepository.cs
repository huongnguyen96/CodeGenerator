
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
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.ProductName != null)
                query = query.Where(q => q.ProductName, filter.ProductName);
            if (filter.FirstVersion != null)
                query = query.Where(q => q.FirstVersion, filter.FirstVersion);
            if (filter.SecondVersion != null)
                query = query.Where(q => q.SecondVersion, filter.SecondVersion);
            if (filter.Price != null)
                query = query.Where(q => q.Price, filter.Price);
            if (filter.DiscountPrice != null)
                query = query.Where(q => q.DiscountPrice, filter.DiscountPrice);
            if (filter.Quantity != null)
                query = query.Where(q => q.Quantity, filter.Quantity);
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
                        case OrderContentOrder.Item:
                            query = query.OrderBy(q => q.Item.Id);
                            break;
                        case OrderContentOrder.ProductName:
                            query = query.OrderBy(q => q.ProductName);
                            break;
                        case OrderContentOrder.FirstVersion:
                            query = query.OrderBy(q => q.FirstVersion);
                            break;
                        case OrderContentOrder.SecondVersion:
                            query = query.OrderBy(q => q.SecondVersion);
                            break;
                        case OrderContentOrder.Price:
                            query = query.OrderBy(q => q.Price);
                            break;
                        case OrderContentOrder.DiscountPrice:
                            query = query.OrderBy(q => q.DiscountPrice);
                            break;
                        case OrderContentOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
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
                        case OrderContentOrder.Item:
                            query = query.OrderByDescending(q => q.Item.Id);
                            break;
                        case OrderContentOrder.ProductName:
                            query = query.OrderByDescending(q => q.ProductName);
                            break;
                        case OrderContentOrder.FirstVersion:
                            query = query.OrderByDescending(q => q.FirstVersion);
                            break;
                        case OrderContentOrder.SecondVersion:
                            query = query.OrderByDescending(q => q.SecondVersion);
                            break;
                        case OrderContentOrder.Price:
                            query = query.OrderByDescending(q => q.Price);
                            break;
                        case OrderContentOrder.DiscountPrice:
                            query = query.OrderByDescending(q => q.DiscountPrice);
                            break;
                        case OrderContentOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
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
                ItemId = filter.Selects.Contains(OrderContentSelect.Item) ? q.ItemId : default(long?),
                ProductName = filter.Selects.Contains(OrderContentSelect.ProductName) ? q.ProductName : default(string),
                FirstVersion = filter.Selects.Contains(OrderContentSelect.FirstVersion) ? q.FirstVersion : default(string),
                SecondVersion = filter.Selects.Contains(OrderContentSelect.SecondVersion) ? q.SecondVersion : default(string),
                Price = filter.Selects.Contains(OrderContentSelect.Price) ? q.Price : default(long),
                DiscountPrice = filter.Selects.Contains(OrderContentSelect.DiscountPrice) ? q.DiscountPrice : default(long),
                Quantity = filter.Selects.Contains(OrderContentSelect.Quantity) ? q.Quantity : default(long),
                Item = filter.Selects.Contains(OrderContentSelect.Item) && q.Item != null ? new Item
                {
                    
                    Id = q.Item.Id,
                    ProductId = q.Item.ProductId,
                    FirstVariationId = q.Item.FirstVariationId,
                    SecondVariationId = q.Item.SecondVariationId,
                    SKU = q.Item.SKU,
                    Price = q.Item.Price,
                    MinPrice = q.Item.MinPrice,
                } : null,
                Order = filter.Selects.Contains(OrderContentSelect.Order) && q.Order != null ? new Order
                {
                    
                    Id = q.Order.Id,
                    CustomerId = q.Order.CustomerId,
                    CreatedDate = q.Order.CreatedDate,
                    VoucherCode = q.Order.VoucherCode,
                    Total = q.Order.Total,
                    VoucherDiscount = q.Order.VoucherDiscount,
                    CampaignDiscount = q.Order.CampaignDiscount,
                    StatusId = q.Order.StatusId,
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
                ItemId = OrderContentDAO.ItemId,
                ProductName = OrderContentDAO.ProductName,
                FirstVersion = OrderContentDAO.FirstVersion,
                SecondVersion = OrderContentDAO.SecondVersion,
                Price = OrderContentDAO.Price,
                DiscountPrice = OrderContentDAO.DiscountPrice,
                Quantity = OrderContentDAO.Quantity,
                Item = OrderContentDAO.Item == null ? null : new Item
                {
                    
                    Id = OrderContentDAO.Item.Id,
                    ProductId = OrderContentDAO.Item.ProductId,
                    FirstVariationId = OrderContentDAO.Item.FirstVariationId,
                    SecondVariationId = OrderContentDAO.Item.SecondVariationId,
                    SKU = OrderContentDAO.Item.SKU,
                    Price = OrderContentDAO.Item.Price,
                    MinPrice = OrderContentDAO.Item.MinPrice,
                },
                Order = OrderContentDAO.Order == null ? null : new Order
                {
                    
                    Id = OrderContentDAO.Order.Id,
                    CustomerId = OrderContentDAO.Order.CustomerId,
                    CreatedDate = OrderContentDAO.Order.CreatedDate,
                    VoucherCode = OrderContentDAO.Order.VoucherCode,
                    Total = OrderContentDAO.Order.Total,
                    VoucherDiscount = OrderContentDAO.Order.VoucherDiscount,
                    CampaignDiscount = OrderContentDAO.Order.CampaignDiscount,
                    StatusId = OrderContentDAO.Order.StatusId,
                },
            }).FirstOrDefaultAsync();
            return OrderContent;
        }

        public async Task<bool> Create(OrderContent OrderContent)
        {
            OrderContentDAO OrderContentDAO = new OrderContentDAO();
            
            OrderContentDAO.Id = OrderContent.Id;
            OrderContentDAO.OrderId = OrderContent.OrderId;
            OrderContentDAO.ItemId = OrderContent.ItemId;
            OrderContentDAO.ProductName = OrderContent.ProductName;
            OrderContentDAO.FirstVersion = OrderContent.FirstVersion;
            OrderContentDAO.SecondVersion = OrderContent.SecondVersion;
            OrderContentDAO.Price = OrderContent.Price;
            OrderContentDAO.DiscountPrice = OrderContent.DiscountPrice;
            OrderContentDAO.Quantity = OrderContent.Quantity;
            
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
            OrderContentDAO.ItemId = OrderContent.ItemId;
            OrderContentDAO.ProductName = OrderContent.ProductName;
            OrderContentDAO.FirstVersion = OrderContent.FirstVersion;
            OrderContentDAO.SecondVersion = OrderContent.SecondVersion;
            OrderContentDAO.Price = OrderContent.Price;
            OrderContentDAO.DiscountPrice = OrderContent.DiscountPrice;
            OrderContentDAO.Quantity = OrderContent.Quantity;
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
