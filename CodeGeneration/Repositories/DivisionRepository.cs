
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
    public interface IDivisionRepository
    {
        Task<int> Count(DivisionFilter DivisionFilter);
        Task<List<Division>> List(DivisionFilter DivisionFilter);
        Task<Division> Get(Guid Id);
        Task<bool> Create(Division Division);
        Task<bool> Update(Division Division);
        Task<bool> Delete(Guid Id);
        
    }
    public class DivisionRepository : IDivisionRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public DivisionRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<DivisionDAO> DynamicFilter(IQueryable<DivisionDAO> query, DivisionFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.ShortName != null)
                query = query.Where(q => q.ShortName, filter.ShortName);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<DivisionDAO> DynamicOrder(IQueryable<DivisionDAO> query,  DivisionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case DivisionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case DivisionOrder.ShortName:
                            query = query.OrderBy(q => q.ShortName);
                            break;
                        case DivisionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case DivisionOrder.Description:
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
                        
                        case DivisionOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case DivisionOrder.ShortName:
                            query = query.OrderByDescending(q => q.ShortName);
                            break;
                        case DivisionOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case DivisionOrder.Description:
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

        private async Task<List<Division>> DynamicSelect(IQueryable<DivisionDAO> query, DivisionFilter filter)
        {
            List <Division> Divisions = await query.Select(q => new Division()
            {
                
                Id = filter.Selects.Contains(DivisionSelect.Id) ? q.Id : default(Guid),
                LegalEntityId = filter.Selects.Contains(DivisionSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                Code = filter.Selects.Contains(DivisionSelect.Code) ? q.Code : default(string),
                ShortName = filter.Selects.Contains(DivisionSelect.ShortName) ? q.ShortName : default(string),
                Name = filter.Selects.Contains(DivisionSelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(DivisionSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Description = filter.Selects.Contains(DivisionSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return Divisions;
        }

        public async Task<int> Count(DivisionFilter filter)
        {
            IQueryable <DivisionDAO> DivisionDAOs = ERPContext.Division;
            DivisionDAOs = DynamicFilter(DivisionDAOs, filter);
            return await DivisionDAOs.CountAsync();
        }

        public async Task<List<Division>> List(DivisionFilter filter)
        {
            if (filter == null) return new List<Division>();
            IQueryable<DivisionDAO> DivisionDAOs = ERPContext.Division;
            DivisionDAOs = DynamicFilter(DivisionDAOs, filter);
            DivisionDAOs = DynamicOrder(DivisionDAOs, filter);
            var Divisions = await DynamicSelect(DivisionDAOs, filter);
            return Divisions;
        }

        public async Task<Division> Get(Guid Id)
        {
            Division Division = await ERPContext.Division.Where(l => l.Id == Id).Select(DivisionDAO => new Division()
            {
                 
                Id = DivisionDAO.Id,
                LegalEntityId = DivisionDAO.LegalEntityId,
                Code = DivisionDAO.Code,
                ShortName = DivisionDAO.ShortName,
                Name = DivisionDAO.Name,
                BusinessGroupId = DivisionDAO.BusinessGroupId,
                Description = DivisionDAO.Description,
            }).FirstOrDefaultAsync();
            return Division;
        }

        public async Task<bool> Create(Division Division)
        {
            DivisionDAO DivisionDAO = new DivisionDAO();
            
            DivisionDAO.Id = Division.Id;
            DivisionDAO.LegalEntityId = Division.LegalEntityId;
            DivisionDAO.Code = Division.Code;
            DivisionDAO.ShortName = Division.ShortName;
            DivisionDAO.Name = Division.Name;
            DivisionDAO.BusinessGroupId = Division.BusinessGroupId;
            DivisionDAO.Description = Division.Description;
            DivisionDAO.Disabled = false;
            
            await ERPContext.Division.AddAsync(DivisionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Division Division)
        {
            DivisionDAO DivisionDAO = ERPContext.Division.Where(b => b.Id == Division.Id).FirstOrDefault();
            
            DivisionDAO.Id = Division.Id;
            DivisionDAO.LegalEntityId = Division.LegalEntityId;
            DivisionDAO.Code = Division.Code;
            DivisionDAO.ShortName = Division.ShortName;
            DivisionDAO.Name = Division.Name;
            DivisionDAO.BusinessGroupId = Division.BusinessGroupId;
            DivisionDAO.Description = Division.Description;
            DivisionDAO.Disabled = false;
            ERPContext.Division.Update(DivisionDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            DivisionDAO DivisionDAO = await ERPContext.Division.Where(x => x.Id == Id).FirstOrDefaultAsync();
            DivisionDAO.Disabled = true;
            ERPContext.Division.Update(DivisionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
