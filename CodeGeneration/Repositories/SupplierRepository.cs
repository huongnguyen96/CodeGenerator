
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
    public interface ISupplierRepository
    {
        Task<int> Count(SupplierFilter SupplierFilter);
        Task<List<Supplier>> List(SupplierFilter SupplierFilter);
        Task<Supplier> Get(Guid Id);
        Task<bool> Create(Supplier Supplier);
        Task<bool> Update(Supplier Supplier);
        Task<bool> Delete(Guid Id);
        
    }
    public class SupplierRepository : ISupplierRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SupplierRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierDAO> DynamicFilter(IQueryable<SupplierDAO> query, SupplierFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.TaxCode != null)
                query = query.Where(q => q.TaxCode, filter.TaxCode);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Note != null)
                query = query.Where(q => q.Note, filter.Note);
            return query;
        }
        private IQueryable<SupplierDAO> DynamicOrder(IQueryable<SupplierDAO> query,  SupplierFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SupplierOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SupplierOrder.TaxCode:
                            query = query.OrderBy(q => q.TaxCode);
                            break;
                        case SupplierOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SupplierOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SupplierOrder.TaxCode:
                            query = query.OrderByDescending(q => q.TaxCode);
                            break;
                        case SupplierOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
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

        private async Task<List<Supplier>> DynamicSelect(IQueryable<SupplierDAO> query, SupplierFilter filter)
        {
            List <Supplier> Suppliers = await query.Select(q => new Supplier()
            {
                
                Id = filter.Selects.Contains(SupplierSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(SupplierSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Code = filter.Selects.Contains(SupplierSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SupplierSelect.Name) ? q.Name : default(string),
                TaxCode = filter.Selects.Contains(SupplierSelect.TaxCode) ? q.TaxCode : default(string),
                StatusId = filter.Selects.Contains(SupplierSelect.Status) ? q.StatusId : default(Guid),
                Note = filter.Selects.Contains(SupplierSelect.Note) ? q.Note : default(string),
            }).ToListAsync();
            return Suppliers;
        }

        public async Task<int> Count(SupplierFilter filter)
        {
            IQueryable <SupplierDAO> SupplierDAOs = ERPContext.Supplier;
            SupplierDAOs = DynamicFilter(SupplierDAOs, filter);
            return await SupplierDAOs.CountAsync();
        }

        public async Task<List<Supplier>> List(SupplierFilter filter)
        {
            if (filter == null) return new List<Supplier>();
            IQueryable<SupplierDAO> SupplierDAOs = ERPContext.Supplier;
            SupplierDAOs = DynamicFilter(SupplierDAOs, filter);
            SupplierDAOs = DynamicOrder(SupplierDAOs, filter);
            var Suppliers = await DynamicSelect(SupplierDAOs, filter);
            return Suppliers;
        }

        public async Task<Supplier> Get(Guid Id)
        {
            Supplier Supplier = await ERPContext.Supplier.Where(l => l.Id == Id).Select(SupplierDAO => new Supplier()
            {
                 
                Id = SupplierDAO.Id,
                BusinessGroupId = SupplierDAO.BusinessGroupId,
                Code = SupplierDAO.Code,
                Name = SupplierDAO.Name,
                TaxCode = SupplierDAO.TaxCode,
                StatusId = SupplierDAO.StatusId,
                Note = SupplierDAO.Note,
            }).FirstOrDefaultAsync();
            return Supplier;
        }

        public async Task<bool> Create(Supplier Supplier)
        {
            SupplierDAO SupplierDAO = new SupplierDAO();
            
            SupplierDAO.Id = Supplier.Id;
            SupplierDAO.BusinessGroupId = Supplier.BusinessGroupId;
            SupplierDAO.Code = Supplier.Code;
            SupplierDAO.Name = Supplier.Name;
            SupplierDAO.TaxCode = Supplier.TaxCode;
            SupplierDAO.StatusId = Supplier.StatusId;
            SupplierDAO.Note = Supplier.Note;
            SupplierDAO.Disabled = false;
            
            await ERPContext.Supplier.AddAsync(SupplierDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Supplier Supplier)
        {
            SupplierDAO SupplierDAO = ERPContext.Supplier.Where(b => b.Id == Supplier.Id).FirstOrDefault();
            
            SupplierDAO.Id = Supplier.Id;
            SupplierDAO.BusinessGroupId = Supplier.BusinessGroupId;
            SupplierDAO.Code = Supplier.Code;
            SupplierDAO.Name = Supplier.Name;
            SupplierDAO.TaxCode = Supplier.TaxCode;
            SupplierDAO.StatusId = Supplier.StatusId;
            SupplierDAO.Note = Supplier.Note;
            SupplierDAO.Disabled = false;
            ERPContext.Supplier.Update(SupplierDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SupplierDAO SupplierDAO = await ERPContext.Supplier.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SupplierDAO.Disabled = true;
            ERPContext.Supplier.Update(SupplierDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
