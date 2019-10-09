
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
    public interface IFeaturePermissionRepository
    {
        Task<int> Count(FeaturePermissionFilter FeaturePermissionFilter);
        Task<List<FeaturePermission>> List(FeaturePermissionFilter FeaturePermissionFilter);
        Task<FeaturePermission> Get(Guid Id);
        Task<bool> Create(FeaturePermission FeaturePermission);
        Task<bool> Update(FeaturePermission FeaturePermission);
        Task<bool> Delete(Guid Id);
        
    }
    public class FeaturePermissionRepository : IFeaturePermissionRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public FeaturePermissionRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<FeaturePermissionDAO> DynamicFilter(IQueryable<FeaturePermissionDAO> query, FeaturePermissionFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.PermissionId != null)
                query = query.Where(q => q.PermissionId, filter.PermissionId);
            if (filter.FeatureId != null)
                query = query.Where(q => q.FeatureId, filter.FeatureId);
            return query;
        }
        private IQueryable<FeaturePermissionDAO> DynamicOrder(IQueryable<FeaturePermissionDAO> query,  FeaturePermissionFilter filter)
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

        private async Task<List<FeaturePermission>> DynamicSelect(IQueryable<FeaturePermissionDAO> query, FeaturePermissionFilter filter)
        {
            List <FeaturePermission> FeaturePermissions = await query.Select(q => new FeaturePermission()
            {
                
                Id = filter.Selects.Contains(FeaturePermissionSelect.Id) ? q.Id : default(Guid),
                PermissionId = filter.Selects.Contains(FeaturePermissionSelect.Permission) ? q.PermissionId : default(Guid),
                FeatureId = filter.Selects.Contains(FeaturePermissionSelect.Feature) ? q.FeatureId : default(Guid),
            }).ToListAsync();
            return FeaturePermissions;
        }

        public async Task<int> Count(FeaturePermissionFilter filter)
        {
            IQueryable <FeaturePermissionDAO> FeaturePermissionDAOs = ERPContext.FeaturePermission;
            FeaturePermissionDAOs = DynamicFilter(FeaturePermissionDAOs, filter);
            return await FeaturePermissionDAOs.CountAsync();
        }

        public async Task<List<FeaturePermission>> List(FeaturePermissionFilter filter)
        {
            if (filter == null) return new List<FeaturePermission>();
            IQueryable<FeaturePermissionDAO> FeaturePermissionDAOs = ERPContext.FeaturePermission;
            FeaturePermissionDAOs = DynamicFilter(FeaturePermissionDAOs, filter);
            FeaturePermissionDAOs = DynamicOrder(FeaturePermissionDAOs, filter);
            var FeaturePermissions = await DynamicSelect(FeaturePermissionDAOs, filter);
            return FeaturePermissions;
        }

        public async Task<FeaturePermission> Get(Guid Id)
        {
            FeaturePermission FeaturePermission = await ERPContext.FeaturePermission.Where(l => l.Id == Id).Select(FeaturePermissionDAO => new FeaturePermission()
            {
                 
                Id = FeaturePermissionDAO.Id,
                PermissionId = FeaturePermissionDAO.PermissionId,
                FeatureId = FeaturePermissionDAO.FeatureId,
            }).FirstOrDefaultAsync();
            return FeaturePermission;
        }

        public async Task<bool> Create(FeaturePermission FeaturePermission)
        {
            FeaturePermissionDAO FeaturePermissionDAO = new FeaturePermissionDAO();
            
            FeaturePermissionDAO.Id = FeaturePermission.Id;
            FeaturePermissionDAO.PermissionId = FeaturePermission.PermissionId;
            FeaturePermissionDAO.FeatureId = FeaturePermission.FeatureId;
            FeaturePermissionDAO.Disabled = false;
            
            await ERPContext.FeaturePermission.AddAsync(FeaturePermissionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(FeaturePermission FeaturePermission)
        {
            FeaturePermissionDAO FeaturePermissionDAO = ERPContext.FeaturePermission.Where(b => b.Id == FeaturePermission.Id).FirstOrDefault();
            
            FeaturePermissionDAO.Id = FeaturePermission.Id;
            FeaturePermissionDAO.PermissionId = FeaturePermission.PermissionId;
            FeaturePermissionDAO.FeatureId = FeaturePermission.FeatureId;
            FeaturePermissionDAO.Disabled = false;
            ERPContext.FeaturePermission.Update(FeaturePermissionDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            FeaturePermissionDAO FeaturePermissionDAO = await ERPContext.FeaturePermission.Where(x => x.Id == Id).FirstOrDefaultAsync();
            FeaturePermissionDAO.Disabled = true;
            ERPContext.FeaturePermission.Update(FeaturePermissionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
