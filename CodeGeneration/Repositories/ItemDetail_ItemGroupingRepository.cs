
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
    public interface IItemDetail_ItemGroupingRepository
    {
        Task<int> Count(ItemDetail_ItemGroupingFilter ItemDetail_ItemGroupingFilter);
        Task<List<ItemDetail_ItemGrouping>> List(ItemDetail_ItemGroupingFilter ItemDetail_ItemGroupingFilter);
        Task<ItemDetail_ItemGrouping> Get(Guid Id);
        Task<bool> Create(ItemDetail_ItemGrouping ItemDetail_ItemGrouping);
        Task<bool> Update(ItemDetail_ItemGrouping ItemDetail_ItemGrouping);
        Task<bool> Delete(Guid Id);
        
    }
    public class ItemDetail_ItemGroupingRepository : IItemDetail_ItemGroupingRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ItemDetail_ItemGroupingRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemDetail_ItemGroupingDAO> DynamicFilter(IQueryable<ItemDetail_ItemGroupingDAO> query, ItemDetail_ItemGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.ItemDetaiId != null)
                query = query.Where(q => q.ItemDetaiId, filter.ItemDetaiId);
            if (filter.ItemGroupingId != null)
                query = query.Where(q => q.ItemGroupingId, filter.ItemGroupingId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<ItemDetail_ItemGroupingDAO> DynamicOrder(IQueryable<ItemDetail_ItemGroupingDAO> query,  ItemDetail_ItemGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
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

        private async Task<List<ItemDetail_ItemGrouping>> DynamicSelect(IQueryable<ItemDetail_ItemGroupingDAO> query, ItemDetail_ItemGroupingFilter filter)
        {
            List <ItemDetail_ItemGrouping> ItemDetail_ItemGroupings = await query.Select(q => new ItemDetail_ItemGrouping()
            {
                
                ItemDetaiId = filter.Selects.Contains(ItemDetail_ItemGroupingSelect.ItemDetai) ? q.ItemDetaiId : default(Guid),
                ItemGroupingId = filter.Selects.Contains(ItemDetail_ItemGroupingSelect.ItemGrouping) ? q.ItemGroupingId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(ItemDetail_ItemGroupingSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return ItemDetail_ItemGroupings;
        }

        public async Task<int> Count(ItemDetail_ItemGroupingFilter filter)
        {
            IQueryable <ItemDetail_ItemGroupingDAO> ItemDetail_ItemGroupingDAOs = ERPContext.ItemDetail_ItemGrouping;
            ItemDetail_ItemGroupingDAOs = DynamicFilter(ItemDetail_ItemGroupingDAOs, filter);
            return await ItemDetail_ItemGroupingDAOs.CountAsync();
        }

        public async Task<List<ItemDetail_ItemGrouping>> List(ItemDetail_ItemGroupingFilter filter)
        {
            if (filter == null) return new List<ItemDetail_ItemGrouping>();
            IQueryable<ItemDetail_ItemGroupingDAO> ItemDetail_ItemGroupingDAOs = ERPContext.ItemDetail_ItemGrouping;
            ItemDetail_ItemGroupingDAOs = DynamicFilter(ItemDetail_ItemGroupingDAOs, filter);
            ItemDetail_ItemGroupingDAOs = DynamicOrder(ItemDetail_ItemGroupingDAOs, filter);
            var ItemDetail_ItemGroupings = await DynamicSelect(ItemDetail_ItemGroupingDAOs, filter);
            return ItemDetail_ItemGroupings;
        }

        public async Task<ItemDetail_ItemGrouping> Get(Guid Id)
        {
            ItemDetail_ItemGrouping ItemDetail_ItemGrouping = await ERPContext.ItemDetail_ItemGrouping.Where(l => l.Id == Id).Select(ItemDetail_ItemGroupingDAO => new ItemDetail_ItemGrouping()
            {
                 
                ItemDetaiId = ItemDetail_ItemGroupingDAO.ItemDetaiId,
                ItemGroupingId = ItemDetail_ItemGroupingDAO.ItemGroupingId,
                BusinessGroupId = ItemDetail_ItemGroupingDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return ItemDetail_ItemGrouping;
        }

        public async Task<bool> Create(ItemDetail_ItemGrouping ItemDetail_ItemGrouping)
        {
            ItemDetail_ItemGroupingDAO ItemDetail_ItemGroupingDAO = new ItemDetail_ItemGroupingDAO();
            
            ItemDetail_ItemGroupingDAO.ItemDetaiId = ItemDetail_ItemGrouping.ItemDetaiId;
            ItemDetail_ItemGroupingDAO.ItemGroupingId = ItemDetail_ItemGrouping.ItemGroupingId;
            ItemDetail_ItemGroupingDAO.BusinessGroupId = ItemDetail_ItemGrouping.BusinessGroupId;
            ItemDetail_ItemGroupingDAO.Disabled = false;
            
            await ERPContext.ItemDetail_ItemGrouping.AddAsync(ItemDetail_ItemGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ItemDetail_ItemGrouping ItemDetail_ItemGrouping)
        {
            ItemDetail_ItemGroupingDAO ItemDetail_ItemGroupingDAO = ERPContext.ItemDetail_ItemGrouping.Where(b => b.Id == ItemDetail_ItemGrouping.Id).FirstOrDefault();
            
            ItemDetail_ItemGroupingDAO.ItemDetaiId = ItemDetail_ItemGrouping.ItemDetaiId;
            ItemDetail_ItemGroupingDAO.ItemGroupingId = ItemDetail_ItemGrouping.ItemGroupingId;
            ItemDetail_ItemGroupingDAO.BusinessGroupId = ItemDetail_ItemGrouping.BusinessGroupId;
            ItemDetail_ItemGroupingDAO.Disabled = false;
            ERPContext.ItemDetail_ItemGrouping.Update(ItemDetail_ItemGroupingDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ItemDetail_ItemGroupingDAO ItemDetail_ItemGroupingDAO = await ERPContext.ItemDetail_ItemGrouping.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ItemDetail_ItemGroupingDAO.Disabled = true;
            ERPContext.ItemDetail_ItemGrouping.Update(ItemDetail_ItemGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
