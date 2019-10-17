
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
    public interface IItemStockRepository
    {
        Task<int> Count(ItemStockFilter ItemStockFilter);
        Task<List<ItemStock>> List(ItemStockFilter ItemStockFilter);
        Task<ItemStock> Get(long Id);
        Task<bool> Create(ItemStock ItemStock);
        Task<bool> Update(ItemStock ItemStock);
        Task<bool> Delete(ItemStock ItemStock);
        
    }
    public class ItemStockRepository : IItemStockRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ItemStockRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ItemStockDAO> DynamicFilter(IQueryable<ItemStockDAO> query, ItemStockFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.WarehouseId != null)
                query = query.Where(q => q.WarehouseId, filter.WarehouseId);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Quantity != null)
                query = query.Where(q => q.Quantity, filter.Quantity);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ItemStockDAO> DynamicOrder(IQueryable<ItemStockDAO> query,  ItemStockFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemStockOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ItemStockOrder.Item:
                            query = query.OrderBy(q => q.Item.Id);
                            break;
                        case ItemStockOrder.Warehouse:
                            query = query.OrderBy(q => q.Warehouse.Id);
                            break;
                        case ItemStockOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasure.Id);
                            break;
                        case ItemStockOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ItemStockOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ItemStockOrder.Item:
                            query = query.OrderByDescending(q => q.Item.Id);
                            break;
                        case ItemStockOrder.Warehouse:
                            query = query.OrderByDescending(q => q.Warehouse.Id);
                            break;
                        case ItemStockOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasure.Id);
                            break;
                        case ItemStockOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ItemStock>> DynamicSelect(IQueryable<ItemStockDAO> query, ItemStockFilter filter)
        {
            List <ItemStock> ItemStocks = await query.Select(q => new ItemStock()
            {
                
                Id = filter.Selects.Contains(ItemStockSelect.Id) ? q.Id : default(long),
                ItemId = filter.Selects.Contains(ItemStockSelect.Item) ? q.ItemId : default(long),
                WarehouseId = filter.Selects.Contains(ItemStockSelect.Warehouse) ? q.WarehouseId : default(long),
                UnitOfMeasureId = filter.Selects.Contains(ItemStockSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                Quantity = filter.Selects.Contains(ItemStockSelect.Quantity) ? q.Quantity : default(decimal),
                Item = filter.Selects.Contains(ItemStockSelect.Item) && q.Item != null ? new Item
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
                UnitOfMeasure = filter.Selects.Contains(ItemStockSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new ItemUnitOfMeasure
                {
                    
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
                } : null,
                Warehouse = filter.Selects.Contains(ItemStockSelect.Warehouse) && q.Warehouse != null ? new Warehouse
                {
                    
                    Id = q.Warehouse.Id,
                    Name = q.Warehouse.Name,
                    Phone = q.Warehouse.Phone,
                    Email = q.Warehouse.Email,
                    Address = q.Warehouse.Address,
                    SupplierId = q.Warehouse.SupplierId,
                } : null,
            }).ToListAsync();
            return ItemStocks;
        }

        public async Task<int> Count(ItemStockFilter filter)
        {
            IQueryable <ItemStockDAO> ItemStockDAOs = DataContext.ItemStock;
            ItemStockDAOs = DynamicFilter(ItemStockDAOs, filter);
            return await ItemStockDAOs.CountAsync();
        }

        public async Task<List<ItemStock>> List(ItemStockFilter filter)
        {
            if (filter == null) return new List<ItemStock>();
            IQueryable<ItemStockDAO> ItemStockDAOs = DataContext.ItemStock;
            ItemStockDAOs = DynamicFilter(ItemStockDAOs, filter);
            ItemStockDAOs = DynamicOrder(ItemStockDAOs, filter);
            var ItemStocks = await DynamicSelect(ItemStockDAOs, filter);
            return ItemStocks;
        }

        
        public async Task<ItemStock> Get(long Id)
        {
            ItemStock ItemStock = await DataContext.ItemStock.Where(x => x.Id == Id).Select(ItemStockDAO => new ItemStock()
            {
                 
                Id = ItemStockDAO.Id,
                ItemId = ItemStockDAO.ItemId,
                WarehouseId = ItemStockDAO.WarehouseId,
                UnitOfMeasureId = ItemStockDAO.UnitOfMeasureId,
                Quantity = ItemStockDAO.Quantity,
                Item = ItemStockDAO.Item == null ? null : new Item
                {
                    
                    Id = ItemStockDAO.Item.Id,
                    Code = ItemStockDAO.Item.Code,
                    Name = ItemStockDAO.Item.Name,
                    SKU = ItemStockDAO.Item.SKU,
                    TypeId = ItemStockDAO.Item.TypeId,
                    PurchasePrice = ItemStockDAO.Item.PurchasePrice,
                    SalePrice = ItemStockDAO.Item.SalePrice,
                    Description = ItemStockDAO.Item.Description,
                    StatusId = ItemStockDAO.Item.StatusId,
                    UnitOfMeasureId = ItemStockDAO.Item.UnitOfMeasureId,
                    SupplierId = ItemStockDAO.Item.SupplierId,
                },
                UnitOfMeasure = ItemStockDAO.UnitOfMeasure == null ? null : new ItemUnitOfMeasure
                {
                    
                    Id = ItemStockDAO.UnitOfMeasure.Id,
                    Code = ItemStockDAO.UnitOfMeasure.Code,
                    Name = ItemStockDAO.UnitOfMeasure.Name,
                },
                Warehouse = ItemStockDAO.Warehouse == null ? null : new Warehouse
                {
                    
                    Id = ItemStockDAO.Warehouse.Id,
                    Name = ItemStockDAO.Warehouse.Name,
                    Phone = ItemStockDAO.Warehouse.Phone,
                    Email = ItemStockDAO.Warehouse.Email,
                    Address = ItemStockDAO.Warehouse.Address,
                    SupplierId = ItemStockDAO.Warehouse.SupplierId,
                },
            }).FirstOrDefaultAsync();
            return ItemStock;
        }

        public async Task<bool> Create(ItemStock ItemStock)
        {
            ItemStockDAO ItemStockDAO = new ItemStockDAO();
            
            ItemStockDAO.Id = ItemStock.Id;
            ItemStockDAO.ItemId = ItemStock.ItemId;
            ItemStockDAO.WarehouseId = ItemStock.WarehouseId;
            ItemStockDAO.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            ItemStockDAO.Quantity = ItemStock.Quantity;
            
            await DataContext.ItemStock.AddAsync(ItemStockDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(ItemStock ItemStock)
        {
            ItemStockDAO ItemStockDAO = DataContext.ItemStock.Where(x => x.Id == ItemStock.Id).FirstOrDefault();
            
            ItemStockDAO.Id = ItemStock.Id;
            ItemStockDAO.ItemId = ItemStock.ItemId;
            ItemStockDAO.WarehouseId = ItemStock.WarehouseId;
            ItemStockDAO.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            ItemStockDAO.Quantity = ItemStock.Quantity;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ItemStock ItemStock)
        {
            ItemStockDAO ItemStockDAO = await DataContext.ItemStock.Where(x => x.Id == ItemStock.Id).FirstOrDefaultAsync();
            DataContext.ItemStock.Remove(ItemStockDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
