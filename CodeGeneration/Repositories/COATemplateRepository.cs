
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
    public interface ICOATemplateRepository
    {
        Task<int> Count(COATemplateFilter COATemplateFilter);
        Task<List<COATemplate>> List(COATemplateFilter COATemplateFilter);
        Task<COATemplate> Get(Guid Id);
        Task<bool> Create(COATemplate COATemplate);
        Task<bool> Update(COATemplate COATemplate);
        Task<bool> Delete(Guid Id);
        
    }
    public class COATemplateRepository : ICOATemplateRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public COATemplateRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<COATemplateDAO> DynamicFilter(IQueryable<COATemplateDAO> query, COATemplateFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Type != null)
                query = query.Where(q => q.Type, filter.Type);
            return query;
        }
        private IQueryable<COATemplateDAO> DynamicOrder(IQueryable<COATemplateDAO> query,  COATemplateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case COATemplateOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case COATemplateOrder.Type:
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
                        
                        case COATemplateOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case COATemplateOrder.Type:
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

        private async Task<List<COATemplate>> DynamicSelect(IQueryable<COATemplateDAO> query, COATemplateFilter filter)
        {
            List <COATemplate> COATemplates = await query.Select(q => new COATemplate()
            {
                
                Id = filter.Selects.Contains(COATemplateSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(COATemplateSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Name = filter.Selects.Contains(COATemplateSelect.Name) ? q.Name : default(string),
                Type = filter.Selects.Contains(COATemplateSelect.Type) ? q.Type : default(string),
            }).ToListAsync();
            return COATemplates;
        }

        public async Task<int> Count(COATemplateFilter filter)
        {
            IQueryable <COATemplateDAO> COATemplateDAOs = ERPContext.COATemplate;
            COATemplateDAOs = DynamicFilter(COATemplateDAOs, filter);
            return await COATemplateDAOs.CountAsync();
        }

        public async Task<List<COATemplate>> List(COATemplateFilter filter)
        {
            if (filter == null) return new List<COATemplate>();
            IQueryable<COATemplateDAO> COATemplateDAOs = ERPContext.COATemplate;
            COATemplateDAOs = DynamicFilter(COATemplateDAOs, filter);
            COATemplateDAOs = DynamicOrder(COATemplateDAOs, filter);
            var COATemplates = await DynamicSelect(COATemplateDAOs, filter);
            return COATemplates;
        }

        public async Task<COATemplate> Get(Guid Id)
        {
            COATemplate COATemplate = await ERPContext.COATemplate.Where(l => l.Id == Id).Select(COATemplateDAO => new COATemplate()
            {
                 
                Id = COATemplateDAO.Id,
                BusinessGroupId = COATemplateDAO.BusinessGroupId,
                Name = COATemplateDAO.Name,
                Type = COATemplateDAO.Type,
            }).FirstOrDefaultAsync();
            return COATemplate;
        }

        public async Task<bool> Create(COATemplate COATemplate)
        {
            COATemplateDAO COATemplateDAO = new COATemplateDAO();
            
            COATemplateDAO.Id = COATemplate.Id;
            COATemplateDAO.BusinessGroupId = COATemplate.BusinessGroupId;
            COATemplateDAO.Name = COATemplate.Name;
            COATemplateDAO.Type = COATemplate.Type;
            COATemplateDAO.Disabled = false;
            
            await ERPContext.COATemplate.AddAsync(COATemplateDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(COATemplate COATemplate)
        {
            COATemplateDAO COATemplateDAO = ERPContext.COATemplate.Where(b => b.Id == COATemplate.Id).FirstOrDefault();
            
            COATemplateDAO.Id = COATemplate.Id;
            COATemplateDAO.BusinessGroupId = COATemplate.BusinessGroupId;
            COATemplateDAO.Name = COATemplate.Name;
            COATemplateDAO.Type = COATemplate.Type;
            COATemplateDAO.Disabled = false;
            ERPContext.COATemplate.Update(COATemplateDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            COATemplateDAO COATemplateDAO = await ERPContext.COATemplate.Where(x => x.Id == Id).FirstOrDefaultAsync();
            COATemplateDAO.Disabled = true;
            ERPContext.COATemplate.Update(COATemplateDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
