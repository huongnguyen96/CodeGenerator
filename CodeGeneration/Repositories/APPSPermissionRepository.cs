
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
    public interface IAPPSPermissionRepository
    {
        Task<int> Count(APPSPermissionFilter APPSPermissionFilter);
        Task<List<APPSPermission>> List(APPSPermissionFilter APPSPermissionFilter);
        Task<APPSPermission> Get(Guid Id);
        Task<bool> Create(APPSPermission APPSPermission);
        Task<bool> Update(APPSPermission APPSPermission);
        Task<bool> Delete(Guid Id);
        
    }
    public class APPSPermissionRepository : IAPPSPermissionRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public APPSPermissionRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<APPSPermissionDAO> DynamicFilter(IQueryable<APPSPermissionDAO> query, APPSPermissionFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.BusinessGroupId.HasValue)
                query = query.Where(q => q.BusinessGroupId.HasValue && q.BusinessGroupId.Value == filter.BusinessGroupId.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.SetOfBookId.HasValue)
                query = query.Where(q => q.SetOfBookId.HasValue && q.SetOfBookId.Value == filter.SetOfBookId.Value);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.LegalEntityId.HasValue)
                query = query.Where(q => q.LegalEntityId.HasValue && q.LegalEntityId.Value == filter.LegalEntityId.Value);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.DivisionId.HasValue)
                query = query.Where(q => q.DivisionId.HasValue && q.DivisionId.Value == filter.DivisionId.Value);
            if (filter.DivisionId != null)
                query = query.Where(q => q.DivisionId, filter.DivisionId);
            return query;
        }
        private IQueryable<APPSPermissionDAO> DynamicOrder(IQueryable<APPSPermissionDAO> query,  APPSPermissionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case APPSPermissionOrder.BusinessGroupId:
                            query = query.OrderBy(q => q.BusinessGroupId);
                            break;
                        case APPSPermissionOrder.SetOfBookId:
                            query = query.OrderBy(q => q.SetOfBookId);
                            break;
                        case APPSPermissionOrder.LegalEntityId:
                            query = query.OrderBy(q => q.LegalEntityId);
                            break;
                        case APPSPermissionOrder.DivisionId:
                            query = query.OrderBy(q => q.DivisionId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case APPSPermissionOrder.BusinessGroupId:
                            query = query.OrderByDescending(q => q.BusinessGroupId);
                            break;
                        case APPSPermissionOrder.SetOfBookId:
                            query = query.OrderByDescending(q => q.SetOfBookId);
                            break;
                        case APPSPermissionOrder.LegalEntityId:
                            query = query.OrderByDescending(q => q.LegalEntityId);
                            break;
                        case APPSPermissionOrder.DivisionId:
                            query = query.OrderByDescending(q => q.DivisionId);
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

        private async Task<List<APPSPermission>> DynamicSelect(IQueryable<APPSPermissionDAO> query, APPSPermissionFilter filter)
        {
            List <APPSPermission> APPSPermissions = await query.Select(q => new APPSPermission()
            {
                
                Id = filter.Selects.Contains(APPSPermissionSelect.Id) ? q.Id : default(Guid),
                UserId = filter.Selects.Contains(APPSPermissionSelect.User) ? q.UserId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(APPSPermissionSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid?),
                SetOfBookId = filter.Selects.Contains(APPSPermissionSelect.SetOfBook) ? q.SetOfBookId : default(Guid?),
                LegalEntityId = filter.Selects.Contains(APPSPermissionSelect.LegalEntity) ? q.LegalEntityId : default(Guid?),
                DivisionId = filter.Selects.Contains(APPSPermissionSelect.Division) ? q.DivisionId : default(Guid?),
            }).ToListAsync();
            return APPSPermissions;
        }

        public async Task<int> Count(APPSPermissionFilter filter)
        {
            IQueryable <APPSPermissionDAO> APPSPermissionDAOs = ERPContext.APPSPermission;
            APPSPermissionDAOs = DynamicFilter(APPSPermissionDAOs, filter);
            return await APPSPermissionDAOs.CountAsync();
        }

        public async Task<List<APPSPermission>> List(APPSPermissionFilter filter)
        {
            if (filter == null) return new List<APPSPermission>();
            IQueryable<APPSPermissionDAO> APPSPermissionDAOs = ERPContext.APPSPermission;
            APPSPermissionDAOs = DynamicFilter(APPSPermissionDAOs, filter);
            APPSPermissionDAOs = DynamicOrder(APPSPermissionDAOs, filter);
            var APPSPermissions = await DynamicSelect(APPSPermissionDAOs, filter);
            return APPSPermissions;
        }

        public async Task<APPSPermission> Get(Guid Id)
        {
            APPSPermission APPSPermission = await ERPContext.APPSPermission.Where(l => l.Id == Id).Select(APPSPermissionDAO => new APPSPermission()
            {
                 
                Id = APPSPermissionDAO.Id,
                UserId = APPSPermissionDAO.UserId,
                BusinessGroupId = APPSPermissionDAO.BusinessGroupId,
                SetOfBookId = APPSPermissionDAO.SetOfBookId,
                LegalEntityId = APPSPermissionDAO.LegalEntityId,
                DivisionId = APPSPermissionDAO.DivisionId,
            }).FirstOrDefaultAsync();
            return APPSPermission;
        }

        public async Task<bool> Create(APPSPermission APPSPermission)
        {
            APPSPermissionDAO APPSPermissionDAO = new APPSPermissionDAO();
            
            APPSPermissionDAO.Id = APPSPermission.Id;
            APPSPermissionDAO.UserId = APPSPermission.UserId;
            APPSPermissionDAO.BusinessGroupId = APPSPermission.BusinessGroupId;
            APPSPermissionDAO.SetOfBookId = APPSPermission.SetOfBookId;
            APPSPermissionDAO.LegalEntityId = APPSPermission.LegalEntityId;
            APPSPermissionDAO.DivisionId = APPSPermission.DivisionId;
            APPSPermissionDAO.Disabled = false;
            
            await ERPContext.APPSPermission.AddAsync(APPSPermissionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(APPSPermission APPSPermission)
        {
            APPSPermissionDAO APPSPermissionDAO = ERPContext.APPSPermission.Where(b => b.Id == APPSPermission.Id).FirstOrDefault();
            
            APPSPermissionDAO.Id = APPSPermission.Id;
            APPSPermissionDAO.UserId = APPSPermission.UserId;
            APPSPermissionDAO.BusinessGroupId = APPSPermission.BusinessGroupId;
            APPSPermissionDAO.SetOfBookId = APPSPermission.SetOfBookId;
            APPSPermissionDAO.LegalEntityId = APPSPermission.LegalEntityId;
            APPSPermissionDAO.DivisionId = APPSPermission.DivisionId;
            APPSPermissionDAO.Disabled = false;
            ERPContext.APPSPermission.Update(APPSPermissionDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            APPSPermissionDAO APPSPermissionDAO = await ERPContext.APPSPermission.Where(x => x.Id == Id).FirstOrDefaultAsync();
            APPSPermissionDAO.Disabled = true;
            ERPContext.APPSPermission.Update(APPSPermissionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
