
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
    public interface ISupplierGroupingRepository
    {
        Task<int> Count(SupplierGroupingFilter SupplierGroupingFilter);
        Task<List<SupplierGrouping>> List(SupplierGroupingFilter SupplierGroupingFilter);
        Task<SupplierGrouping> Get(Guid Id);
        Task<bool> Create(SupplierGrouping SupplierGrouping);
        Task<bool> Update(SupplierGrouping SupplierGrouping);
        Task<bool> Delete(Guid Id);
        
    }
    public class SupplierGroupingRepository : ISupplierGroupingRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SupplierGroupingRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierGroupingDAO> DynamicFilter(IQueryable<SupplierGroupingDAO> query, SupplierGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<SupplierGroupingDAO> DynamicOrder(IQueryable<SupplierGroupingDAO> query,  SupplierGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SupplierGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SupplierGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SupplierGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SupplierGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
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

        private async Task<List<SupplierGrouping>> DynamicSelect(IQueryable<SupplierGroupingDAO> query, SupplierGroupingFilter filter)
        {
            List <SupplierGrouping> SupplierGroupings = await query.Select(q => new SupplierGrouping()
            {
                
                Id = filter.Selects.Contains(SupplierGroupingSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(SupplierGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SupplierGroupingSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(SupplierGroupingSelect.Description) ? q.Description : default(string),
                LegalEntityId = filter.Selects.Contains(SupplierGroupingSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(SupplierGroupingSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return SupplierGroupings;
        }

        public async Task<int> Count(SupplierGroupingFilter filter)
        {
            IQueryable <SupplierGroupingDAO> SupplierGroupingDAOs = ERPContext.SupplierGrouping;
            SupplierGroupingDAOs = DynamicFilter(SupplierGroupingDAOs, filter);
            return await SupplierGroupingDAOs.CountAsync();
        }

        public async Task<List<SupplierGrouping>> List(SupplierGroupingFilter filter)
        {
            if (filter == null) return new List<SupplierGrouping>();
            IQueryable<SupplierGroupingDAO> SupplierGroupingDAOs = ERPContext.SupplierGrouping;
            SupplierGroupingDAOs = DynamicFilter(SupplierGroupingDAOs, filter);
            SupplierGroupingDAOs = DynamicOrder(SupplierGroupingDAOs, filter);
            var SupplierGroupings = await DynamicSelect(SupplierGroupingDAOs, filter);
            return SupplierGroupings;
        }

        public async Task<SupplierGrouping> Get(Guid Id)
        {
            SupplierGrouping SupplierGrouping = await ERPContext.SupplierGrouping.Where(l => l.Id == Id).Select(SupplierGroupingDAO => new SupplierGrouping()
            {
                 
                Id = SupplierGroupingDAO.Id,
                Code = SupplierGroupingDAO.Code,
                Name = SupplierGroupingDAO.Name,
                Description = SupplierGroupingDAO.Description,
                LegalEntityId = SupplierGroupingDAO.LegalEntityId,
                BusinessGroupId = SupplierGroupingDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return SupplierGrouping;
        }

        public async Task<bool> Create(SupplierGrouping SupplierGrouping)
        {
            SupplierGroupingDAO SupplierGroupingDAO = new SupplierGroupingDAO();
            
            SupplierGroupingDAO.Id = SupplierGrouping.Id;
            SupplierGroupingDAO.Code = SupplierGrouping.Code;
            SupplierGroupingDAO.Name = SupplierGrouping.Name;
            SupplierGroupingDAO.Description = SupplierGrouping.Description;
            SupplierGroupingDAO.LegalEntityId = SupplierGrouping.LegalEntityId;
            SupplierGroupingDAO.BusinessGroupId = SupplierGrouping.BusinessGroupId;
            SupplierGroupingDAO.Disabled = false;
            
            await ERPContext.SupplierGrouping.AddAsync(SupplierGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SupplierGrouping SupplierGrouping)
        {
            SupplierGroupingDAO SupplierGroupingDAO = ERPContext.SupplierGrouping.Where(b => b.Id == SupplierGrouping.Id).FirstOrDefault();
            
            SupplierGroupingDAO.Id = SupplierGrouping.Id;
            SupplierGroupingDAO.Code = SupplierGrouping.Code;
            SupplierGroupingDAO.Name = SupplierGrouping.Name;
            SupplierGroupingDAO.Description = SupplierGrouping.Description;
            SupplierGroupingDAO.LegalEntityId = SupplierGrouping.LegalEntityId;
            SupplierGroupingDAO.BusinessGroupId = SupplierGrouping.BusinessGroupId;
            SupplierGroupingDAO.Disabled = false;
            ERPContext.SupplierGrouping.Update(SupplierGroupingDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SupplierGroupingDAO SupplierGroupingDAO = await ERPContext.SupplierGrouping.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SupplierGroupingDAO.Disabled = true;
            ERPContext.SupplierGrouping.Update(SupplierGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
