
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
    public interface IHROrganizationRepository
    {
        Task<int> Count(HROrganizationFilter HROrganizationFilter);
        Task<List<HROrganization>> List(HROrganizationFilter HROrganizationFilter);
        Task<HROrganization> Get(Guid Id);
        Task<bool> Create(HROrganization HROrganization);
        Task<bool> Update(HROrganization HROrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class HROrganizationRepository : IHROrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public HROrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<HROrganizationDAO> DynamicFilter(IQueryable<HROrganizationDAO> query, HROrganizationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.ShortName != null)
                query = query.Where(q => q.ShortName, filter.ShortName);
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
        private IQueryable<HROrganizationDAO> DynamicOrder(IQueryable<HROrganizationDAO> query,  HROrganizationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case HROrganizationOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case HROrganizationOrder.ShortName:
                            query = query.OrderBy(q => q.ShortName);
                            break;
                        case HROrganizationOrder.Name:
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
                        
                        case HROrganizationOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case HROrganizationOrder.ShortName:
                            query = query.OrderByDescending(q => q.ShortName);
                            break;
                        case HROrganizationOrder.Name:
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

        private async Task<List<HROrganization>> DynamicSelect(IQueryable<HROrganizationDAO> query, HROrganizationFilter filter)
        {
            List <HROrganization> HROrganizations = await query.Select(q => new HROrganization()
            {
                
                Id = filter.Selects.Contains(HROrganizationSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(HROrganizationSelect.Code) ? q.Code : default(string),
                ShortName = filter.Selects.Contains(HROrganizationSelect.ShortName) ? q.ShortName : default(string),
                Name = filter.Selects.Contains(HROrganizationSelect.Name) ? q.Name : default(string),
                DivisionId = filter.Selects.Contains(HROrganizationSelect.Division) ? q.DivisionId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(HROrganizationSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return HROrganizations;
        }

        public async Task<int> Count(HROrganizationFilter filter)
        {
            IQueryable <HROrganizationDAO> HROrganizationDAOs = ERPContext.HROrganization;
            HROrganizationDAOs = DynamicFilter(HROrganizationDAOs, filter);
            return await HROrganizationDAOs.CountAsync();
        }

        public async Task<List<HROrganization>> List(HROrganizationFilter filter)
        {
            if (filter == null) return new List<HROrganization>();
            IQueryable<HROrganizationDAO> HROrganizationDAOs = ERPContext.HROrganization;
            HROrganizationDAOs = DynamicFilter(HROrganizationDAOs, filter);
            HROrganizationDAOs = DynamicOrder(HROrganizationDAOs, filter);
            var HROrganizations = await DynamicSelect(HROrganizationDAOs, filter);
            return HROrganizations;
        }

        public async Task<HROrganization> Get(Guid Id)
        {
            HROrganization HROrganization = await ERPContext.HROrganization.Where(l => l.Id == Id).Select(HROrganizationDAO => new HROrganization()
            {
                 
                Id = HROrganizationDAO.Id,
                Code = HROrganizationDAO.Code,
                ShortName = HROrganizationDAO.ShortName,
                Name = HROrganizationDAO.Name,
                DivisionId = HROrganizationDAO.DivisionId,
                BusinessGroupId = HROrganizationDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return HROrganization;
        }

        public async Task<bool> Create(HROrganization HROrganization)
        {
            HROrganizationDAO HROrganizationDAO = new HROrganizationDAO();
            
            HROrganizationDAO.Id = HROrganization.Id;
            HROrganizationDAO.Code = HROrganization.Code;
            HROrganizationDAO.ShortName = HROrganization.ShortName;
            HROrganizationDAO.Name = HROrganization.Name;
            HROrganizationDAO.DivisionId = HROrganization.DivisionId;
            HROrganizationDAO.BusinessGroupId = HROrganization.BusinessGroupId;
            HROrganizationDAO.Disabled = false;
            
            await ERPContext.HROrganization.AddAsync(HROrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(HROrganization HROrganization)
        {
            HROrganizationDAO HROrganizationDAO = ERPContext.HROrganization.Where(b => b.Id == HROrganization.Id).FirstOrDefault();
            
            HROrganizationDAO.Id = HROrganization.Id;
            HROrganizationDAO.Code = HROrganization.Code;
            HROrganizationDAO.ShortName = HROrganization.ShortName;
            HROrganizationDAO.Name = HROrganization.Name;
            HROrganizationDAO.DivisionId = HROrganization.DivisionId;
            HROrganizationDAO.BusinessGroupId = HROrganization.BusinessGroupId;
            HROrganizationDAO.Disabled = false;
            ERPContext.HROrganization.Update(HROrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            HROrganizationDAO HROrganizationDAO = await ERPContext.HROrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            HROrganizationDAO.Disabled = true;
            ERPContext.HROrganization.Update(HROrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
