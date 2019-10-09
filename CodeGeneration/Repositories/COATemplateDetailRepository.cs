
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
    public interface ICOATemplateDetailRepository
    {
        Task<int> Count(COATemplateDetailFilter COATemplateDetailFilter);
        Task<List<COATemplateDetail>> List(COATemplateDetailFilter COATemplateDetailFilter);
        Task<COATemplateDetail> Get(Guid Id);
        Task<bool> Create(COATemplateDetail COATemplateDetail);
        Task<bool> Update(COATemplateDetail COATemplateDetail);
        Task<bool> Delete(Guid Id);
        
    }
    public class COATemplateDetailRepository : ICOATemplateDetailRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public COATemplateDetailRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<COATemplateDetailDAO> DynamicFilter(IQueryable<COATemplateDetailDAO> query, COATemplateDetailFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.COATemplateId != null)
                query = query.Where(q => q.COATemplateId, filter.COATemplateId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.ParentId.HasValue)
                query = query.Where(q => q.ParentId.HasValue && q.ParentId.Value == filter.ParentId.Value);
            if (filter.ParentId != null)
                query = query.Where(q => q.ParentId, filter.ParentId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<COATemplateDetailDAO> DynamicOrder(IQueryable<COATemplateDetailDAO> query,  COATemplateDetailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case COATemplateDetailOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case COATemplateDetailOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case COATemplateDetailOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case COATemplateDetailOrder.ParentId:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case COATemplateDetailOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case COATemplateDetailOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case COATemplateDetailOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case COATemplateDetailOrder.ParentId:
                            query = query.OrderByDescending(q => q.ParentId);
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

        private async Task<List<COATemplateDetail>> DynamicSelect(IQueryable<COATemplateDetailDAO> query, COATemplateDetailFilter filter)
        {
            List <COATemplateDetail> COATemplateDetails = await query.Select(q => new COATemplateDetail()
            {
                
                Id = filter.Selects.Contains(COATemplateDetailSelect.Id) ? q.Id : default(Guid),
                COATemplateId = filter.Selects.Contains(COATemplateDetailSelect.COATemplate) ? q.COATemplateId : default(Guid),
                Code = filter.Selects.Contains(COATemplateDetailSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(COATemplateDetailSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(COATemplateDetailSelect.Description) ? q.Description : default(string),
                ParentId = filter.Selects.Contains(COATemplateDetailSelect.Parent) ? q.ParentId : default(Guid?),
                BusinessGroupId = filter.Selects.Contains(COATemplateDetailSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return COATemplateDetails;
        }

        public async Task<int> Count(COATemplateDetailFilter filter)
        {
            IQueryable <COATemplateDetailDAO> COATemplateDetailDAOs = ERPContext.COATemplateDetail;
            COATemplateDetailDAOs = DynamicFilter(COATemplateDetailDAOs, filter);
            return await COATemplateDetailDAOs.CountAsync();
        }

        public async Task<List<COATemplateDetail>> List(COATemplateDetailFilter filter)
        {
            if (filter == null) return new List<COATemplateDetail>();
            IQueryable<COATemplateDetailDAO> COATemplateDetailDAOs = ERPContext.COATemplateDetail;
            COATemplateDetailDAOs = DynamicFilter(COATemplateDetailDAOs, filter);
            COATemplateDetailDAOs = DynamicOrder(COATemplateDetailDAOs, filter);
            var COATemplateDetails = await DynamicSelect(COATemplateDetailDAOs, filter);
            return COATemplateDetails;
        }

        public async Task<COATemplateDetail> Get(Guid Id)
        {
            COATemplateDetail COATemplateDetail = await ERPContext.COATemplateDetail.Where(l => l.Id == Id).Select(COATemplateDetailDAO => new COATemplateDetail()
            {
                 
                Id = COATemplateDetailDAO.Id,
                COATemplateId = COATemplateDetailDAO.COATemplateId,
                Code = COATemplateDetailDAO.Code,
                Name = COATemplateDetailDAO.Name,
                Description = COATemplateDetailDAO.Description,
                ParentId = COATemplateDetailDAO.ParentId,
                BusinessGroupId = COATemplateDetailDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return COATemplateDetail;
        }

        public async Task<bool> Create(COATemplateDetail COATemplateDetail)
        {
            COATemplateDetailDAO COATemplateDetailDAO = new COATemplateDetailDAO();
            
            COATemplateDetailDAO.Id = COATemplateDetail.Id;
            COATemplateDetailDAO.COATemplateId = COATemplateDetail.COATemplateId;
            COATemplateDetailDAO.Code = COATemplateDetail.Code;
            COATemplateDetailDAO.Name = COATemplateDetail.Name;
            COATemplateDetailDAO.Description = COATemplateDetail.Description;
            COATemplateDetailDAO.ParentId = COATemplateDetail.ParentId;
            COATemplateDetailDAO.BusinessGroupId = COATemplateDetail.BusinessGroupId;
            COATemplateDetailDAO.Disabled = false;
            
            await ERPContext.COATemplateDetail.AddAsync(COATemplateDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(COATemplateDetail COATemplateDetail)
        {
            COATemplateDetailDAO COATemplateDetailDAO = ERPContext.COATemplateDetail.Where(b => b.Id == COATemplateDetail.Id).FirstOrDefault();
            
            COATemplateDetailDAO.Id = COATemplateDetail.Id;
            COATemplateDetailDAO.COATemplateId = COATemplateDetail.COATemplateId;
            COATemplateDetailDAO.Code = COATemplateDetail.Code;
            COATemplateDetailDAO.Name = COATemplateDetail.Name;
            COATemplateDetailDAO.Description = COATemplateDetail.Description;
            COATemplateDetailDAO.ParentId = COATemplateDetail.ParentId;
            COATemplateDetailDAO.BusinessGroupId = COATemplateDetail.BusinessGroupId;
            COATemplateDetailDAO.Disabled = false;
            ERPContext.COATemplateDetail.Update(COATemplateDetailDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            COATemplateDetailDAO COATemplateDetailDAO = await ERPContext.COATemplateDetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
            COATemplateDetailDAO.Disabled = true;
            ERPContext.COATemplateDetail.Update(COATemplateDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
