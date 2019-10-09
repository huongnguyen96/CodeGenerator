
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
    public interface ISupplierDetail_SupplierGroupingRepository
    {
        Task<int> Count(SupplierDetail_SupplierGroupingFilter SupplierDetail_SupplierGroupingFilter);
        Task<List<SupplierDetail_SupplierGrouping>> List(SupplierDetail_SupplierGroupingFilter SupplierDetail_SupplierGroupingFilter);
        Task<SupplierDetail_SupplierGrouping> Get(Guid Id);
        Task<bool> Create(SupplierDetail_SupplierGrouping SupplierDetail_SupplierGrouping);
        Task<bool> Update(SupplierDetail_SupplierGrouping SupplierDetail_SupplierGrouping);
        Task<bool> Delete(Guid Id);
        
    }
    public class SupplierDetail_SupplierGroupingRepository : ISupplierDetail_SupplierGroupingRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SupplierDetail_SupplierGroupingRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierDetail_SupplierGroupingDAO> DynamicFilter(IQueryable<SupplierDetail_SupplierGroupingDAO> query, SupplierDetail_SupplierGroupingFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.SupplierGroupingId != null)
                query = query.Where(q => q.SupplierGroupingId, filter.SupplierGroupingId);
            if (filter.SupplierDetailId != null)
                query = query.Where(q => q.SupplierDetailId, filter.SupplierDetailId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<SupplierDetail_SupplierGroupingDAO> DynamicOrder(IQueryable<SupplierDetail_SupplierGroupingDAO> query,  SupplierDetail_SupplierGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
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

        private async Task<List<SupplierDetail_SupplierGrouping>> DynamicSelect(IQueryable<SupplierDetail_SupplierGroupingDAO> query, SupplierDetail_SupplierGroupingFilter filter)
        {
            List <SupplierDetail_SupplierGrouping> SupplierDetail_SupplierGroupings = await query.Select(q => new SupplierDetail_SupplierGrouping()
            {
                
                Id = filter.Selects.Contains(SupplierDetail_SupplierGroupingSelect.Id) ? q.Id : default(Guid),
                SupplierGroupingId = filter.Selects.Contains(SupplierDetail_SupplierGroupingSelect.SupplierGrouping) ? q.SupplierGroupingId : default(Guid),
                SupplierDetailId = filter.Selects.Contains(SupplierDetail_SupplierGroupingSelect.SupplierDetail) ? q.SupplierDetailId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(SupplierDetail_SupplierGroupingSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return SupplierDetail_SupplierGroupings;
        }

        public async Task<int> Count(SupplierDetail_SupplierGroupingFilter filter)
        {
            IQueryable <SupplierDetail_SupplierGroupingDAO> SupplierDetail_SupplierGroupingDAOs = ERPContext.SupplierDetail_SupplierGrouping;
            SupplierDetail_SupplierGroupingDAOs = DynamicFilter(SupplierDetail_SupplierGroupingDAOs, filter);
            return await SupplierDetail_SupplierGroupingDAOs.CountAsync();
        }

        public async Task<List<SupplierDetail_SupplierGrouping>> List(SupplierDetail_SupplierGroupingFilter filter)
        {
            if (filter == null) return new List<SupplierDetail_SupplierGrouping>();
            IQueryable<SupplierDetail_SupplierGroupingDAO> SupplierDetail_SupplierGroupingDAOs = ERPContext.SupplierDetail_SupplierGrouping;
            SupplierDetail_SupplierGroupingDAOs = DynamicFilter(SupplierDetail_SupplierGroupingDAOs, filter);
            SupplierDetail_SupplierGroupingDAOs = DynamicOrder(SupplierDetail_SupplierGroupingDAOs, filter);
            var SupplierDetail_SupplierGroupings = await DynamicSelect(SupplierDetail_SupplierGroupingDAOs, filter);
            return SupplierDetail_SupplierGroupings;
        }

        public async Task<SupplierDetail_SupplierGrouping> Get(Guid Id)
        {
            SupplierDetail_SupplierGrouping SupplierDetail_SupplierGrouping = await ERPContext.SupplierDetail_SupplierGrouping.Where(l => l.Id == Id).Select(SupplierDetail_SupplierGroupingDAO => new SupplierDetail_SupplierGrouping()
            {
                 
                Id = SupplierDetail_SupplierGroupingDAO.Id,
                SupplierGroupingId = SupplierDetail_SupplierGroupingDAO.SupplierGroupingId,
                SupplierDetailId = SupplierDetail_SupplierGroupingDAO.SupplierDetailId,
                BusinessGroupId = SupplierDetail_SupplierGroupingDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return SupplierDetail_SupplierGrouping;
        }

        public async Task<bool> Create(SupplierDetail_SupplierGrouping SupplierDetail_SupplierGrouping)
        {
            SupplierDetail_SupplierGroupingDAO SupplierDetail_SupplierGroupingDAO = new SupplierDetail_SupplierGroupingDAO();
            
            SupplierDetail_SupplierGroupingDAO.Id = SupplierDetail_SupplierGrouping.Id;
            SupplierDetail_SupplierGroupingDAO.SupplierGroupingId = SupplierDetail_SupplierGrouping.SupplierGroupingId;
            SupplierDetail_SupplierGroupingDAO.SupplierDetailId = SupplierDetail_SupplierGrouping.SupplierDetailId;
            SupplierDetail_SupplierGroupingDAO.BusinessGroupId = SupplierDetail_SupplierGrouping.BusinessGroupId;
            SupplierDetail_SupplierGroupingDAO.Disabled = false;
            
            await ERPContext.SupplierDetail_SupplierGrouping.AddAsync(SupplierDetail_SupplierGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SupplierDetail_SupplierGrouping SupplierDetail_SupplierGrouping)
        {
            SupplierDetail_SupplierGroupingDAO SupplierDetail_SupplierGroupingDAO = ERPContext.SupplierDetail_SupplierGrouping.Where(b => b.Id == SupplierDetail_SupplierGrouping.Id).FirstOrDefault();
            
            SupplierDetail_SupplierGroupingDAO.Id = SupplierDetail_SupplierGrouping.Id;
            SupplierDetail_SupplierGroupingDAO.SupplierGroupingId = SupplierDetail_SupplierGrouping.SupplierGroupingId;
            SupplierDetail_SupplierGroupingDAO.SupplierDetailId = SupplierDetail_SupplierGrouping.SupplierDetailId;
            SupplierDetail_SupplierGroupingDAO.BusinessGroupId = SupplierDetail_SupplierGrouping.BusinessGroupId;
            SupplierDetail_SupplierGroupingDAO.Disabled = false;
            ERPContext.SupplierDetail_SupplierGrouping.Update(SupplierDetail_SupplierGroupingDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SupplierDetail_SupplierGroupingDAO SupplierDetail_SupplierGroupingDAO = await ERPContext.SupplierDetail_SupplierGrouping.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SupplierDetail_SupplierGroupingDAO.Disabled = true;
            ERPContext.SupplierDetail_SupplierGrouping.Update(SupplierDetail_SupplierGroupingDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
