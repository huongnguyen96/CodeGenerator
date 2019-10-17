
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
    public interface IItemUnitOfMeasureRepository
    {
        Task<int> Count(ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter);
        Task<List<ItemUnitOfMeasure>> List(ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter);
        Task<ItemUnitOfMeasure> Get(long Id);
        Task<bool> Create(ItemUnitOfMeasure ItemUnitOfMeasure);
        Task<bool> Update(ItemUnitOfMeasure ItemUnitOfMeasure);
        Task<bool> Delete(ItemUnitOfMeasure ItemUnitOfMeasure);
        
    }
    public class ItemUnitOfMeasureRepository : IItemUnitOfMeasureRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ItemUnitOfMeasureRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemUnitOfMeasureDAO> DynamicFilter(IQueryable<ItemUnitOfMeasureDAO> query, ItemUnitOfMeasureFilter filter)
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
        private IQueryable<ItemUnitOfMeasureDAO> DynamicOrder(IQueryable<ItemUnitOfMeasureDAO> query,  ItemUnitOfMeasureFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemUnitOfMeasureOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ItemUnitOfMeasureOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ItemUnitOfMeasureOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemUnitOfMeasureOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ItemUnitOfMeasureOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ItemUnitOfMeasureOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ItemUnitOfMeasure>> DynamicSelect(IQueryable<ItemUnitOfMeasureDAO> query, ItemUnitOfMeasureFilter filter)
        {
            List <ItemUnitOfMeasure> ItemUnitOfMeasures = await query.Select(q => new ItemUnitOfMeasure()
            {
                
                Id = filter.Selects.Contains(ItemUnitOfMeasureSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ItemUnitOfMeasureSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ItemUnitOfMeasureSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ItemUnitOfMeasures;
        }

        public async Task<int> Count(ItemUnitOfMeasureFilter filter)
        {
            IQueryable <ItemUnitOfMeasureDAO> ItemUnitOfMeasureDAOs = DataContext.ItemUnitOfMeasure;
            ItemUnitOfMeasureDAOs = DynamicFilter(ItemUnitOfMeasureDAOs, filter);
            return await ItemUnitOfMeasureDAOs.CountAsync();
        }

        public async Task<List<ItemUnitOfMeasure>> List(ItemUnitOfMeasureFilter filter)
        {
            if (filter == null) return new List<ItemUnitOfMeasure>();
            IQueryable<ItemUnitOfMeasureDAO> ItemUnitOfMeasureDAOs = DataContext.ItemUnitOfMeasure;
            ItemUnitOfMeasureDAOs = DynamicFilter(ItemUnitOfMeasureDAOs, filter);
            ItemUnitOfMeasureDAOs = DynamicOrder(ItemUnitOfMeasureDAOs, filter);
            var ItemUnitOfMeasures = await DynamicSelect(ItemUnitOfMeasureDAOs, filter);
            return ItemUnitOfMeasures;
        }

        
        public async Task<ItemUnitOfMeasure> Get(long Id)
        {
            ItemUnitOfMeasure ItemUnitOfMeasure = await DataContext.ItemUnitOfMeasure.Where(x => x.Id == Id).Select(ItemUnitOfMeasureDAO => new ItemUnitOfMeasure()
            {
                 
                Id = ItemUnitOfMeasureDAO.Id,
                Code = ItemUnitOfMeasureDAO.Code,
                Name = ItemUnitOfMeasureDAO.Name,
            }).FirstOrDefaultAsync();
            return ItemUnitOfMeasure;
        }

        public async Task<bool> Create(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            ItemUnitOfMeasureDAO ItemUnitOfMeasureDAO = new ItemUnitOfMeasureDAO();
            
            ItemUnitOfMeasureDAO.Id = ItemUnitOfMeasure.Id;
            ItemUnitOfMeasureDAO.Code = ItemUnitOfMeasure.Code;
            ItemUnitOfMeasureDAO.Name = ItemUnitOfMeasure.Name;
            
            await DataContext.ItemUnitOfMeasure.AddAsync(ItemUnitOfMeasureDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            ItemUnitOfMeasureDAO ItemUnitOfMeasureDAO = DataContext.ItemUnitOfMeasure.Where(x => x.Id == ItemUnitOfMeasure.Id).FirstOrDefault();
            
            ItemUnitOfMeasureDAO.Id = ItemUnitOfMeasure.Id;
            ItemUnitOfMeasureDAO.Code = ItemUnitOfMeasure.Code;
            ItemUnitOfMeasureDAO.Name = ItemUnitOfMeasure.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            ItemUnitOfMeasureDAO ItemUnitOfMeasureDAO = await DataContext.ItemUnitOfMeasure.Where(x => x.Id == ItemUnitOfMeasure.Id).FirstOrDefaultAsync();
            DataContext.ItemUnitOfMeasure.Remove(ItemUnitOfMeasureDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
