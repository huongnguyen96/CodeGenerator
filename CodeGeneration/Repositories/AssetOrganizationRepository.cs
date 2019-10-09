
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
    public interface IAssetOrganizationRepository
    {
        Task<int> Count(AssetOrganizationFilter AssetOrganizationFilter);
        Task<List<AssetOrganization>> List(AssetOrganizationFilter AssetOrganizationFilter);
        Task<AssetOrganization> Get(Guid Id);
        Task<bool> Create(AssetOrganization AssetOrganization);
        Task<bool> Update(AssetOrganization AssetOrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class AssetOrganizationRepository : IAssetOrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public AssetOrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<AssetOrganizationDAO> DynamicFilter(IQueryable<AssetOrganizationDAO> query, AssetOrganizationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.DivisionId != null)
                query = query.Where(q => q.DivisionId, filter.DivisionId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<AssetOrganizationDAO> DynamicOrder(IQueryable<AssetOrganizationDAO> query,  AssetOrganizationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case AssetOrganizationOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case AssetOrganizationOrder.Name:
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
                        
                        case AssetOrganizationOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case AssetOrganizationOrder.Name:
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

        private async Task<List<AssetOrganization>> DynamicSelect(IQueryable<AssetOrganizationDAO> query, AssetOrganizationFilter filter)
        {
            List <AssetOrganization> AssetOrganizations = await query.Select(q => new AssetOrganization()
            {
                
                Id = filter.Selects.Contains(AssetOrganizationSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(AssetOrganizationSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(AssetOrganizationSelect.Name) ? q.Name : default(string),
                DivisionId = filter.Selects.Contains(AssetOrganizationSelect.Division) ? q.DivisionId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(AssetOrganizationSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return AssetOrganizations;
        }

        public async Task<int> Count(AssetOrganizationFilter filter)
        {
            IQueryable <AssetOrganizationDAO> AssetOrganizationDAOs = ERPContext.AssetOrganization;
            AssetOrganizationDAOs = DynamicFilter(AssetOrganizationDAOs, filter);
            return await AssetOrganizationDAOs.CountAsync();
        }

        public async Task<List<AssetOrganization>> List(AssetOrganizationFilter filter)
        {
            if (filter == null) return new List<AssetOrganization>();
            IQueryable<AssetOrganizationDAO> AssetOrganizationDAOs = ERPContext.AssetOrganization;
            AssetOrganizationDAOs = DynamicFilter(AssetOrganizationDAOs, filter);
            AssetOrganizationDAOs = DynamicOrder(AssetOrganizationDAOs, filter);
            var AssetOrganizations = await DynamicSelect(AssetOrganizationDAOs, filter);
            return AssetOrganizations;
        }

        public async Task<AssetOrganization> Get(Guid Id)
        {
            AssetOrganization AssetOrganization = await ERPContext.AssetOrganization.Where(l => l.Id == Id).Select(AssetOrganizationDAO => new AssetOrganization()
            {
                 
                Id = AssetOrganizationDAO.Id,
                Code = AssetOrganizationDAO.Code,
                Name = AssetOrganizationDAO.Name,
                DivisionId = AssetOrganizationDAO.DivisionId,
                BusinessGroupId = AssetOrganizationDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return AssetOrganization;
        }

        public async Task<bool> Create(AssetOrganization AssetOrganization)
        {
            AssetOrganizationDAO AssetOrganizationDAO = new AssetOrganizationDAO();
            
            AssetOrganizationDAO.Id = AssetOrganization.Id;
            AssetOrganizationDAO.Code = AssetOrganization.Code;
            AssetOrganizationDAO.Name = AssetOrganization.Name;
            AssetOrganizationDAO.DivisionId = AssetOrganization.DivisionId;
            AssetOrganizationDAO.BusinessGroupId = AssetOrganization.BusinessGroupId;
            AssetOrganizationDAO.Disabled = false;
            
            await ERPContext.AssetOrganization.AddAsync(AssetOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(AssetOrganization AssetOrganization)
        {
            AssetOrganizationDAO AssetOrganizationDAO = ERPContext.AssetOrganization.Where(b => b.Id == AssetOrganization.Id).FirstOrDefault();
            
            AssetOrganizationDAO.Id = AssetOrganization.Id;
            AssetOrganizationDAO.Code = AssetOrganization.Code;
            AssetOrganizationDAO.Name = AssetOrganization.Name;
            AssetOrganizationDAO.DivisionId = AssetOrganization.DivisionId;
            AssetOrganizationDAO.BusinessGroupId = AssetOrganization.BusinessGroupId;
            AssetOrganizationDAO.Disabled = false;
            ERPContext.AssetOrganization.Update(AssetOrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            AssetOrganizationDAO AssetOrganizationDAO = await ERPContext.AssetOrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            AssetOrganizationDAO.Disabled = true;
            ERPContext.AssetOrganization.Update(AssetOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
