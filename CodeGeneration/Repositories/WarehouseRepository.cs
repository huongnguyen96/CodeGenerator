
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
    public interface IWarehouseRepository
    {
        Task<int> Count(WarehouseFilter WarehouseFilter);
        Task<List<Warehouse>> List(WarehouseFilter WarehouseFilter);
        Task<Warehouse> Get(long Id);
        Task<bool> Create(Warehouse Warehouse);
        Task<bool> Update(Warehouse Warehouse);
        Task<bool> Delete(Warehouse Warehouse);
        
    }
    public class WarehouseRepository : IWarehouseRepository
    {
        private WGContext WGContext;
        private ICurrentContext CurrentContext;
        public WarehouseRepository(WGContext WGContext, ICurrentContext CurrentContext)
        {
            this.WGContext = WGContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<WarehouseDAO> DynamicFilter(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ManagerId != null)
                query = query.Where(q => q.ManagerId, filter.ManagerId);
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
        private IQueryable<WarehouseDAO> DynamicOrder(IQueryable<WarehouseDAO> query,  WarehouseFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case WarehouseOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case WarehouseOrder.Manager:
                            query = query.OrderBy(q => q.Manager.Id);
                            break;
                        case WarehouseOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case WarehouseOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case WarehouseOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case WarehouseOrder.Manager:
                            query = query.OrderByDescending(q => q.Manager.Id);
                            break;
                        case WarehouseOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case WarehouseOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Warehouse>> DynamicSelect(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            List <Warehouse> Warehouses = await query.Select(q => new Warehouse()
            {
                
                Id = filter.Selects.Contains(WarehouseSelect.Id) ? q.Id : default(long),
                ManagerId = filter.Selects.Contains(WarehouseSelect.Manager) ? q.ManagerId : default(long),
                Code = filter.Selects.Contains(WarehouseSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(WarehouseSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Warehouses;
        }

        public async Task<int> Count(WarehouseFilter filter)
        {
            IQueryable <WarehouseDAO> WarehouseDAOs = WGContext.Warehouse;
            WarehouseDAOs = DynamicFilter(WarehouseDAOs, filter);
            return await WarehouseDAOs.CountAsync();
        }

        public async Task<List<Warehouse>> List(WarehouseFilter filter)
        {
            if (filter == null) return new List<Warehouse>();
            IQueryable<WarehouseDAO> WarehouseDAOs = WGContext.Warehouse;
            WarehouseDAOs = DynamicFilter(WarehouseDAOs, filter);
            WarehouseDAOs = DynamicOrder(WarehouseDAOs, filter);
            var Warehouses = await DynamicSelect(WarehouseDAOs, filter);
            return Warehouses;
        }

        
        public async Task<Warehouse> Get(long Id)
        {
            Warehouse Warehouse = await WGContext.Warehouse.Where(x => x.Id == Id).Select(WarehouseDAO => new Warehouse()
            {
                 
                Id = WarehouseDAO.Id,
                ManagerId = WarehouseDAO.ManagerId,
                Code = WarehouseDAO.Code,
                Name = WarehouseDAO.Name,
            }).FirstOrDefaultAsync();
            return Warehouse;
        }

        public async Task<bool> Create(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = new WarehouseDAO();
            
            WarehouseDAO.Id = Warehouse.Id;
            WarehouseDAO.ManagerId = Warehouse.ManagerId;
            WarehouseDAO.Code = Warehouse.Code;
            WarehouseDAO.Name = Warehouse.Name;
            
            await WGContext.Warehouse.AddAsync(WarehouseDAO);
            await WGContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = WGContext.Warehouse.Where(x => x.Id == Warehouse.Id).FirstOrDefault();
            
            WarehouseDAO.Id = Warehouse.Id;
            WarehouseDAO.ManagerId = Warehouse.ManagerId;
            WarehouseDAO.Code = Warehouse.Code;
            WarehouseDAO.Name = Warehouse.Name;
            await WGContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = await WGContext.Warehouse.Where(x => x.Id == Warehouse.Id).FirstOrDefaultAsync();
            WGContext.Warehouse.Remove(WarehouseDAO);
            await WGContext.SaveChangesAsync();
            return true;
        }

    }
}
