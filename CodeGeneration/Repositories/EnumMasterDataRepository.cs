
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
    public interface IEnumMasterDataRepository
    {
        Task<int> Count(EnumMasterDataFilter EnumMasterDataFilter);
        Task<List<EnumMasterData>> List(EnumMasterDataFilter EnumMasterDataFilter);
        Task<EnumMasterData> Get(Guid Id);
        Task<bool> Create(EnumMasterData EnumMasterData);
        Task<bool> Update(EnumMasterData EnumMasterData);
        Task<bool> Delete(Guid Id);
        
    }
    public class EnumMasterDataRepository : IEnumMasterDataRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public EnumMasterDataRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EnumMasterDataDAO> DynamicFilter(IQueryable<EnumMasterDataDAO> query, EnumMasterDataFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Key != null)
                query = query.Where(q => q.Key, filter.Key);
            if (filter.Value != null)
                query = query.Where(q => q.Value, filter.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<EnumMasterDataDAO> DynamicOrder(IQueryable<EnumMasterDataDAO> query,  EnumMasterDataFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case EnumMasterDataOrder.Key:
                            query = query.OrderBy(q => q.Key);
                            break;
                        case EnumMasterDataOrder.Value:
                            query = query.OrderBy(q => q.Value);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case EnumMasterDataOrder.Key:
                            query = query.OrderByDescending(q => q.Key);
                            break;
                        case EnumMasterDataOrder.Value:
                            query = query.OrderByDescending(q => q.Value);
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

        private async Task<List<EnumMasterData>> DynamicSelect(IQueryable<EnumMasterDataDAO> query, EnumMasterDataFilter filter)
        {
            List <EnumMasterData> EnumMasterDatas = await query.Select(q => new EnumMasterData()
            {
                
                Id = filter.Selects.Contains(EnumMasterDataSelect.Id) ? q.Id : default(Guid),
                Key = filter.Selects.Contains(EnumMasterDataSelect.Key) ? q.Key : default(string),
                Value = filter.Selects.Contains(EnumMasterDataSelect.Value) ? q.Value : default(string),
                BusinessGroupId = filter.Selects.Contains(EnumMasterDataSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return EnumMasterDatas;
        }

        public async Task<int> Count(EnumMasterDataFilter filter)
        {
            IQueryable <EnumMasterDataDAO> EnumMasterDataDAOs = ERPContext.EnumMasterData;
            EnumMasterDataDAOs = DynamicFilter(EnumMasterDataDAOs, filter);
            return await EnumMasterDataDAOs.CountAsync();
        }

        public async Task<List<EnumMasterData>> List(EnumMasterDataFilter filter)
        {
            if (filter == null) return new List<EnumMasterData>();
            IQueryable<EnumMasterDataDAO> EnumMasterDataDAOs = ERPContext.EnumMasterData;
            EnumMasterDataDAOs = DynamicFilter(EnumMasterDataDAOs, filter);
            EnumMasterDataDAOs = DynamicOrder(EnumMasterDataDAOs, filter);
            var EnumMasterDatas = await DynamicSelect(EnumMasterDataDAOs, filter);
            return EnumMasterDatas;
        }

        public async Task<EnumMasterData> Get(Guid Id)
        {
            EnumMasterData EnumMasterData = await ERPContext.EnumMasterData.Where(l => l.Id == Id).Select(EnumMasterDataDAO => new EnumMasterData()
            {
                 
                Id = EnumMasterDataDAO.Id,
                Key = EnumMasterDataDAO.Key,
                Value = EnumMasterDataDAO.Value,
                BusinessGroupId = EnumMasterDataDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return EnumMasterData;
        }

        public async Task<bool> Create(EnumMasterData EnumMasterData)
        {
            EnumMasterDataDAO EnumMasterDataDAO = new EnumMasterDataDAO();
            
            EnumMasterDataDAO.Id = EnumMasterData.Id;
            EnumMasterDataDAO.Key = EnumMasterData.Key;
            EnumMasterDataDAO.Value = EnumMasterData.Value;
            EnumMasterDataDAO.BusinessGroupId = EnumMasterData.BusinessGroupId;
            EnumMasterDataDAO.Disabled = false;
            
            await ERPContext.EnumMasterData.AddAsync(EnumMasterDataDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(EnumMasterData EnumMasterData)
        {
            EnumMasterDataDAO EnumMasterDataDAO = ERPContext.EnumMasterData.Where(b => b.Id == EnumMasterData.Id).FirstOrDefault();
            
            EnumMasterDataDAO.Id = EnumMasterData.Id;
            EnumMasterDataDAO.Key = EnumMasterData.Key;
            EnumMasterDataDAO.Value = EnumMasterData.Value;
            EnumMasterDataDAO.BusinessGroupId = EnumMasterData.BusinessGroupId;
            EnumMasterDataDAO.Disabled = false;
            ERPContext.EnumMasterData.Update(EnumMasterDataDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            EnumMasterDataDAO EnumMasterDataDAO = await ERPContext.EnumMasterData.Where(x => x.Id == Id).FirstOrDefaultAsync();
            EnumMasterDataDAO.Disabled = true;
            ERPContext.EnumMasterData.Update(EnumMasterDataDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
