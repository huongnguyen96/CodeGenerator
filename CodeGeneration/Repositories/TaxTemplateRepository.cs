
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
    public interface ITaxTemplateRepository
    {
        Task<int> Count(TaxTemplateFilter TaxTemplateFilter);
        Task<List<TaxTemplate>> List(TaxTemplateFilter TaxTemplateFilter);
        Task<TaxTemplate> Get(Guid Id);
        Task<bool> Create(TaxTemplate TaxTemplate);
        Task<bool> Update(TaxTemplate TaxTemplate);
        Task<bool> Delete(Guid Id);
        
    }
    public class TaxTemplateRepository : ITaxTemplateRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public TaxTemplateRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<TaxTemplateDAO> DynamicFilter(IQueryable<TaxTemplateDAO> query, TaxTemplateFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Type != null)
                query = query.Where(q => q.Type, filter.Type);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<TaxTemplateDAO> DynamicOrder(IQueryable<TaxTemplateDAO> query,  TaxTemplateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case TaxTemplateOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TaxTemplateOrder.Type:
                            query = query.OrderBy(q => q.Type);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case TaxTemplateOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TaxTemplateOrder.Type:
                            query = query.OrderByDescending(q => q.Type);
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

        private async Task<List<TaxTemplate>> DynamicSelect(IQueryable<TaxTemplateDAO> query, TaxTemplateFilter filter)
        {
            List <TaxTemplate> TaxTemplates = await query.Select(q => new TaxTemplate()
            {
                
                Id = filter.Selects.Contains(TaxTemplateSelect.Id) ? q.Id : default(Guid),
                Name = filter.Selects.Contains(TaxTemplateSelect.Name) ? q.Name : default(string),
                Type = filter.Selects.Contains(TaxTemplateSelect.Type) ? q.Type : default(string),
                BusinessGroupId = filter.Selects.Contains(TaxTemplateSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return TaxTemplates;
        }

        public async Task<int> Count(TaxTemplateFilter filter)
        {
            IQueryable <TaxTemplateDAO> TaxTemplateDAOs = ERPContext.TaxTemplate;
            TaxTemplateDAOs = DynamicFilter(TaxTemplateDAOs, filter);
            return await TaxTemplateDAOs.CountAsync();
        }

        public async Task<List<TaxTemplate>> List(TaxTemplateFilter filter)
        {
            if (filter == null) return new List<TaxTemplate>();
            IQueryable<TaxTemplateDAO> TaxTemplateDAOs = ERPContext.TaxTemplate;
            TaxTemplateDAOs = DynamicFilter(TaxTemplateDAOs, filter);
            TaxTemplateDAOs = DynamicOrder(TaxTemplateDAOs, filter);
            var TaxTemplates = await DynamicSelect(TaxTemplateDAOs, filter);
            return TaxTemplates;
        }

        public async Task<TaxTemplate> Get(Guid Id)
        {
            TaxTemplate TaxTemplate = await ERPContext.TaxTemplate.Where(l => l.Id == Id).Select(TaxTemplateDAO => new TaxTemplate()
            {
                 
                Id = TaxTemplateDAO.Id,
                Name = TaxTemplateDAO.Name,
                Type = TaxTemplateDAO.Type,
                BusinessGroupId = TaxTemplateDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return TaxTemplate;
        }

        public async Task<bool> Create(TaxTemplate TaxTemplate)
        {
            TaxTemplateDAO TaxTemplateDAO = new TaxTemplateDAO();
            
            TaxTemplateDAO.Id = TaxTemplate.Id;
            TaxTemplateDAO.Name = TaxTemplate.Name;
            TaxTemplateDAO.Type = TaxTemplate.Type;
            TaxTemplateDAO.BusinessGroupId = TaxTemplate.BusinessGroupId;
            TaxTemplateDAO.Disabled = false;
            
            await ERPContext.TaxTemplate.AddAsync(TaxTemplateDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(TaxTemplate TaxTemplate)
        {
            TaxTemplateDAO TaxTemplateDAO = ERPContext.TaxTemplate.Where(b => b.Id == TaxTemplate.Id).FirstOrDefault();
            
            TaxTemplateDAO.Id = TaxTemplate.Id;
            TaxTemplateDAO.Name = TaxTemplate.Name;
            TaxTemplateDAO.Type = TaxTemplate.Type;
            TaxTemplateDAO.BusinessGroupId = TaxTemplate.BusinessGroupId;
            TaxTemplateDAO.Disabled = false;
            ERPContext.TaxTemplate.Update(TaxTemplateDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            TaxTemplateDAO TaxTemplateDAO = await ERPContext.TaxTemplate.Where(x => x.Id == Id).FirstOrDefaultAsync();
            TaxTemplateDAO.Disabled = true;
            ERPContext.TaxTemplate.Update(TaxTemplateDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
