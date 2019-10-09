
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
    public interface IVoucherTypeRepository
    {
        Task<int> Count(VoucherTypeFilter VoucherTypeFilter);
        Task<List<VoucherType>> List(VoucherTypeFilter VoucherTypeFilter);
        Task<VoucherType> Get(Guid Id);
        Task<bool> Create(VoucherType VoucherType);
        Task<bool> Update(VoucherType VoucherType);
        Task<bool> Delete(Guid Id);
        
    }
    public class VoucherTypeRepository : IVoucherTypeRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public VoucherTypeRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<VoucherTypeDAO> DynamicFilter(IQueryable<VoucherTypeDAO> query, VoucherTypeFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<VoucherTypeDAO> DynamicOrder(IQueryable<VoucherTypeDAO> query,  VoucherTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case VoucherTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case VoucherTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case VoucherTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case VoucherTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
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

        private async Task<List<VoucherType>> DynamicSelect(IQueryable<VoucherTypeDAO> query, VoucherTypeFilter filter)
        {
            List <VoucherType> VoucherTypes = await query.Select(q => new VoucherType()
            {
                
                Id = filter.Selects.Contains(VoucherTypeSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(VoucherTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(VoucherTypeSelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(VoucherTypeSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return VoucherTypes;
        }

        public async Task<int> Count(VoucherTypeFilter filter)
        {
            IQueryable <VoucherTypeDAO> VoucherTypeDAOs = ERPContext.VoucherType;
            VoucherTypeDAOs = DynamicFilter(VoucherTypeDAOs, filter);
            return await VoucherTypeDAOs.CountAsync();
        }

        public async Task<List<VoucherType>> List(VoucherTypeFilter filter)
        {
            if (filter == null) return new List<VoucherType>();
            IQueryable<VoucherTypeDAO> VoucherTypeDAOs = ERPContext.VoucherType;
            VoucherTypeDAOs = DynamicFilter(VoucherTypeDAOs, filter);
            VoucherTypeDAOs = DynamicOrder(VoucherTypeDAOs, filter);
            var VoucherTypes = await DynamicSelect(VoucherTypeDAOs, filter);
            return VoucherTypes;
        }

        public async Task<VoucherType> Get(Guid Id)
        {
            VoucherType VoucherType = await ERPContext.VoucherType.Where(l => l.Id == Id).Select(VoucherTypeDAO => new VoucherType()
            {
                 
                Id = VoucherTypeDAO.Id,
                Code = VoucherTypeDAO.Code,
                Name = VoucherTypeDAO.Name,
                BusinessGroupId = VoucherTypeDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return VoucherType;
        }

        public async Task<bool> Create(VoucherType VoucherType)
        {
            VoucherTypeDAO VoucherTypeDAO = new VoucherTypeDAO();
            
            VoucherTypeDAO.Id = VoucherType.Id;
            VoucherTypeDAO.Code = VoucherType.Code;
            VoucherTypeDAO.Name = VoucherType.Name;
            VoucherTypeDAO.BusinessGroupId = VoucherType.BusinessGroupId;
            VoucherTypeDAO.Disabled = false;
            
            await ERPContext.VoucherType.AddAsync(VoucherTypeDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(VoucherType VoucherType)
        {
            VoucherTypeDAO VoucherTypeDAO = ERPContext.VoucherType.Where(b => b.Id == VoucherType.Id).FirstOrDefault();
            
            VoucherTypeDAO.Id = VoucherType.Id;
            VoucherTypeDAO.Code = VoucherType.Code;
            VoucherTypeDAO.Name = VoucherType.Name;
            VoucherTypeDAO.BusinessGroupId = VoucherType.BusinessGroupId;
            VoucherTypeDAO.Disabled = false;
            ERPContext.VoucherType.Update(VoucherTypeDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            VoucherTypeDAO VoucherTypeDAO = await ERPContext.VoucherType.Where(x => x.Id == Id).FirstOrDefaultAsync();
            VoucherTypeDAO.Disabled = true;
            ERPContext.VoucherType.Update(VoucherTypeDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
