
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
    public interface ISupplierRepository
    {
        Task<int> Count(SupplierFilter SupplierFilter);
        Task<List<Supplier>> List(SupplierFilter SupplierFilter);
        Task<Supplier> Get(long Id);
        Task<bool> Create(Supplier Supplier);
        Task<bool> Update(Supplier Supplier);
        Task<bool> Delete(Supplier Supplier);
        
    }
    public class SupplierRepository : ISupplierRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public SupplierRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierDAO> DynamicFilter(IQueryable<SupplierDAO> query, SupplierFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.ContactPerson != null)
                query = query.Where(q => q.ContactPerson, filter.ContactPerson);
            if (filter.Address != null)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<SupplierDAO> DynamicOrder(IQueryable<SupplierDAO> query,  SupplierFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SupplierOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SupplierOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case SupplierOrder.ContactPerson:
                            query = query.OrderBy(q => q.ContactPerson);
                            break;
                        case SupplierOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SupplierOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SupplierOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case SupplierOrder.ContactPerson:
                            query = query.OrderByDescending(q => q.ContactPerson);
                            break;
                        case SupplierOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Supplier>> DynamicSelect(IQueryable<SupplierDAO> query, SupplierFilter filter)
        {
            List <Supplier> Suppliers = await query.Select(q => new Supplier()
            {
                
                Id = filter.Selects.Contains(SupplierSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(SupplierSelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(SupplierSelect.Phone) ? q.Phone : default(string),
                ContactPerson = filter.Selects.Contains(SupplierSelect.ContactPerson) ? q.ContactPerson : default(string),
                Address = filter.Selects.Contains(SupplierSelect.Address) ? q.Address : default(string),
            }).ToListAsync();
            return Suppliers;
        }

        public async Task<int> Count(SupplierFilter filter)
        {
            IQueryable <SupplierDAO> SupplierDAOs = DataContext.Supplier;
            SupplierDAOs = DynamicFilter(SupplierDAOs, filter);
            return await SupplierDAOs.CountAsync();
        }

        public async Task<List<Supplier>> List(SupplierFilter filter)
        {
            if (filter == null) return new List<Supplier>();
            IQueryable<SupplierDAO> SupplierDAOs = DataContext.Supplier;
            SupplierDAOs = DynamicFilter(SupplierDAOs, filter);
            SupplierDAOs = DynamicOrder(SupplierDAOs, filter);
            var Suppliers = await DynamicSelect(SupplierDAOs, filter);
            return Suppliers;
        }

        
        public async Task<Supplier> Get(long Id)
        {
            Supplier Supplier = await DataContext.Supplier.Where(x => x.Id == Id).Select(SupplierDAO => new Supplier()
            {
                 
                Id = SupplierDAO.Id,
                Name = SupplierDAO.Name,
                Phone = SupplierDAO.Phone,
                ContactPerson = SupplierDAO.ContactPerson,
                Address = SupplierDAO.Address,
            }).FirstOrDefaultAsync();
            return Supplier;
        }

        public async Task<bool> Create(Supplier Supplier)
        {
            SupplierDAO SupplierDAO = new SupplierDAO();
            
            SupplierDAO.Id = Supplier.Id;
            SupplierDAO.Name = Supplier.Name;
            SupplierDAO.Phone = Supplier.Phone;
            SupplierDAO.ContactPerson = Supplier.ContactPerson;
            SupplierDAO.Address = Supplier.Address;
            
            await DataContext.Supplier.AddAsync(SupplierDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(Supplier Supplier)
        {
            SupplierDAO SupplierDAO = DataContext.Supplier.Where(x => x.Id == Supplier.Id).FirstOrDefault();
            
            SupplierDAO.Id = Supplier.Id;
            SupplierDAO.Name = Supplier.Name;
            SupplierDAO.Phone = Supplier.Phone;
            SupplierDAO.ContactPerson = Supplier.ContactPerson;
            SupplierDAO.Address = Supplier.Address;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(Supplier Supplier)
        {
            SupplierDAO SupplierDAO = await DataContext.Supplier.Where(x => x.Id == Supplier.Id).FirstOrDefaultAsync();
            DataContext.Supplier.Remove(SupplierDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
