
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
    public interface ITaxTemplateDetailRepository
    {
        Task<int> Count(TaxTemplateDetailFilter TaxTemplateDetailFilter);
        Task<List<TaxTemplateDetail>> List(TaxTemplateDetailFilter TaxTemplateDetailFilter);
        Task<TaxTemplateDetail> Get(Guid Id);
        Task<bool> Create(TaxTemplateDetail TaxTemplateDetail);
        Task<bool> Update(TaxTemplateDetail TaxTemplateDetail);
        Task<bool> Delete(Guid Id);
        
    }
    public class TaxTemplateDetailRepository : ITaxTemplateDetailRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public TaxTemplateDetailRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<TaxTemplateDetailDAO> DynamicFilter(IQueryable<TaxTemplateDetailDAO> query, TaxTemplateDetailFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.TaxTemplateId != null)
                query = query.Where(q => q.TaxTemplateId, filter.TaxTemplateId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Rate != null)
                query = query.Where(q => q.Rate, filter.Rate);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<TaxTemplateDetailDAO> DynamicOrder(IQueryable<TaxTemplateDetailDAO> query,  TaxTemplateDetailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case TaxTemplateDetailOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TaxTemplateDetailOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TaxTemplateDetailOrder.Rate:
                            query = query.OrderBy(q => q.Rate);
                            break;
                        case TaxTemplateDetailOrder.Description:
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
                        
                        case TaxTemplateDetailOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TaxTemplateDetailOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TaxTemplateDetailOrder.Rate:
                            query = query.OrderByDescending(q => q.Rate);
                            break;
                        case TaxTemplateDetailOrder.Description:
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

        private async Task<List<TaxTemplateDetail>> DynamicSelect(IQueryable<TaxTemplateDetailDAO> query, TaxTemplateDetailFilter filter)
        {
            List <TaxTemplateDetail> TaxTemplateDetails = await query.Select(q => new TaxTemplateDetail()
            {
                
                Id = filter.Selects.Contains(TaxTemplateDetailSelect.Id) ? q.Id : default(Guid),
                TaxTemplateId = filter.Selects.Contains(TaxTemplateDetailSelect.TaxTemplate) ? q.TaxTemplateId : default(Guid),
                Code = filter.Selects.Contains(TaxTemplateDetailSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(TaxTemplateDetailSelect.Name) ? q.Name : default(string),
                UnitOfMeasureId = filter.Selects.Contains(TaxTemplateDetailSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(Guid),
                Rate = filter.Selects.Contains(TaxTemplateDetailSelect.Rate) ? q.Rate : default(decimal),
                Description = filter.Selects.Contains(TaxTemplateDetailSelect.Description) ? q.Description : default(string),
                BusinessGroupId = filter.Selects.Contains(TaxTemplateDetailSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return TaxTemplateDetails;
        }

        public async Task<int> Count(TaxTemplateDetailFilter filter)
        {
            IQueryable <TaxTemplateDetailDAO> TaxTemplateDetailDAOs = ERPContext.TaxTemplateDetail;
            TaxTemplateDetailDAOs = DynamicFilter(TaxTemplateDetailDAOs, filter);
            return await TaxTemplateDetailDAOs.CountAsync();
        }

        public async Task<List<TaxTemplateDetail>> List(TaxTemplateDetailFilter filter)
        {
            if (filter == null) return new List<TaxTemplateDetail>();
            IQueryable<TaxTemplateDetailDAO> TaxTemplateDetailDAOs = ERPContext.TaxTemplateDetail;
            TaxTemplateDetailDAOs = DynamicFilter(TaxTemplateDetailDAOs, filter);
            TaxTemplateDetailDAOs = DynamicOrder(TaxTemplateDetailDAOs, filter);
            var TaxTemplateDetails = await DynamicSelect(TaxTemplateDetailDAOs, filter);
            return TaxTemplateDetails;
        }

        public async Task<TaxTemplateDetail> Get(Guid Id)
        {
            TaxTemplateDetail TaxTemplateDetail = await ERPContext.TaxTemplateDetail.Where(l => l.Id == Id).Select(TaxTemplateDetailDAO => new TaxTemplateDetail()
            {
                 
                Id = TaxTemplateDetailDAO.Id,
                TaxTemplateId = TaxTemplateDetailDAO.TaxTemplateId,
                Code = TaxTemplateDetailDAO.Code,
                Name = TaxTemplateDetailDAO.Name,
                UnitOfMeasureId = TaxTemplateDetailDAO.UnitOfMeasureId,
                Rate = TaxTemplateDetailDAO.Rate,
                Description = TaxTemplateDetailDAO.Description,
                BusinessGroupId = TaxTemplateDetailDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return TaxTemplateDetail;
        }

        public async Task<bool> Create(TaxTemplateDetail TaxTemplateDetail)
        {
            TaxTemplateDetailDAO TaxTemplateDetailDAO = new TaxTemplateDetailDAO();
            
            TaxTemplateDetailDAO.Id = TaxTemplateDetail.Id;
            TaxTemplateDetailDAO.TaxTemplateId = TaxTemplateDetail.TaxTemplateId;
            TaxTemplateDetailDAO.Code = TaxTemplateDetail.Code;
            TaxTemplateDetailDAO.Name = TaxTemplateDetail.Name;
            TaxTemplateDetailDAO.UnitOfMeasureId = TaxTemplateDetail.UnitOfMeasureId;
            TaxTemplateDetailDAO.Rate = TaxTemplateDetail.Rate;
            TaxTemplateDetailDAO.Description = TaxTemplateDetail.Description;
            TaxTemplateDetailDAO.BusinessGroupId = TaxTemplateDetail.BusinessGroupId;
            TaxTemplateDetailDAO.Disabled = false;
            
            await ERPContext.TaxTemplateDetail.AddAsync(TaxTemplateDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(TaxTemplateDetail TaxTemplateDetail)
        {
            TaxTemplateDetailDAO TaxTemplateDetailDAO = ERPContext.TaxTemplateDetail.Where(b => b.Id == TaxTemplateDetail.Id).FirstOrDefault();
            
            TaxTemplateDetailDAO.Id = TaxTemplateDetail.Id;
            TaxTemplateDetailDAO.TaxTemplateId = TaxTemplateDetail.TaxTemplateId;
            TaxTemplateDetailDAO.Code = TaxTemplateDetail.Code;
            TaxTemplateDetailDAO.Name = TaxTemplateDetail.Name;
            TaxTemplateDetailDAO.UnitOfMeasureId = TaxTemplateDetail.UnitOfMeasureId;
            TaxTemplateDetailDAO.Rate = TaxTemplateDetail.Rate;
            TaxTemplateDetailDAO.Description = TaxTemplateDetail.Description;
            TaxTemplateDetailDAO.BusinessGroupId = TaxTemplateDetail.BusinessGroupId;
            TaxTemplateDetailDAO.Disabled = false;
            ERPContext.TaxTemplateDetail.Update(TaxTemplateDetailDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            TaxTemplateDetailDAO TaxTemplateDetailDAO = await ERPContext.TaxTemplateDetail.Where(x => x.Id == Id).FirstOrDefaultAsync();
            TaxTemplateDetailDAO.Disabled = true;
            ERPContext.TaxTemplateDetail.Update(TaxTemplateDetailDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
