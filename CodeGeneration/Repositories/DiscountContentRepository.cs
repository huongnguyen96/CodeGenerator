
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
    public interface IDiscountContentRepository
    {
        Task<int> Count(DiscountContentFilter DiscountContentFilter);
        Task<List<DiscountContent>> List(DiscountContentFilter DiscountContentFilter);
        Task<DiscountContent> Get(long Id);
        Task<bool> Create(DiscountContent DiscountContent);
        Task<bool> Update(DiscountContent DiscountContent);
        Task<bool> Delete(DiscountContent DiscountContent);
        
    }
    public class DiscountContentRepository : IDiscountContentRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public DiscountContentRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<DiscountContentDAO> DynamicFilter(IQueryable<DiscountContentDAO> query, DiscountContentFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.DiscountValue != null)
                query = query.Where(q => q.DiscountValue, filter.DiscountValue);
            if (filter.DiscountId != null)
                query = query.Where(q => q.DiscountId, filter.DiscountId);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<DiscountContentDAO> DynamicOrder(IQueryable<DiscountContentDAO> query,  DiscountContentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountContentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DiscountContentOrder.Item:
                            query = query.OrderBy(q => q.Item.Id);
                            break;
                        case DiscountContentOrder.DiscountValue:
                            query = query.OrderBy(q => q.DiscountValue);
                            break;
                        case DiscountContentOrder.Discount:
                            query = query.OrderBy(q => q.Discount.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountContentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DiscountContentOrder.Item:
                            query = query.OrderByDescending(q => q.Item.Id);
                            break;
                        case DiscountContentOrder.DiscountValue:
                            query = query.OrderByDescending(q => q.DiscountValue);
                            break;
                        case DiscountContentOrder.Discount:
                            query = query.OrderByDescending(q => q.Discount.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<DiscountContent>> DynamicSelect(IQueryable<DiscountContentDAO> query, DiscountContentFilter filter)
        {
            List <DiscountContent> DiscountContents = await query.Select(q => new DiscountContent()
            {
                
                Id = filter.Selects.Contains(DiscountContentSelect.Id) ? q.Id : default(long),
                ItemId = filter.Selects.Contains(DiscountContentSelect.Item) ? q.ItemId : default(long),
                DiscountValue = filter.Selects.Contains(DiscountContentSelect.DiscountValue) ? q.DiscountValue : default(long),
                DiscountId = filter.Selects.Contains(DiscountContentSelect.Discount) ? q.DiscountId : default(long),
                Discount = filter.Selects.Contains(DiscountContentSelect.Discount) && q.Discount != null ? new Discount
                {
                    
                    Id = q.Discount.Id,
                    Name = q.Discount.Name,
                    Start = q.Discount.Start,
                    End = q.Discount.End,
                    Type = q.Discount.Type,
                } : null,
                Item = filter.Selects.Contains(DiscountContentSelect.Item) && q.Item != null ? new Item
                {
                    
                    Id = q.Item.Id,
                    ProductId = q.Item.ProductId,
                    FirstVariationId = q.Item.FirstVariationId,
                    SecondVariationId = q.Item.SecondVariationId,
                    SKU = q.Item.SKU,
                    Price = q.Item.Price,
                    MinPrice = q.Item.MinPrice,
                } : null,
            }).ToListAsync();
            return DiscountContents;
        }

        public async Task<int> Count(DiscountContentFilter filter)
        {
            IQueryable <DiscountContentDAO> DiscountContentDAOs = DataContext.DiscountContent;
            DiscountContentDAOs = DynamicFilter(DiscountContentDAOs, filter);
            return await DiscountContentDAOs.CountAsync();
        }

        public async Task<List<DiscountContent>> List(DiscountContentFilter filter)
        {
            if (filter == null) return new List<DiscountContent>();
            IQueryable<DiscountContentDAO> DiscountContentDAOs = DataContext.DiscountContent;
            DiscountContentDAOs = DynamicFilter(DiscountContentDAOs, filter);
            DiscountContentDAOs = DynamicOrder(DiscountContentDAOs, filter);
            var DiscountContents = await DynamicSelect(DiscountContentDAOs, filter);
            return DiscountContents;
        }

        
        public async Task<DiscountContent> Get(long Id)
        {
            DiscountContent DiscountContent = await DataContext.DiscountContent.Where(x => x.Id == Id).Select(DiscountContentDAO => new DiscountContent()
            {
                 
                Id = DiscountContentDAO.Id,
                ItemId = DiscountContentDAO.ItemId,
                DiscountValue = DiscountContentDAO.DiscountValue,
                DiscountId = DiscountContentDAO.DiscountId,
                Discount = DiscountContentDAO.Discount == null ? null : new Discount
                {
                    
                    Id = DiscountContentDAO.Discount.Id,
                    Name = DiscountContentDAO.Discount.Name,
                    Start = DiscountContentDAO.Discount.Start,
                    End = DiscountContentDAO.Discount.End,
                    Type = DiscountContentDAO.Discount.Type,
                },
                Item = DiscountContentDAO.Item == null ? null : new Item
                {
                    
                    Id = DiscountContentDAO.Item.Id,
                    ProductId = DiscountContentDAO.Item.ProductId,
                    FirstVariationId = DiscountContentDAO.Item.FirstVariationId,
                    SecondVariationId = DiscountContentDAO.Item.SecondVariationId,
                    SKU = DiscountContentDAO.Item.SKU,
                    Price = DiscountContentDAO.Item.Price,
                    MinPrice = DiscountContentDAO.Item.MinPrice,
                },
            }).FirstOrDefaultAsync();
            return DiscountContent;
        }

        public async Task<bool> Create(DiscountContent DiscountContent)
        {
            DiscountContentDAO DiscountContentDAO = new DiscountContentDAO();
            
            DiscountContentDAO.Id = DiscountContent.Id;
            DiscountContentDAO.ItemId = DiscountContent.ItemId;
            DiscountContentDAO.DiscountValue = DiscountContent.DiscountValue;
            DiscountContentDAO.DiscountId = DiscountContent.DiscountId;
            
            await DataContext.DiscountContent.AddAsync(DiscountContentDAO);
            await DataContext.SaveChangesAsync();
            DiscountContent.Id = DiscountContentDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(DiscountContent DiscountContent)
        {
            DiscountContentDAO DiscountContentDAO = DataContext.DiscountContent.Where(x => x.Id == DiscountContent.Id).FirstOrDefault();
            
            DiscountContentDAO.Id = DiscountContent.Id;
            DiscountContentDAO.ItemId = DiscountContent.ItemId;
            DiscountContentDAO.DiscountValue = DiscountContent.DiscountValue;
            DiscountContentDAO.DiscountId = DiscountContent.DiscountId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(DiscountContent DiscountContent)
        {
            DiscountContentDAO DiscountContentDAO = await DataContext.DiscountContent.Where(x => x.Id == DiscountContent.Id).FirstOrDefaultAsync();
            DataContext.DiscountContent.Remove(DiscountContentDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
