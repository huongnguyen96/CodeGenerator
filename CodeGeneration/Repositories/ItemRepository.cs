
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
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ItemRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
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
            if (filter.SKU != null)
                query = query.Where(q => q.SKU, filter.SKU);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.TypeId != null)
                query = query.Where(q => q.TypeId, filter.TypeId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.PartnerId != null)
                query = query.Where(q => q.PartnerId, filter.PartnerId);
            if (filter.CategoryId != null)
                query = query.Where(q => q.CategoryId, filter.CategoryId);
            if (filter.BrandId != null)
                query = query.Where(q => q.BrandId, filter.BrandId);
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
                        case ItemOrder.SKU:
                            query = query.OrderBy(q => q.SKU);
                            break;
                        case ItemOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ItemOrder.Type:
                            query = query.OrderBy(q => q.Type.Id);
                            break;
                        case ItemOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                        case ItemOrder.Partner:
                            query = query.OrderBy(q => q.Partner.Id);
                            break;
                        case ItemOrder.Category:
                            query = query.OrderBy(q => q.Category.Id);
                            break;
                        case ItemOrder.Brand:
                            query = query.OrderBy(q => q.Brand.Id);
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
                        case ItemOrder.SKU:
                            query = query.OrderByDescending(q => q.SKU);
                            break;
                        case ItemOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ItemOrder.Type:
                            query = query.OrderByDescending(q => q.Type.Id);
                            break;
                        case ItemOrder.Status:
                            query = query.OrderByDescending(q => q.Status.Id);
                            break;
                        case ItemOrder.Partner:
                            query = query.OrderByDescending(q => q.Partner.Id);
                            break;
                        case ItemOrder.Category:
                            query = query.OrderByDescending(q => q.Category.Id);
                            break;
                        case ItemOrder.Brand:
                            query = query.OrderByDescending(q => q.Brand.Id);
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
                SKU = filter.Selects.Contains(ItemSelect.SKU) ? q.SKU : default(string),
                Description = filter.Selects.Contains(ItemSelect.Description) ? q.Description : default(string),
                TypeId = filter.Selects.Contains(ItemSelect.Type) ? q.TypeId : default(long),
                StatusId = filter.Selects.Contains(ItemSelect.Status) ? q.StatusId : default(long),
                PartnerId = filter.Selects.Contains(ItemSelect.Partner) ? q.PartnerId : default(long),
                CategoryId = filter.Selects.Contains(ItemSelect.Category) ? q.CategoryId : default(long),
                BrandId = filter.Selects.Contains(ItemSelect.Brand) ? q.BrandId : default(long),
                Brand = filter.Selects.Contains(ItemSelect.Brand) && q.Brand != null ? new Brand
                {
                    
                    Id = q.Brand.Id,
                    Name = q.Brand.Name,
                    CategoryId = q.Brand.CategoryId,
                } : null,
                Category = filter.Selects.Contains(ItemSelect.Category) && q.Category != null ? new Category
                {
                    
                    Id = q.Category.Id,
                    Code = q.Category.Code,
                    Name = q.Category.Name,
                    ParentId = q.Category.ParentId,
                    Icon = q.Category.Icon,
                } : null,
                Partner = filter.Selects.Contains(ItemSelect.Partner) && q.Partner != null ? new Partner
                {
                    
                    Id = q.Partner.Id,
                    Name = q.Partner.Name,
                    Phone = q.Partner.Phone,
                    ContactPerson = q.Partner.ContactPerson,
                    Address = q.Partner.Address,
                } : null,
                Status = filter.Selects.Contains(ItemSelect.Status) && q.Status != null ? new ItemStatus
                {
                    
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Type = filter.Selects.Contains(ItemSelect.Type) && q.Type != null ? new ItemType
                {
                    
                    Id = q.Type.Id,
                    Code = q.Type.Code,
                    Name = q.Type.Name,
                } : null,
            }).ToListAsync();
            return Items;
        }

        public async Task<int> Count(ItemFilter filter)
        {
            IQueryable <ItemDAO> ItemDAOs = DataContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            return await ItemDAOs.CountAsync();
        }

        public async Task<List<Item>> List(ItemFilter filter)
        {
            if (filter == null) return new List<Item>();
            IQueryable<ItemDAO> ItemDAOs = DataContext.Item;
            ItemDAOs = DynamicFilter(ItemDAOs, filter);
            ItemDAOs = DynamicOrder(ItemDAOs, filter);
            var Items = await DynamicSelect(ItemDAOs, filter);
            return Items;
        }

        
        public async Task<Item> Get(long Id)
        {
            Item Item = await DataContext.Item.Where(x => x.Id == Id).Select(ItemDAO => new Item()
            {
                 
                Id = ItemDAO.Id,
                Code = ItemDAO.Code,
                Name = ItemDAO.Name,
                SKU = ItemDAO.SKU,
                Description = ItemDAO.Description,
                TypeId = ItemDAO.TypeId,
                StatusId = ItemDAO.StatusId,
                PartnerId = ItemDAO.PartnerId,
                CategoryId = ItemDAO.CategoryId,
                BrandId = ItemDAO.BrandId,
                Brand = ItemDAO.Brand == null ? null : new Brand
                {
                    
                    Id = ItemDAO.Brand.Id,
                    Name = ItemDAO.Brand.Name,
                    CategoryId = ItemDAO.Brand.CategoryId,
                },
                Category = ItemDAO.Category == null ? null : new Category
                {
                    
                    Id = ItemDAO.Category.Id,
                    Code = ItemDAO.Category.Code,
                    Name = ItemDAO.Category.Name,
                    ParentId = ItemDAO.Category.ParentId,
                    Icon = ItemDAO.Category.Icon,
                },
                Partner = ItemDAO.Partner == null ? null : new Partner
                {
                    
                    Id = ItemDAO.Partner.Id,
                    Name = ItemDAO.Partner.Name,
                    Phone = ItemDAO.Partner.Phone,
                    ContactPerson = ItemDAO.Partner.ContactPerson,
                    Address = ItemDAO.Partner.Address,
                },
                Status = ItemDAO.Status == null ? null : new ItemStatus
                {
                    
                    Id = ItemDAO.Status.Id,
                    Code = ItemDAO.Status.Code,
                    Name = ItemDAO.Status.Name,
                },
                Type = ItemDAO.Type == null ? null : new ItemType
                {
                    
                    Id = ItemDAO.Type.Id,
                    Code = ItemDAO.Type.Code,
                    Name = ItemDAO.Type.Name,
                },
            }).FirstOrDefaultAsync();
            return Item;
        }

        public async Task<bool> Create(Item Item)
        {
            ItemDAO ItemDAO = new ItemDAO();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            ItemDAO.SKU = Item.SKU;
            ItemDAO.Description = Item.Description;
            ItemDAO.TypeId = Item.TypeId;
            ItemDAO.StatusId = Item.StatusId;
            ItemDAO.PartnerId = Item.PartnerId;
            ItemDAO.CategoryId = Item.CategoryId;
            ItemDAO.BrandId = Item.BrandId;
            
            await DataContext.Item.AddAsync(ItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Item Item)
        {
            ItemDAO ItemDAO = DataContext.Item.Where(x => x.Id == Item.Id).FirstOrDefault();
            
            ItemDAO.Id = Item.Id;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            ItemDAO.SKU = Item.SKU;
            ItemDAO.Description = Item.Description;
            ItemDAO.TypeId = Item.TypeId;
            ItemDAO.StatusId = Item.StatusId;
            ItemDAO.PartnerId = Item.PartnerId;
            ItemDAO.CategoryId = Item.CategoryId;
            ItemDAO.BrandId = Item.BrandId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Item Item)
        {
            ItemDAO ItemDAO = await DataContext.Item.Where(x => x.Id == Item.Id).FirstOrDefaultAsync();
            DataContext.Item.Remove(ItemDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
