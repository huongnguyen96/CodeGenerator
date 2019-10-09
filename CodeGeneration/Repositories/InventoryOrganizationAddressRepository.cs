
using Common;
using ERP.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    public interface IInventoryOrganizationAddressRepository
    {
        Task<int> Count(InventoryOrganizationAddressFilter InventoryOrganizationAddressFilter);
        Task<List<InventoryOrganizationAddress>> List(InventoryOrganizationAddressFilter InventoryOrganizationAddressFilter);
        Task<InventoryOrganizationAddress> Get(Guid Id);
        Task<bool> Create(InventoryOrganizationAddress InventoryOrganizationAddress);
        Task<bool> Update(InventoryOrganizationAddress InventoryOrganizationAddress);
        Task<bool> Delete(Guid Id);
        
    }
    public class InventoryOrganizationAddressRepository : IInventoryOrganizationAddressRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public InventoryOrganizationAddressRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<InventoryOrganizationAddressDAO> DynamicFilter(IQueryable<InventoryOrganizationAddressDAO> query, InventoryOrganizationAddressFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.InventoryOrganizationId != null)
                query = query.Where(q => q.InventoryOrganizationId, filter.InventoryOrganizationId);
            return query;
        }
        private IQueryable<InventoryOrganizationAddressDAO> DynamicOrder(IQueryable<InventoryOrganizationAddressDAO> query,  InventoryOrganizationAddressFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case InventoryOrganizationAddressOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case InventoryOrganizationAddressOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case InventoryOrganizationAddressOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case InventoryOrganizationAddressOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<InventoryOrganizationAddress>> DynamicSelect(IQueryable<InventoryOrganizationAddressDAO> query, InventoryOrganizationAddressFilter filter)
        {
            List <InventoryOrganizationAddress> InventoryOrganizationAddresss = await query.Select(q => new InventoryOrganizationAddress()
            {
                
                Id = filter.Selects.Contains(InventoryOrganizationAddressSelect.Id) ? q.Id : default(Guid),
                Name = filter.Selects.Contains(InventoryOrganizationAddressSelect.Name) ? q.Name : default(string),
                Address = filter.Selects.Contains(InventoryOrganizationAddressSelect.Address) ? q.Address : default(string),
                InventoryOrganizationId = filter.Selects.Contains(InventoryOrganizationAddressSelect.InventoryOrganization) ? q.InventoryOrganizationId : default(Guid),
            }).ToListAsync();
            return InventoryOrganizationAddresss;
        }

        public async Task<int> Count(InventoryOrganizationAddressFilter filter)
        {
            IQueryable <InventoryOrganizationAddressDAO> InventoryOrganizationAddressDAOs = ERPContext.InventoryOrganizationAddress;
            InventoryOrganizationAddressDAOs = DynamicFilter(InventoryOrganizationAddressDAOs, filter);
            return await InventoryOrganizationAddressDAOs.CountAsync();
        }

        public async Task<List<InventoryOrganizationAddress>> List(InventoryOrganizationAddressFilter filter)
        {
            if (filter == null) return new List<InventoryOrganizationAddress>();
            IQueryable<InventoryOrganizationAddressDAO> InventoryOrganizationAddressDAOs = ERPContext.InventoryOrganizationAddress;
            InventoryOrganizationAddressDAOs = DynamicFilter(InventoryOrganizationAddressDAOs, filter);
            InventoryOrganizationAddressDAOs = DynamicOrder(InventoryOrganizationAddressDAOs, filter);
            var InventoryOrganizationAddresss = await DynamicSelect(InventoryOrganizationAddressDAOs, filter);
            return InventoryOrganizationAddresss;
        }

        public async Task<InventoryOrganizationAddress> Get(Guid Id)
        {
            InventoryOrganizationAddress InventoryOrganizationAddress = await ERPContext.InventoryOrganizationAddress.Where(l => l.Id == Id).Select(InventoryOrganizationAddressDAO => new InventoryOrganizationAddress()
            {
                 
                Id = InventoryOrganizationAddressDAO.Id,
                Name = InventoryOrganizationAddressDAO.Name,
                Address = InventoryOrganizationAddressDAO.Address,
                InventoryOrganizationId = InventoryOrganizationAddressDAO.InventoryOrganizationId,
            }).FirstOrDefaultAsync();
            return InventoryOrganizationAddress;
        }

        public async Task<bool> Create(InventoryOrganizationAddress InventoryOrganizationAddress)
        {
            InventoryOrganizationAddressDAO InventoryOrganizationAddressDAO = new InventoryOrganizationAddressDAO();
            
            InventoryOrganizationAddressDAO.Id = InventoryOrganizationAddress.Id;
            InventoryOrganizationAddressDAO.Name = InventoryOrganizationAddress.Name;
            InventoryOrganizationAddressDAO.Address = InventoryOrganizationAddress.Address;
            InventoryOrganizationAddressDAO.InventoryOrganizationId = InventoryOrganizationAddress.InventoryOrganizationId;
            InventoryOrganizationAddressDAO.Disabled = false;
            
            await ERPContext.InventoryOrganizationAddress.AddAsync(InventoryOrganizationAddressDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(InventoryOrganizationAddress InventoryOrganizationAddress)
        {
            InventoryOrganizationAddressDAO InventoryOrganizationAddressDAO = ERPContext.InventoryOrganizationAddress.Where(b => b.Id == InventoryOrganizationAddress.Id).FirstOrDefault();
            
            InventoryOrganizationAddressDAO.Id = InventoryOrganizationAddress.Id;
            InventoryOrganizationAddressDAO.Name = InventoryOrganizationAddress.Name;
            InventoryOrganizationAddressDAO.Address = InventoryOrganizationAddress.Address;
            InventoryOrganizationAddressDAO.InventoryOrganizationId = InventoryOrganizationAddress.InventoryOrganizationId;
            InventoryOrganizationAddressDAO.Disabled = false;
            ERPContext.InventoryOrganizationAddress.Update(InventoryOrganizationAddressDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            InventoryOrganizationAddressDAO InventoryOrganizationAddressDAO = await ERPContext.InventoryOrganizationAddress.Where(x => x.Id == Id).FirstOrDefaultAsync();
            InventoryOrganizationAddressDAO.Disabled = true;
            ERPContext.InventoryOrganizationAddress.Update(InventoryOrganizationAddressDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
