
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
    public interface ICategory_ItemRepository
    {
        Task<int> Count(Category_ItemFilter Category_ItemFilter);
        Task<List<Category_Item>> List(Category_ItemFilter Category_ItemFilter);
        Task<Category_Item> Get(long CategoryId, long ItemId);
        Task<bool> Create(Category_Item Category_Item);
        Task<bool> Update(Category_Item Category_Item);
        Task<bool> Delete(Category_Item Category_Item);
        
    }
    public class Category_ItemRepository : ICategory_ItemRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public Category_ItemRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Category_ItemDAO> DynamicFilter(IQueryable<Category_ItemDAO> query, Category_ItemFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.CategoryId != null)
                query = query.Where(q => q.CategoryId, filter.CategoryId);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            return query;
        }
        private IQueryable<Category_ItemDAO> DynamicOrder(IQueryable<Category_ItemDAO> query,  Category_ItemFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case Category_ItemOrder.Category:
                            query = query.OrderBy(q => q.Category.Id);
                            break;
                        case Category_ItemOrder.Item:
                            query = query.OrderBy(q => q.Item.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case Category_ItemOrder.Category:
                            query = query.OrderByDescending(q => q.Category.Id);
                            break;
                        case Category_ItemOrder.Item:
                            query = query.OrderByDescending(q => q.Item.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Category_Item>> DynamicSelect(IQueryable<Category_ItemDAO> query, Category_ItemFilter filter)
        {
            List <Category_Item> Category_Items = await query.Select(q => new Category_Item()
            {
                
                CategoryId = filter.Selects.Contains(Category_ItemSelect.Category) ? q.CategoryId : default(long),
                ItemId = filter.Selects.Contains(Category_ItemSelect.Item) ? q.ItemId : default(long),
                Category = filter.Selects.Contains(Category_ItemSelect.Category) && q.Category != null ? new Category
                {
                    
                    Id = q.Category.Id,
                    Code = q.Category.Code,
                    Name = q.Category.Name,
                } : null,
                Item = filter.Selects.Contains(Category_ItemSelect.Item) && q.Item != null ? new Item
                {
                    
                    Id = q.Item.Id,
                    Code = q.Item.Code,
                    Name = q.Item.Name,
                    SKU = q.Item.SKU,
                    TypeId = q.Item.TypeId,
                    PurchasePrice = q.Item.PurchasePrice,
                    SalePrice = q.Item.SalePrice,
                    Description = q.Item.Description,
                    StatusId = q.Item.StatusId,
                    UnitOfMeasureId = q.Item.UnitOfMeasureId,
                    SupplierId = q.Item.SupplierId,
                } : null,
            }).ToListAsync();
            return Category_Items;
        }

        public async Task<int> Count(Category_ItemFilter filter)
        {
            IQueryable <Category_ItemDAO> Category_ItemDAOs = DataContext.Category_Item;
            Category_ItemDAOs = DynamicFilter(Category_ItemDAOs, filter);
            return await Category_ItemDAOs.CountAsync();
        }

        public async Task<List<Category_Item>> List(Category_ItemFilter filter)
        {
            if (filter == null) return new List<Category_Item>();
            IQueryable<Category_ItemDAO> Category_ItemDAOs = DataContext.Category_Item;
            Category_ItemDAOs = DynamicFilter(Category_ItemDAOs, filter);
            Category_ItemDAOs = DynamicOrder(Category_ItemDAOs, filter);
            var Category_Items = await DynamicSelect(Category_ItemDAOs, filter);
            return Category_Items;
        }

        
        public async Task<Category_Item> Get(long CategoryId, long ItemId)
        {
            Category_Item Category_Item = await DataContext.Category_Item.Where(x => x.CategoryId == CategoryId && x.ItemId == ItemId).Select(Category_ItemDAO => new Category_Item()
            {
                 
                CategoryId = Category_ItemDAO.CategoryId,
                ItemId = Category_ItemDAO.ItemId,
                Category = Category_ItemDAO.Category == null ? null : new Category
                {
                    
                    Id = Category_ItemDAO.Category.Id,
                    Code = Category_ItemDAO.Category.Code,
                    Name = Category_ItemDAO.Category.Name,
                },
                Item = Category_ItemDAO.Item == null ? null : new Item
                {
                    
                    Id = Category_ItemDAO.Item.Id,
                    Code = Category_ItemDAO.Item.Code,
                    Name = Category_ItemDAO.Item.Name,
                    SKU = Category_ItemDAO.Item.SKU,
                    TypeId = Category_ItemDAO.Item.TypeId,
                    PurchasePrice = Category_ItemDAO.Item.PurchasePrice,
                    SalePrice = Category_ItemDAO.Item.SalePrice,
                    Description = Category_ItemDAO.Item.Description,
                    StatusId = Category_ItemDAO.Item.StatusId,
                    UnitOfMeasureId = Category_ItemDAO.Item.UnitOfMeasureId,
                    SupplierId = Category_ItemDAO.Item.SupplierId,
                },
            }).FirstOrDefaultAsync();
            return Category_Item;
        }

        public async Task<bool> Create(Category_Item Category_Item)
        {
            Category_ItemDAO Category_ItemDAO = new Category_ItemDAO();
            
            Category_ItemDAO.CategoryId = Category_Item.CategoryId;
            Category_ItemDAO.ItemId = Category_Item.ItemId;
            
            await DataContext.Category_Item.AddAsync(Category_ItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Category_Item Category_Item)
        {
            Category_ItemDAO Category_ItemDAO = DataContext.Category_Item.Where(x => x.CategoryId == Category_Item.CategoryId && x.ItemId == Category_Item.ItemId).FirstOrDefault();
            
            Category_ItemDAO.CategoryId = Category_Item.CategoryId;
            Category_ItemDAO.ItemId = Category_Item.ItemId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Category_Item Category_Item)
        {
            Category_ItemDAO Category_ItemDAO = await DataContext.Category_Item.Where(x => x.CategoryId == Category_Item.CategoryId && x.ItemId == Category_Item.ItemId).FirstOrDefaultAsync();
            DataContext.Category_Item.Remove(Category_ItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
