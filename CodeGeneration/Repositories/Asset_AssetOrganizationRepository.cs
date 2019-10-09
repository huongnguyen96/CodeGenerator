
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
    public interface IAsset_AssetOrganizationRepository
    {
        Task<int> Count(Asset_AssetOrganizationFilter Asset_AssetOrganizationFilter);
        Task<List<Asset_AssetOrganization>> List(Asset_AssetOrganizationFilter Asset_AssetOrganizationFilter);
        Task<Asset_AssetOrganization> Get(Guid Id);
        Task<bool> Create(Asset_AssetOrganization Asset_AssetOrganization);
        Task<bool> Update(Asset_AssetOrganization Asset_AssetOrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class Asset_AssetOrganizationRepository : IAsset_AssetOrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public Asset_AssetOrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Asset_AssetOrganizationDAO> DynamicFilter(IQueryable<Asset_AssetOrganizationDAO> query, Asset_AssetOrganizationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.AssetOrganizationId != null)
                query = query.Where(q => q.AssetOrganizationId, filter.AssetOrganizationId);
            if (filter.AssetId != null)
                query = query.Where(q => q.AssetId, filter.AssetId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.OwnerId.HasValue)
                query = query.Where(q => q.OwnerId.HasValue && q.OwnerId.Value == filter.OwnerId.Value);
            if (filter.OwnerId != null)
                query = query.Where(q => q.OwnerId, filter.OwnerId);
            if (filter.FromDate.HasValue)
                query = query.Where(q => q.FromDate.HasValue && q.FromDate.Value == filter.FromDate.Value);
            if (filter.FromDate != null)
                query = query.Where(q => q.FromDate, filter.FromDate);
            if (filter.ToDate.HasValue)
                query = query.Where(q => q.ToDate.HasValue && q.ToDate.Value == filter.ToDate.Value);
            if (filter.ToDate != null)
                query = query.Where(q => q.ToDate, filter.ToDate);
            return query;
        }
        private IQueryable<Asset_AssetOrganizationDAO> DynamicOrder(IQueryable<Asset_AssetOrganizationDAO> query,  Asset_AssetOrganizationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case Asset_AssetOrganizationOrder.OwnerId:
                            query = query.OrderBy(q => q.OwnerId);
                            break;
                        case Asset_AssetOrganizationOrder.FromDate:
                            query = query.OrderBy(q => q.FromDate);
                            break;
                        case Asset_AssetOrganizationOrder.ToDate:
                            query = query.OrderBy(q => q.ToDate);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case Asset_AssetOrganizationOrder.OwnerId:
                            query = query.OrderByDescending(q => q.OwnerId);
                            break;
                        case Asset_AssetOrganizationOrder.FromDate:
                            query = query.OrderByDescending(q => q.FromDate);
                            break;
                        case Asset_AssetOrganizationOrder.ToDate:
                            query = query.OrderByDescending(q => q.ToDate);
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

        private async Task<List<Asset_AssetOrganization>> DynamicSelect(IQueryable<Asset_AssetOrganizationDAO> query, Asset_AssetOrganizationFilter filter)
        {
            List <Asset_AssetOrganization> Asset_AssetOrganizations = await query.Select(q => new Asset_AssetOrganization()
            {
                
                AssetOrganizationId = filter.Selects.Contains(Asset_AssetOrganizationSelect.AssetOrganization) ? q.AssetOrganizationId : default(Guid),
                AssetId = filter.Selects.Contains(Asset_AssetOrganizationSelect.Asset) ? q.AssetId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(Asset_AssetOrganizationSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                OwnerId = filter.Selects.Contains(Asset_AssetOrganizationSelect.Owner) ? q.OwnerId : default(Guid?),
                FromDate = filter.Selects.Contains(Asset_AssetOrganizationSelect.FromDate) ? q.FromDate : default(Guid?),
                ToDate = filter.Selects.Contains(Asset_AssetOrganizationSelect.ToDate) ? q.ToDate : default(Guid?),
            }).ToListAsync();
            return Asset_AssetOrganizations;
        }

        public async Task<int> Count(Asset_AssetOrganizationFilter filter)
        {
            IQueryable <Asset_AssetOrganizationDAO> Asset_AssetOrganizationDAOs = ERPContext.Asset_AssetOrganization;
            Asset_AssetOrganizationDAOs = DynamicFilter(Asset_AssetOrganizationDAOs, filter);
            return await Asset_AssetOrganizationDAOs.CountAsync();
        }

        public async Task<List<Asset_AssetOrganization>> List(Asset_AssetOrganizationFilter filter)
        {
            if (filter == null) return new List<Asset_AssetOrganization>();
            IQueryable<Asset_AssetOrganizationDAO> Asset_AssetOrganizationDAOs = ERPContext.Asset_AssetOrganization;
            Asset_AssetOrganizationDAOs = DynamicFilter(Asset_AssetOrganizationDAOs, filter);
            Asset_AssetOrganizationDAOs = DynamicOrder(Asset_AssetOrganizationDAOs, filter);
            var Asset_AssetOrganizations = await DynamicSelect(Asset_AssetOrganizationDAOs, filter);
            return Asset_AssetOrganizations;
        }

        public async Task<Asset_AssetOrganization> Get(Guid Id)
        {
            Asset_AssetOrganization Asset_AssetOrganization = await ERPContext.Asset_AssetOrganization.Where(l => l.Id == Id).Select(Asset_AssetOrganizationDAO => new Asset_AssetOrganization()
            {
                 
                AssetOrganizationId = Asset_AssetOrganizationDAO.AssetOrganizationId,
                AssetId = Asset_AssetOrganizationDAO.AssetId,
                BusinessGroupId = Asset_AssetOrganizationDAO.BusinessGroupId,
                OwnerId = Asset_AssetOrganizationDAO.OwnerId,
                FromDate = Asset_AssetOrganizationDAO.FromDate,
                ToDate = Asset_AssetOrganizationDAO.ToDate,
            }).FirstOrDefaultAsync();
            return Asset_AssetOrganization;
        }

        public async Task<bool> Create(Asset_AssetOrganization Asset_AssetOrganization)
        {
            Asset_AssetOrganizationDAO Asset_AssetOrganizationDAO = new Asset_AssetOrganizationDAO();
            
            Asset_AssetOrganizationDAO.AssetOrganizationId = Asset_AssetOrganization.AssetOrganizationId;
            Asset_AssetOrganizationDAO.AssetId = Asset_AssetOrganization.AssetId;
            Asset_AssetOrganizationDAO.BusinessGroupId = Asset_AssetOrganization.BusinessGroupId;
            Asset_AssetOrganizationDAO.OwnerId = Asset_AssetOrganization.OwnerId;
            Asset_AssetOrganizationDAO.FromDate = Asset_AssetOrganization.FromDate;
            Asset_AssetOrganizationDAO.ToDate = Asset_AssetOrganization.ToDate;
            Asset_AssetOrganizationDAO.Disabled = false;
            
            await ERPContext.Asset_AssetOrganization.AddAsync(Asset_AssetOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Asset_AssetOrganization Asset_AssetOrganization)
        {
            Asset_AssetOrganizationDAO Asset_AssetOrganizationDAO = ERPContext.Asset_AssetOrganization.Where(b => b.Id == Asset_AssetOrganization.Id).FirstOrDefault();
            
            Asset_AssetOrganizationDAO.AssetOrganizationId = Asset_AssetOrganization.AssetOrganizationId;
            Asset_AssetOrganizationDAO.AssetId = Asset_AssetOrganization.AssetId;
            Asset_AssetOrganizationDAO.BusinessGroupId = Asset_AssetOrganization.BusinessGroupId;
            Asset_AssetOrganizationDAO.OwnerId = Asset_AssetOrganization.OwnerId;
            Asset_AssetOrganizationDAO.FromDate = Asset_AssetOrganization.FromDate;
            Asset_AssetOrganizationDAO.ToDate = Asset_AssetOrganization.ToDate;
            Asset_AssetOrganizationDAO.Disabled = false;
            ERPContext.Asset_AssetOrganization.Update(Asset_AssetOrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            Asset_AssetOrganizationDAO Asset_AssetOrganizationDAO = await ERPContext.Asset_AssetOrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            Asset_AssetOrganizationDAO.Disabled = true;
            ERPContext.Asset_AssetOrganization.Update(Asset_AssetOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
