
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
    public interface ILegalEntityRepository
    {
        Task<int> Count(LegalEntityFilter LegalEntityFilter);
        Task<List<LegalEntity>> List(LegalEntityFilter LegalEntityFilter);
        Task<LegalEntity> Get(Guid Id);
        Task<bool> Create(LegalEntity LegalEntity);
        Task<bool> Update(LegalEntity LegalEntity);
        Task<bool> Delete(Guid Id);
        
    }
    public class LegalEntityRepository : ILegalEntityRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public LegalEntityRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<LegalEntityDAO> DynamicFilter(IQueryable<LegalEntityDAO> query, LegalEntityFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.ShortName != null)
                query = query.Where(q => q.ShortName, filter.ShortName);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<LegalEntityDAO> DynamicOrder(IQueryable<LegalEntityDAO> query,  LegalEntityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case LegalEntityOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case LegalEntityOrder.ShortName:
                            query = query.OrderBy(q => q.ShortName);
                            break;
                        case LegalEntityOrder.Name:
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
                        
                        case LegalEntityOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case LegalEntityOrder.ShortName:
                            query = query.OrderByDescending(q => q.ShortName);
                            break;
                        case LegalEntityOrder.Name:
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

        private async Task<List<LegalEntity>> DynamicSelect(IQueryable<LegalEntityDAO> query, LegalEntityFilter filter)
        {
            List <LegalEntity> LegalEntitys = await query.Select(q => new LegalEntity()
            {
                
                Id = filter.Selects.Contains(LegalEntitySelect.Id) ? q.Id : default(Guid),
                SetOfBookId = filter.Selects.Contains(LegalEntitySelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                Code = filter.Selects.Contains(LegalEntitySelect.Code) ? q.Code : default(string),
                ShortName = filter.Selects.Contains(LegalEntitySelect.ShortName) ? q.ShortName : default(string),
                Name = filter.Selects.Contains(LegalEntitySelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(LegalEntitySelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return LegalEntitys;
        }

        public async Task<int> Count(LegalEntityFilter filter)
        {
            IQueryable <LegalEntityDAO> LegalEntityDAOs = ERPContext.LegalEntity;
            LegalEntityDAOs = DynamicFilter(LegalEntityDAOs, filter);
            return await LegalEntityDAOs.CountAsync();
        }

        public async Task<List<LegalEntity>> List(LegalEntityFilter filter)
        {
            if (filter == null) return new List<LegalEntity>();
            IQueryable<LegalEntityDAO> LegalEntityDAOs = ERPContext.LegalEntity;
            LegalEntityDAOs = DynamicFilter(LegalEntityDAOs, filter);
            LegalEntityDAOs = DynamicOrder(LegalEntityDAOs, filter);
            var LegalEntitys = await DynamicSelect(LegalEntityDAOs, filter);
            return LegalEntitys;
        }

        public async Task<LegalEntity> Get(Guid Id)
        {
            LegalEntity LegalEntity = await ERPContext.LegalEntity.Where(l => l.Id == Id).Select(LegalEntityDAO => new LegalEntity()
            {
                 
                Id = LegalEntityDAO.Id,
                SetOfBookId = LegalEntityDAO.SetOfBookId,
                Code = LegalEntityDAO.Code,
                ShortName = LegalEntityDAO.ShortName,
                Name = LegalEntityDAO.Name,
                BusinessGroupId = LegalEntityDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return LegalEntity;
        }

        public async Task<bool> Create(LegalEntity LegalEntity)
        {
            LegalEntityDAO LegalEntityDAO = new LegalEntityDAO();
            
            LegalEntityDAO.Id = LegalEntity.Id;
            LegalEntityDAO.SetOfBookId = LegalEntity.SetOfBookId;
            LegalEntityDAO.Code = LegalEntity.Code;
            LegalEntityDAO.ShortName = LegalEntity.ShortName;
            LegalEntityDAO.Name = LegalEntity.Name;
            LegalEntityDAO.BusinessGroupId = LegalEntity.BusinessGroupId;
            LegalEntityDAO.Disabled = false;
            
            await ERPContext.LegalEntity.AddAsync(LegalEntityDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(LegalEntity LegalEntity)
        {
            LegalEntityDAO LegalEntityDAO = ERPContext.LegalEntity.Where(b => b.Id == LegalEntity.Id).FirstOrDefault();
            
            LegalEntityDAO.Id = LegalEntity.Id;
            LegalEntityDAO.SetOfBookId = LegalEntity.SetOfBookId;
            LegalEntityDAO.Code = LegalEntity.Code;
            LegalEntityDAO.ShortName = LegalEntity.ShortName;
            LegalEntityDAO.Name = LegalEntity.Name;
            LegalEntityDAO.BusinessGroupId = LegalEntity.BusinessGroupId;
            LegalEntityDAO.Disabled = false;
            ERPContext.LegalEntity.Update(LegalEntityDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            LegalEntityDAO LegalEntityDAO = await ERPContext.LegalEntity.Where(x => x.Id == Id).FirstOrDefaultAsync();
            LegalEntityDAO.Disabled = true;
            ERPContext.LegalEntity.Update(LegalEntityDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
