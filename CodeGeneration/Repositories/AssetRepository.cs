
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
    public interface IAssetRepository
    {
        Task<int> Count(AssetFilter AssetFilter);
        Task<List<Asset>> List(AssetFilter AssetFilter);
        Task<Asset> Get(Guid Id);
        Task<bool> Create(Asset Asset);
        Task<bool> Update(Asset Asset);
        Task<bool> Delete(Guid Id);
        
    }
    public class AssetRepository : IAssetRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public AssetRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<AssetDAO> DynamicFilter(IQueryable<AssetDAO> query, AssetFilter filter)
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
            if (filter.TypeId != null)
                query = query.Where(q => q.TypeId, filter.TypeId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<AssetDAO> DynamicOrder(IQueryable<AssetDAO> query,  AssetFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case AssetOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case AssetOrder.Name:
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
                        
                        case AssetOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case AssetOrder.Name:
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

        private async Task<List<Asset>> DynamicSelect(IQueryable<AssetDAO> query, AssetFilter filter)
        {
            List <Asset> Assets = await query.Select(q => new Asset()
            {
                
                Id = filter.Selects.Contains(AssetSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(AssetSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(AssetSelect.Name) ? q.Name : default(string),
                TypeId = filter.Selects.Contains(AssetSelect.Type) ? q.TypeId : default(Guid),
                StatusId = filter.Selects.Contains(AssetSelect.Status) ? q.StatusId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(AssetSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return Assets;
        }

        public async Task<int> Count(AssetFilter filter)
        {
            IQueryable <AssetDAO> AssetDAOs = ERPContext.Asset;
            AssetDAOs = DynamicFilter(AssetDAOs, filter);
            return await AssetDAOs.CountAsync();
        }

        public async Task<List<Asset>> List(AssetFilter filter)
        {
            if (filter == null) return new List<Asset>();
            IQueryable<AssetDAO> AssetDAOs = ERPContext.Asset;
            AssetDAOs = DynamicFilter(AssetDAOs, filter);
            AssetDAOs = DynamicOrder(AssetDAOs, filter);
            var Assets = await DynamicSelect(AssetDAOs, filter);
            return Assets;
        }

        public async Task<Asset> Get(Guid Id)
        {
            Asset Asset = await ERPContext.Asset.Where(l => l.Id == Id).Select(AssetDAO => new Asset()
            {
                 
                Id = AssetDAO.Id,
                Code = AssetDAO.Code,
                Name = AssetDAO.Name,
                TypeId = AssetDAO.TypeId,
                StatusId = AssetDAO.StatusId,
                BusinessGroupId = AssetDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return Asset;
        }

        public async Task<bool> Create(Asset Asset)
        {
            AssetDAO AssetDAO = new AssetDAO();
            
            AssetDAO.Id = Asset.Id;
            AssetDAO.Code = Asset.Code;
            AssetDAO.Name = Asset.Name;
            AssetDAO.TypeId = Asset.TypeId;
            AssetDAO.StatusId = Asset.StatusId;
            AssetDAO.BusinessGroupId = Asset.BusinessGroupId;
            AssetDAO.Disabled = false;
            
            await ERPContext.Asset.AddAsync(AssetDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Asset Asset)
        {
            AssetDAO AssetDAO = ERPContext.Asset.Where(b => b.Id == Asset.Id).FirstOrDefault();
            
            AssetDAO.Id = Asset.Id;
            AssetDAO.Code = Asset.Code;
            AssetDAO.Name = Asset.Name;
            AssetDAO.TypeId = Asset.TypeId;
            AssetDAO.StatusId = Asset.StatusId;
            AssetDAO.BusinessGroupId = Asset.BusinessGroupId;
            AssetDAO.Disabled = false;
            ERPContext.Asset.Update(AssetDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            AssetDAO AssetDAO = await ERPContext.Asset.Where(x => x.Id == Id).FirstOrDefaultAsync();
            AssetDAO.Disabled = true;
            ERPContext.Asset.Update(AssetDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
