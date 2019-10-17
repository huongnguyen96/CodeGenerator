
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
    public interface IItemTypeRepository
    {
        Task<int> Count(ItemTypeFilter ItemTypeFilter);
        Task<List<ItemType>> List(ItemTypeFilter ItemTypeFilter);
        Task<ItemType> Get(long Id);
        Task<bool> Create(ItemType ItemType);
        Task<bool> Update(ItemType ItemType);
        Task<bool> Delete(ItemType ItemType);
        
    }
    public class ItemTypeRepository : IItemTypeRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ItemTypeRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemTypeDAO> DynamicFilter(IQueryable<ItemTypeDAO> query, ItemTypeFilter filter)
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
        private IQueryable<ItemTypeDAO> DynamicOrder(IQueryable<ItemTypeDAO> query,  ItemTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ItemTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ItemTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ItemTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ItemTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ItemType>> DynamicSelect(IQueryable<ItemTypeDAO> query, ItemTypeFilter filter)
        {
            List <ItemType> ItemTypes = await query.Select(q => new ItemType()
            {
                
                Id = filter.Selects.Contains(ItemTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ItemTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ItemTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ItemTypes;
        }

        public async Task<int> Count(ItemTypeFilter filter)
        {
            IQueryable <ItemTypeDAO> ItemTypeDAOs = DataContext.ItemType;
            ItemTypeDAOs = DynamicFilter(ItemTypeDAOs, filter);
            return await ItemTypeDAOs.CountAsync();
        }

        public async Task<List<ItemType>> List(ItemTypeFilter filter)
        {
            if (filter == null) return new List<ItemType>();
            IQueryable<ItemTypeDAO> ItemTypeDAOs = DataContext.ItemType;
            ItemTypeDAOs = DynamicFilter(ItemTypeDAOs, filter);
            ItemTypeDAOs = DynamicOrder(ItemTypeDAOs, filter);
            var ItemTypes = await DynamicSelect(ItemTypeDAOs, filter);
            return ItemTypes;
        }

        
        public async Task<ItemType> Get(long Id)
        {
            ItemType ItemType = await DataContext.ItemType.Where(x => x.Id == Id).Select(ItemTypeDAO => new ItemType()
            {
                 
                Id = ItemTypeDAO.Id,
                Code = ItemTypeDAO.Code,
                Name = ItemTypeDAO.Name,
            }).FirstOrDefaultAsync();
            return ItemType;
        }

        public async Task<bool> Create(ItemType ItemType)
        {
            ItemTypeDAO ItemTypeDAO = new ItemTypeDAO();
            
            ItemTypeDAO.Id = ItemType.Id;
            ItemTypeDAO.Code = ItemType.Code;
            ItemTypeDAO.Name = ItemType.Name;
            
            await DataContext.ItemType.AddAsync(ItemTypeDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(ItemType ItemType)
        {
            ItemTypeDAO ItemTypeDAO = DataContext.ItemType.Where(x => x.Id == ItemType.Id).FirstOrDefault();
            
            ItemTypeDAO.Id = ItemType.Id;
            ItemTypeDAO.Code = ItemType.Code;
            ItemTypeDAO.Name = ItemType.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ItemType ItemType)
        {
            ItemTypeDAO ItemTypeDAO = await DataContext.ItemType.Where(x => x.Id == ItemType.Id).FirstOrDefaultAsync();
            DataContext.ItemType.Remove(ItemTypeDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
