
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
    public interface IBusinessGroupRepository
    {
        Task<int> Count(BusinessGroupFilter BusinessGroupFilter);
        Task<List<BusinessGroup>> List(BusinessGroupFilter BusinessGroupFilter);
        Task<BusinessGroup> Get(Guid Id);
        Task<bool> Create(BusinessGroup BusinessGroup);
        Task<bool> Update(BusinessGroup BusinessGroup);
        Task<bool> Delete(Guid Id);
        
    }
    public class BusinessGroupRepository : IBusinessGroupRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public BusinessGroupRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<BusinessGroupDAO> DynamicFilter(IQueryable<BusinessGroupDAO> query, BusinessGroupFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.ShortName != null)
                query = query.Where(q => q.ShortName, filter.ShortName);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<BusinessGroupDAO> DynamicOrder(IQueryable<BusinessGroupDAO> query,  BusinessGroupFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case BusinessGroupOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case BusinessGroupOrder.ShortName:
                            query = query.OrderBy(q => q.ShortName);
                            break;
                        case BusinessGroupOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case BusinessGroupOrder.Description:
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
                        
                        case BusinessGroupOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case BusinessGroupOrder.ShortName:
                            query = query.OrderByDescending(q => q.ShortName);
                            break;
                        case BusinessGroupOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case BusinessGroupOrder.Description:
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

        private async Task<List<BusinessGroup>> DynamicSelect(IQueryable<BusinessGroupDAO> query, BusinessGroupFilter filter)
        {
            List <BusinessGroup> BusinessGroups = await query.Select(q => new BusinessGroup()
            {
                
                Id = filter.Selects.Contains(BusinessGroupSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(BusinessGroupSelect.Code) ? q.Code : default(string),
                ShortName = filter.Selects.Contains(BusinessGroupSelect.ShortName) ? q.ShortName : default(string),
                Name = filter.Selects.Contains(BusinessGroupSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(BusinessGroupSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return BusinessGroups;
        }

        public async Task<int> Count(BusinessGroupFilter filter)
        {
            IQueryable <BusinessGroupDAO> BusinessGroupDAOs = ERPContext.BusinessGroup;
            BusinessGroupDAOs = DynamicFilter(BusinessGroupDAOs, filter);
            return await BusinessGroupDAOs.CountAsync();
        }

        public async Task<List<BusinessGroup>> List(BusinessGroupFilter filter)
        {
            if (filter == null) return new List<BusinessGroup>();
            IQueryable<BusinessGroupDAO> BusinessGroupDAOs = ERPContext.BusinessGroup;
            BusinessGroupDAOs = DynamicFilter(BusinessGroupDAOs, filter);
            BusinessGroupDAOs = DynamicOrder(BusinessGroupDAOs, filter);
            var BusinessGroups = await DynamicSelect(BusinessGroupDAOs, filter);
            return BusinessGroups;
        }

        public async Task<BusinessGroup> Get(Guid Id)
        {
            BusinessGroup BusinessGroup = await ERPContext.BusinessGroup.Where(l => l.Id == Id).Select(BusinessGroupDAO => new BusinessGroup()
            {
                 
                Id = BusinessGroupDAO.Id,
                Code = BusinessGroupDAO.Code,
                ShortName = BusinessGroupDAO.ShortName,
                Name = BusinessGroupDAO.Name,
                Description = BusinessGroupDAO.Description,
            }).FirstOrDefaultAsync();
            return BusinessGroup;
        }

        public async Task<bool> Create(BusinessGroup BusinessGroup)
        {
            BusinessGroupDAO BusinessGroupDAO = new BusinessGroupDAO();
            
            BusinessGroupDAO.Id = BusinessGroup.Id;
            BusinessGroupDAO.Code = BusinessGroup.Code;
            BusinessGroupDAO.ShortName = BusinessGroup.ShortName;
            BusinessGroupDAO.Name = BusinessGroup.Name;
            BusinessGroupDAO.Description = BusinessGroup.Description;
            BusinessGroupDAO.Disabled = false;
            
            await ERPContext.BusinessGroup.AddAsync(BusinessGroupDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(BusinessGroup BusinessGroup)
        {
            BusinessGroupDAO BusinessGroupDAO = ERPContext.BusinessGroup.Where(b => b.Id == BusinessGroup.Id).FirstOrDefault();
            
            BusinessGroupDAO.Id = BusinessGroup.Id;
            BusinessGroupDAO.Code = BusinessGroup.Code;
            BusinessGroupDAO.ShortName = BusinessGroup.ShortName;
            BusinessGroupDAO.Name = BusinessGroup.Name;
            BusinessGroupDAO.Description = BusinessGroup.Description;
            BusinessGroupDAO.Disabled = false;
            ERPContext.BusinessGroup.Update(BusinessGroupDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            BusinessGroupDAO BusinessGroupDAO = await ERPContext.BusinessGroup.Where(x => x.Id == Id).FirstOrDefaultAsync();
            BusinessGroupDAO.Disabled = true;
            ERPContext.BusinessGroup.Update(BusinessGroupDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
