
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
    public interface IStockRepository
    {
        Task<int> Count(StockFilter StockFilter);
        Task<List<Stock>> List(StockFilter StockFilter);
        Task<Stock> Get(long Id);
        Task<bool> Create(Stock Stock);
        Task<bool> Update(Stock Stock);
        Task<bool> Delete(Stock Stock);
        
    }
    public class StockRepository : IStockRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public StockRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<StockDAO> DynamicFilter(IQueryable<StockDAO> query, StockFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.WarehouseId != null)
                query = query.Where(q => q.WarehouseId, filter.WarehouseId);
            if (filter.Quantity != null)
                query = query.Where(q => q.Quantity, filter.Quantity);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<StockDAO> DynamicOrder(IQueryable<StockDAO> query,  StockFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case StockOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StockOrder.Item:
                            query = query.OrderBy(q => q.Item.Id);
                            break;
                        case StockOrder.Warehouse:
                            query = query.OrderBy(q => q.Warehouse.Id);
                            break;
                        case StockOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case StockOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StockOrder.Item:
                            query = query.OrderByDescending(q => q.Item.Id);
                            break;
                        case StockOrder.Warehouse:
                            query = query.OrderByDescending(q => q.Warehouse.Id);
                            break;
                        case StockOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Stock>> DynamicSelect(IQueryable<StockDAO> query, StockFilter filter)
        {
            List <Stock> Stocks = await query.Select(q => new Stock()
            {
                
                Id = filter.Selects.Contains(StockSelect.Id) ? q.Id : default(long),
                ItemId = filter.Selects.Contains(StockSelect.Item) ? q.ItemId : default(long),
                WarehouseId = filter.Selects.Contains(StockSelect.Warehouse) ? q.WarehouseId : default(long),
                Quantity = filter.Selects.Contains(StockSelect.Quantity) ? q.Quantity : default(long),
                Item = filter.Selects.Contains(StockSelect.Item) && q.Item != null ? new Item
                {
                    
                    Id = q.Item.Id,
                    ProductId = q.Item.ProductId,
                    FirstVariationId = q.Item.FirstVariationId,
                    SecondVariationId = q.Item.SecondVariationId,
                    SKU = q.Item.SKU,
                    Price = q.Item.Price,
                    MinPrice = q.Item.MinPrice,
                } : null,
                Warehouse = filter.Selects.Contains(StockSelect.Warehouse) && q.Warehouse != null ? new Warehouse
                {
                    
                    Id = q.Warehouse.Id,
                    Name = q.Warehouse.Name,
                    Phone = q.Warehouse.Phone,
                    Email = q.Warehouse.Email,
                    Address = q.Warehouse.Address,
                    PartnerId = q.Warehouse.PartnerId,
                } : null,
            }).ToListAsync();
            return Stocks;
        }

        public async Task<int> Count(StockFilter filter)
        {
            IQueryable <StockDAO> StockDAOs = DataContext.Stock;
            StockDAOs = DynamicFilter(StockDAOs, filter);
            return await StockDAOs.CountAsync();
        }

        public async Task<List<Stock>> List(StockFilter filter)
        {
            if (filter == null) return new List<Stock>();
            IQueryable<StockDAO> StockDAOs = DataContext.Stock;
            StockDAOs = DynamicFilter(StockDAOs, filter);
            StockDAOs = DynamicOrder(StockDAOs, filter);
            var Stocks = await DynamicSelect(StockDAOs, filter);
            return Stocks;
        }

        
        public async Task<Stock> Get(long Id)
        {
            Stock Stock = await DataContext.Stock.Where(x => x.Id == Id).Select(StockDAO => new Stock()
            {
                 
                Id = StockDAO.Id,
                ItemId = StockDAO.ItemId,
                WarehouseId = StockDAO.WarehouseId,
                Quantity = StockDAO.Quantity,
                Item = StockDAO.Item == null ? null : new Item
                {
                    
                    Id = StockDAO.Item.Id,
                    ProductId = StockDAO.Item.ProductId,
                    FirstVariationId = StockDAO.Item.FirstVariationId,
                    SecondVariationId = StockDAO.Item.SecondVariationId,
                    SKU = StockDAO.Item.SKU,
                    Price = StockDAO.Item.Price,
                    MinPrice = StockDAO.Item.MinPrice,
                },
                Warehouse = StockDAO.Warehouse == null ? null : new Warehouse
                {
                    
                    Id = StockDAO.Warehouse.Id,
                    Name = StockDAO.Warehouse.Name,
                    Phone = StockDAO.Warehouse.Phone,
                    Email = StockDAO.Warehouse.Email,
                    Address = StockDAO.Warehouse.Address,
                    PartnerId = StockDAO.Warehouse.PartnerId,
                },
            }).FirstOrDefaultAsync();
            return Stock;
        }

        public async Task<bool> Create(Stock Stock)
        {
            StockDAO StockDAO = new StockDAO();
            
            StockDAO.Id = Stock.Id;
            StockDAO.ItemId = Stock.ItemId;
            StockDAO.WarehouseId = Stock.WarehouseId;
            StockDAO.Quantity = Stock.Quantity;
            
            await DataContext.Stock.AddAsync(StockDAO);
            await DataContext.SaveChangesAsync();
            Stock.Id = StockDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Stock Stock)
        {
            StockDAO StockDAO = DataContext.Stock.Where(x => x.Id == Stock.Id).FirstOrDefault();
            
            StockDAO.Id = Stock.Id;
            StockDAO.ItemId = Stock.ItemId;
            StockDAO.WarehouseId = Stock.WarehouseId;
            StockDAO.Quantity = Stock.Quantity;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Stock Stock)
        {
            StockDAO StockDAO = await DataContext.Stock.Where(x => x.Id == Stock.Id).FirstOrDefaultAsync();
            DataContext.Stock.Remove(StockDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
