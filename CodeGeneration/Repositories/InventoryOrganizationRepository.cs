
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
    public interface IInventoryOrganizationRepository
    {
        Task<int> Count(InventoryOrganizationFilter InventoryOrganizationFilter);
        Task<List<InventoryOrganization>> List(InventoryOrganizationFilter InventoryOrganizationFilter);
        Task<InventoryOrganization> Get(Guid Id);
        Task<bool> Create(InventoryOrganization InventoryOrganization);
        Task<bool> Update(InventoryOrganization InventoryOrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class InventoryOrganizationRepository : IInventoryOrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public InventoryOrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<InventoryOrganizationDAO> DynamicFilter(IQueryable<InventoryOrganizationDAO> query, InventoryOrganizationFilter filter)
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
        private IQueryable<InventoryOrganizationDAO> DynamicOrder(IQueryable<InventoryOrganizationDAO> query,  InventoryOrganizationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case InventoryOrganizationOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case InventoryOrganizationOrder.ShortName:
                            query = query.OrderBy(q => q.ShortName);
                            break;
                        case InventoryOrganizationOrder.Name:
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
                        
                        case InventoryOrganizationOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case InventoryOrganizationOrder.ShortName:
                            query = query.OrderByDescending(q => q.ShortName);
                            break;
                        case InventoryOrganizationOrder.Name:
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

        private async Task<List<InventoryOrganization>> DynamicSelect(IQueryable<InventoryOrganizationDAO> query, InventoryOrganizationFilter filter)
        {
            List <InventoryOrganization> InventoryOrganizations = await query.Select(q => new InventoryOrganization()
            {
                
                Id = filter.Selects.Contains(InventoryOrganizationSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(InventoryOrganizationSelect.Code) ? q.Code : default(string),
                ShortName = filter.Selects.Contains(InventoryOrganizationSelect.ShortName) ? q.ShortName : default(string),
                Name = filter.Selects.Contains(InventoryOrganizationSelect.Name) ? q.Name : default(string),
                DivisionId = filter.Selects.Contains(InventoryOrganizationSelect.Division) ? q.DivisionId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(InventoryOrganizationSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return InventoryOrganizations;
        }

        public async Task<int> Count(InventoryOrganizationFilter filter)
        {
            IQueryable <InventoryOrganizationDAO> InventoryOrganizationDAOs = ERPContext.InventoryOrganization;
            InventoryOrganizationDAOs = DynamicFilter(InventoryOrganizationDAOs, filter);
            return await InventoryOrganizationDAOs.CountAsync();
        }

        public async Task<List<InventoryOrganization>> List(InventoryOrganizationFilter filter)
        {
            if (filter == null) return new List<InventoryOrganization>();
            IQueryable<InventoryOrganizationDAO> InventoryOrganizationDAOs = ERPContext.InventoryOrganization;
            InventoryOrganizationDAOs = DynamicFilter(InventoryOrganizationDAOs, filter);
            InventoryOrganizationDAOs = DynamicOrder(InventoryOrganizationDAOs, filter);
            var InventoryOrganizations = await DynamicSelect(InventoryOrganizationDAOs, filter);
            return InventoryOrganizations;
        }

        public async Task<InventoryOrganization> Get(Guid Id)
        {
            InventoryOrganization InventoryOrganization = await ERPContext.InventoryOrganization.Where(l => l.Id == Id).Select(InventoryOrganizationDAO => new InventoryOrganization()
            {
                 
                Id = InventoryOrganizationDAO.Id,
                Code = InventoryOrganizationDAO.Code,
                ShortName = InventoryOrganizationDAO.ShortName,
                Name = InventoryOrganizationDAO.Name,
                DivisionId = InventoryOrganizationDAO.DivisionId,
                BusinessGroupId = InventoryOrganizationDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return InventoryOrganization;
        }

        public async Task<bool> Create(InventoryOrganization InventoryOrganization)
        {
            InventoryOrganizationDAO InventoryOrganizationDAO = new InventoryOrganizationDAO();
            
            InventoryOrganizationDAO.Id = InventoryOrganization.Id;
            InventoryOrganizationDAO.Code = InventoryOrganization.Code;
            InventoryOrganizationDAO.ShortName = InventoryOrganization.ShortName;
            InventoryOrganizationDAO.Name = InventoryOrganization.Name;
            InventoryOrganizationDAO.DivisionId = InventoryOrganization.DivisionId;
            InventoryOrganizationDAO.BusinessGroupId = InventoryOrganization.BusinessGroupId;
            InventoryOrganizationDAO.Disabled = false;
            
            await ERPContext.InventoryOrganization.AddAsync(InventoryOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(InventoryOrganization InventoryOrganization)
        {
            InventoryOrganizationDAO InventoryOrganizationDAO = ERPContext.InventoryOrganization.Where(b => b.Id == InventoryOrganization.Id).FirstOrDefault();
            
            InventoryOrganizationDAO.Id = InventoryOrganization.Id;
            InventoryOrganizationDAO.Code = InventoryOrganization.Code;
            InventoryOrganizationDAO.ShortName = InventoryOrganization.ShortName;
            InventoryOrganizationDAO.Name = InventoryOrganization.Name;
            InventoryOrganizationDAO.DivisionId = InventoryOrganization.DivisionId;
            InventoryOrganizationDAO.BusinessGroupId = InventoryOrganization.BusinessGroupId;
            InventoryOrganizationDAO.Disabled = false;
            ERPContext.InventoryOrganization.Update(InventoryOrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            InventoryOrganizationDAO InventoryOrganizationDAO = await ERPContext.InventoryOrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            InventoryOrganizationDAO.Disabled = true;
            ERPContext.InventoryOrganization.Update(InventoryOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
