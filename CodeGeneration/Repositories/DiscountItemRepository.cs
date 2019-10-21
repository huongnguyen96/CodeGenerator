
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
    public interface IDiscountItemRepository
    {
        Task<int> Count(DiscountItemFilter DiscountItemFilter);
        Task<List<DiscountItem>> List(DiscountItemFilter DiscountItemFilter);
        Task<DiscountItem> Get(long Id);
        Task<bool> Create(DiscountItem DiscountItem);
        Task<bool> Update(DiscountItem DiscountItem);
        Task<bool> Delete(DiscountItem DiscountItem);
        
    }
    public class DiscountItemRepository : IDiscountItemRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public DiscountItemRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<DiscountItemDAO> DynamicFilter(IQueryable<DiscountItemDAO> query, DiscountItemFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.UnitId != null)
                query = query.Where(q => q.UnitId, filter.UnitId);
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
        private IQueryable<DiscountItemDAO> DynamicOrder(IQueryable<DiscountItemDAO> query,  DiscountItemFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountItemOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DiscountItemOrder.Unit:
                            query = query.OrderBy(q => q.Unit.Id);
                            break;
                        case DiscountItemOrder.DiscountValue:
                            query = query.OrderBy(q => q.DiscountValue);
                            break;
                        case DiscountItemOrder.Discount:
                            query = query.OrderBy(q => q.Discount.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case DiscountItemOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DiscountItemOrder.Unit:
                            query = query.OrderByDescending(q => q.Unit.Id);
                            break;
                        case DiscountItemOrder.DiscountValue:
                            query = query.OrderByDescending(q => q.DiscountValue);
                            break;
                        case DiscountItemOrder.Discount:
                            query = query.OrderByDescending(q => q.Discount.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<DiscountItem>> DynamicSelect(IQueryable<DiscountItemDAO> query, DiscountItemFilter filter)
        {
            List <DiscountItem> DiscountItems = await query.Select(q => new DiscountItem()
            {
                
                Id = filter.Selects.Contains(DiscountItemSelect.Id) ? q.Id : default(long),
                UnitId = filter.Selects.Contains(DiscountItemSelect.Unit) ? q.UnitId : default(long),
                DiscountValue = filter.Selects.Contains(DiscountItemSelect.DiscountValue) ? q.DiscountValue : default(long),
                DiscountId = filter.Selects.Contains(DiscountItemSelect.Discount) ? q.DiscountId : default(long),
                Discount = filter.Selects.Contains(DiscountItemSelect.Discount) && q.Discount != null ? new Discount
                {
                    
                    Id = q.Discount.Id,
                    Name = q.Discount.Name,
                    Start = q.Discount.Start,
                    End = q.Discount.End,
                    Type = q.Discount.Type,
                } : null,
                Unit = filter.Selects.Contains(DiscountItemSelect.Unit) && q.Unit != null ? new Unit
                {
                    
                    Id = q.Unit.Id,
                    FirstVariationId = q.Unit.FirstVariationId,
                    SecondVariationId = q.Unit.SecondVariationId,
                    ThirdVariationId = q.Unit.ThirdVariationId,
                    SKU = q.Unit.SKU,
                    Price = q.Unit.Price,
                } : null,
            }).ToListAsync();
            return DiscountItems;
        }

        public async Task<int> Count(DiscountItemFilter filter)
        {
            IQueryable <DiscountItemDAO> DiscountItemDAOs = DataContext.DiscountItem;
            DiscountItemDAOs = DynamicFilter(DiscountItemDAOs, filter);
            return await DiscountItemDAOs.CountAsync();
        }

        public async Task<List<DiscountItem>> List(DiscountItemFilter filter)
        {
            if (filter == null) return new List<DiscountItem>();
            IQueryable<DiscountItemDAO> DiscountItemDAOs = DataContext.DiscountItem;
            DiscountItemDAOs = DynamicFilter(DiscountItemDAOs, filter);
            DiscountItemDAOs = DynamicOrder(DiscountItemDAOs, filter);
            var DiscountItems = await DynamicSelect(DiscountItemDAOs, filter);
            return DiscountItems;
        }

        
        public async Task<DiscountItem> Get(long Id)
        {
            DiscountItem DiscountItem = await DataContext.DiscountItem.Where(x => x.Id == Id).Select(DiscountItemDAO => new DiscountItem()
            {
                 
                Id = DiscountItemDAO.Id,
                UnitId = DiscountItemDAO.UnitId,
                DiscountValue = DiscountItemDAO.DiscountValue,
                DiscountId = DiscountItemDAO.DiscountId,
                Discount = DiscountItemDAO.Discount == null ? null : new Discount
                {
                    
                    Id = DiscountItemDAO.Discount.Id,
                    Name = DiscountItemDAO.Discount.Name,
                    Start = DiscountItemDAO.Discount.Start,
                    End = DiscountItemDAO.Discount.End,
                    Type = DiscountItemDAO.Discount.Type,
                },
                Unit = DiscountItemDAO.Unit == null ? null : new Unit
                {
                    
                    Id = DiscountItemDAO.Unit.Id,
                    FirstVariationId = DiscountItemDAO.Unit.FirstVariationId,
                    SecondVariationId = DiscountItemDAO.Unit.SecondVariationId,
                    ThirdVariationId = DiscountItemDAO.Unit.ThirdVariationId,
                    SKU = DiscountItemDAO.Unit.SKU,
                    Price = DiscountItemDAO.Unit.Price,
                },
            }).FirstOrDefaultAsync();
            return DiscountItem;
        }

        public async Task<bool> Create(DiscountItem DiscountItem)
        {
            DiscountItemDAO DiscountItemDAO = new DiscountItemDAO();
            
            DiscountItemDAO.Id = DiscountItem.Id;
            DiscountItemDAO.UnitId = DiscountItem.UnitId;
            DiscountItemDAO.DiscountValue = DiscountItem.DiscountValue;
            DiscountItemDAO.DiscountId = DiscountItem.DiscountId;
            
            await DataContext.DiscountItem.AddAsync(DiscountItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(DiscountItem DiscountItem)
        {
            DiscountItemDAO DiscountItemDAO = DataContext.DiscountItem.Where(x => x.Id == DiscountItem.Id).FirstOrDefault();
            
            DiscountItemDAO.Id = DiscountItem.Id;
            DiscountItemDAO.UnitId = DiscountItem.UnitId;
            DiscountItemDAO.DiscountValue = DiscountItem.DiscountValue;
            DiscountItemDAO.DiscountId = DiscountItem.DiscountId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(DiscountItem DiscountItem)
        {
            DiscountItemDAO DiscountItemDAO = await DataContext.DiscountItem.Where(x => x.Id == DiscountItem.Id).FirstOrDefaultAsync();
            DataContext.DiscountItem.Remove(DiscountItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
