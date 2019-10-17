
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
            if (filter.TypeId != null)
                query = query.Where(q => q.TypeId, filter.TypeId);
            if (filter.PurchasePrice != null)
                query = query.Where(q => q.PurchasePrice, filter.PurchasePrice);
            if (filter.SalePrice != null)
                query = query.Where(q => q.SalePrice, filter.SalePrice);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.SupplierId != null)
                query = query.Where(q => q.SupplierId, filter.SupplierId);
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
                        case ItemOrder.Type:
                            query = query.OrderBy(q => q.Type.Id);
                            break;
                        case ItemOrder.PurchasePrice:
                            query = query.OrderBy(q => q.PurchasePrice);
                            break;
                        case ItemOrder.SalePrice:
                            query = query.OrderBy(q => q.SalePrice);
                            break;
                        case ItemOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ItemOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                        case ItemOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasure.Id);
                            break;
                        case ItemOrder.Supplier:
                            query = query.OrderBy(q => q.Supplier.Id);
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
                        case ItemOrder.Type:
                            query = query.OrderByDescending(q => q.Type.Id);
                            break;
                        case ItemOrder.PurchasePrice:
                            query = query.OrderByDescending(q => q.PurchasePrice);
                            break;
                        case ItemOrder.SalePrice:
                            query = query.OrderByDescending(q => q.SalePrice);
                            break;
                        case ItemOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ItemOrder.Status:
                            query = query.OrderByDescending(q => q.Status.Id);
                            break;
                        case ItemOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasure.Id);
                            break;
                        case ItemOrder.Supplier:
                            query = query.OrderByDescending(q => q.Supplier.Id);
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
                TypeId = filter.Selects.Contains(ItemSelect.Type) ? q.TypeId : default(long),
                PurchasePrice = filter.Selects.Contains(ItemSelect.PurchasePrice) ? q.PurchasePrice : default(decimal?),
                SalePrice = filter.Selects.Contains(ItemSelect.SalePrice) ? q.SalePrice : default(decimal?),
                Description = filter.Selects.Contains(ItemSelect.Description) ? q.Description : default(string),
                StatusId = filter.Selects.Contains(ItemSelect.Status) ? q.StatusId : default(long?),
                UnitOfMeasureId = filter.Selects.Contains(ItemSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                SupplierId = filter.Selects.Contains(ItemSelect.Supplier) ? q.SupplierId : default(long),
                Status = filter.Selects.Contains(ItemSelect.Status) && q.Status != null ? new ItemStatus
                {
                    
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Supplier = filter.Selects.Contains(ItemSelect.Supplier) && q.Supplier != null ? new Supplier
                {
                    
                    Id = q.Supplier.Id,
                    Name = q.Supplier.Name,
                    Phone = q.Supplier.Phone,
                    ContactPerson = q.Supplier.ContactPerson,
                    Address = q.Supplier.Address,
                } : null,
                Type = filter.Selects.Contains(ItemSelect.Type) && q.Type != null ? new ItemType
                {
                    
                    Id = q.Type.Id,
                    Code = q.Type.Code,
                    Name = q.Type.Name,
                } : null,
                UnitOfMeasure = filter.Selects.Contains(ItemSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new ItemUnitOfMeasure
                {
                    
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
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
                TypeId = ItemDAO.TypeId,
                PurchasePrice = ItemDAO.PurchasePrice,
                SalePrice = ItemDAO.SalePrice,
                Description = ItemDAO.Description,
                StatusId = ItemDAO.StatusId,
                UnitOfMeasureId = ItemDAO.UnitOfMeasureId,
                SupplierId = ItemDAO.SupplierId,
                Status = ItemDAO.Status == null ? null : new ItemStatus
                {
                    
                    Id = ItemDAO.Status.Id,
                    Code = ItemDAO.Status.Code,
                    Name = ItemDAO.Status.Name,
                },
                Supplier = ItemDAO.Supplier == null ? null : new Supplier
                {
                    
                    Id = ItemDAO.Supplier.Id,
                    Name = ItemDAO.Supplier.Name,
                    Phone = ItemDAO.Supplier.Phone,
                    ContactPerson = ItemDAO.Supplier.ContactPerson,
                    Address = ItemDAO.Supplier.Address,
                },
                Type = ItemDAO.Type == null ? null : new ItemType
                {
                    
                    Id = ItemDAO.Type.Id,
                    Code = ItemDAO.Type.Code,
                    Name = ItemDAO.Type.Name,
                },
                UnitOfMeasure = ItemDAO.UnitOfMeasure == null ? null : new ItemUnitOfMeasure
                {
                    
                    Id = ItemDAO.UnitOfMeasure.Id,
                    Code = ItemDAO.UnitOfMeasure.Code,
                    Name = ItemDAO.UnitOfMeasure.Name,
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
            ItemDAO.TypeId = Item.TypeId;
            ItemDAO.PurchasePrice = Item.PurchasePrice;
            ItemDAO.SalePrice = Item.SalePrice;
            ItemDAO.Description = Item.Description;
            ItemDAO.StatusId = Item.StatusId;
            ItemDAO.UnitOfMeasureId = Item.UnitOfMeasureId;
            ItemDAO.SupplierId = Item.SupplierId;
            
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
            ItemDAO.TypeId = Item.TypeId;
            ItemDAO.PurchasePrice = Item.PurchasePrice;
            ItemDAO.SalePrice = Item.SalePrice;
            ItemDAO.Description = Item.Description;
            ItemDAO.StatusId = Item.StatusId;
            ItemDAO.UnitOfMeasureId = Item.UnitOfMeasureId;
            ItemDAO.SupplierId = Item.SupplierId;
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
