
using Common;
using WeGift.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeGift.Repositories
{
    public interface IItemRepository
    {
        Task<int> Count(ItemFilter ItemFilter);
        Task<List<Item>> List(ItemFilter ItemFilter);
        Task<Item> Get(long Id);
        Task<bool> Create(Item Item);
        Task<bool> Update(Item Item);
        Task<bool> Delete(Item Item);
        
    }
    public class ItemRepository : IItemRepository
    {
        private WGContext WGContext;
        private ICurrentContext CurrentContext;
        public ItemRepository(WGContext WGContext, ICurrentContext CurrentContext)
        {
            this.WGContext = WGContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemDAO> DynamicFilter(IQueryable<ItemDAO> query, ItemFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ItemDAO> DynamicOrder(IQueryable<ItemDAO> query,  ItemFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ItemOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ItemOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ItemOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ItemOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Item>> DynamicSelect(IQueryable<ItemDAO> query, ItemFilter filter)
        {
            List <Item> Items = await query.Select(q => new Item()
            {
                
                Id = filter.Selects.Contains(ItemSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ItemSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ItemSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Items;
        }

        public async Task<int> Count(ItemFilter filter)
        {
            IQueryable <ItemDAO> ItemDAOs = WGContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            return await ItemDAOs.CountAsync();
        }

        public async Task<List<Item>> List(ItemFilter filter)
        {
            if (filter == null) return new List<Item>();
            IQueryable<ItemDAO> ItemDAOs = WGContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            ItemDAOs = DynamicOrder(ItemDAOs, filter);
            var Items = await DynamicSelect(ItemDAOs, filter);
            return Items;
        }

        
        public async Task<Item> Get(long Id)
        {
            Item Item = await WGContext.Item.Where(x => x.Id == Id).Select(ItemDAO => new Item()
            {
                 
                Id = ItemDAO.Id,
                Code = ItemDAO.Code,
                Name = ItemDAO.Name,
            }).FirstOrDefaultAsync();
            return Item;
        }

        public async Task<bool> Create(Item Item)
        {
            ItemDAO ItemDAO = new ItemDAO();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            
            await WGContext.Item.AddAsync(ItemDAO);
            await WGContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Item Item)
        {
            ItemDAO ItemDAO = WGContext.Item.Where(x => x.Id == Item.Id).FirstOrDefault();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            await WGContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Item Item)
        {
            ItemDAO ItemDAO = await WGContext.Item.Where(x => x.Id == Item.Id).FirstOrDefaultAsync();
            WGContext.Item.Remove(ItemDAO);
            await WGContext.SaveChangesAsync();
            return true;
        }

    }
}
