
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
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public WarehouseRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<WarehouseDAO> DynamicFilter(IQueryable<WarehouseDAO> query, WarehouseFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.Email != null)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.PartnerId != null)
                query = query.Where(q => q.PartnerId, filter.PartnerId);
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
                        case WarehouseOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case WarehouseOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case WarehouseOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case WarehouseOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case WarehouseOrder.Partner:
                            query = query.OrderBy(q => q.Partner.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case WarehouseOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case WarehouseOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case WarehouseOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case WarehouseOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case WarehouseOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case WarehouseOrder.Partner:
                            query = query.OrderByDescending(q => q.Partner.Id);
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
                Name = filter.Selects.Contains(WarehouseSelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(WarehouseSelect.Phone) ? q.Phone : default(string),
                Email = filter.Selects.Contains(WarehouseSelect.Email) ? q.Email : default(string),
                Address = filter.Selects.Contains(WarehouseSelect.Address) ? q.Address : default(string),
                PartnerId = filter.Selects.Contains(WarehouseSelect.Partner) ? q.PartnerId : default(long),
                Partner = filter.Selects.Contains(WarehouseSelect.Partner) && q.Partner != null ? new Partner
                {
                    
                    Id = q.Partner.Id,
                    Name = q.Partner.Name,
                    Phone = q.Partner.Phone,
                    ContactPerson = q.Partner.ContactPerson,
                    Address = q.Partner.Address,
                } : null,
            }).ToListAsync();
            return Warehouses;
        }

        public async Task<int> Count(WarehouseFilter filter)
        {
            IQueryable <WarehouseDAO> WarehouseDAOs = DataContext.Warehouse;
            WarehouseDAOs = DynamicFilter(WarehouseDAOs, filter);
            return await WarehouseDAOs.CountAsync();
        }

        public async Task<List<Warehouse>> List(WarehouseFilter filter)
        {
            if (filter == null) return new List<Warehouse>();
            IQueryable<WarehouseDAO> WarehouseDAOs = DataContext.Warehouse;
            WarehouseDAOs = DynamicFilter(WarehouseDAOs, filter);
            WarehouseDAOs = DynamicOrder(WarehouseDAOs, filter);
            var Warehouses = await DynamicSelect(WarehouseDAOs, filter);
            return Warehouses;
        }

        
        public async Task<Warehouse> Get(long Id)
        {
            Warehouse Warehouse = await DataContext.Warehouse.Where(x => x.Id == Id).Select(WarehouseDAO => new Warehouse()
            {
                 
                Id = WarehouseDAO.Id,
                Name = WarehouseDAO.Name,
                Phone = WarehouseDAO.Phone,
                Email = WarehouseDAO.Email,
                Address = WarehouseDAO.Address,
                PartnerId = WarehouseDAO.PartnerId,
                Partner = WarehouseDAO.Partner == null ? null : new Partner
                {
                    
                    Id = WarehouseDAO.Partner.Id,
                    Name = WarehouseDAO.Partner.Name,
                    Phone = WarehouseDAO.Partner.Phone,
                    ContactPerson = WarehouseDAO.Partner.ContactPerson,
                    Address = WarehouseDAO.Partner.Address,
                },
            }).FirstOrDefaultAsync();
            return Warehouse;
        }

        public async Task<bool> Create(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = new WarehouseDAO();
            
            WarehouseDAO.Id = Warehouse.Id;
            WarehouseDAO.Name = Warehouse.Name;
            WarehouseDAO.Phone = Warehouse.Phone;
            WarehouseDAO.Email = Warehouse.Email;
            WarehouseDAO.Address = Warehouse.Address;
            WarehouseDAO.PartnerId = Warehouse.PartnerId;
            
            await DataContext.Warehouse.AddAsync(WarehouseDAO);
            await DataContext.SaveChangesAsync();
            Warehouse.Id = WarehouseDAO.Id;
            return true;
        }

        
        
        public async Task<bool> Update(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = DataContext.Warehouse.Where(x => x.Id == Warehouse.Id).FirstOrDefault();
            
            WarehouseDAO.Id = Warehouse.Id;
            WarehouseDAO.Name = Warehouse.Name;
            WarehouseDAO.Phone = Warehouse.Phone;
            WarehouseDAO.Email = Warehouse.Email;
            WarehouseDAO.Address = Warehouse.Address;
            WarehouseDAO.PartnerId = Warehouse.PartnerId;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Warehouse Warehouse)
        {
            WarehouseDAO WarehouseDAO = await DataContext.Warehouse.Where(x => x.Id == Warehouse.Id).FirstOrDefaultAsync();
            DataContext.Warehouse.Remove(WarehouseDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
