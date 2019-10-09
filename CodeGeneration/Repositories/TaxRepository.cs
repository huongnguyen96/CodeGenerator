
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
    public interface ITaxRepository
    {
        Task<int> Count(TaxFilter TaxFilter);
        Task<List<Tax>> List(TaxFilter TaxFilter);
        Task<Tax> Get(Guid Id);
        Task<bool> Create(Tax Tax);
        Task<bool> Update(Tax Tax);
        Task<bool> Delete(Guid Id);
        
    }
    public class TaxRepository : ITaxRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public TaxRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<TaxDAO> DynamicFilter(IQueryable<TaxDAO> query, TaxFilter filter)
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
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Rate.HasValue)
                query = query.Where(q => q.Rate.HasValue && q.Rate.Value == filter.Rate.Value);
            if (filter.Rate != null)
                query = query.Where(q => q.Rate, filter.Rate);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.UnitOfMeasureId.HasValue)
                query = query.Where(q => q.UnitOfMeasureId.HasValue && q.UnitOfMeasureId.Value == filter.UnitOfMeasureId.Value);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Type != null)
                query = query.Where(q => q.Type, filter.Type);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.ParentId.HasValue)
                query = query.Where(q => q.ParentId.HasValue && q.ParentId.Value == filter.ParentId.Value);
            if (filter.ParentId != null)
                query = query.Where(q => q.ParentId, filter.ParentId);
            return query;
        }
        private IQueryable<TaxDAO> DynamicOrder(IQueryable<TaxDAO> query,  TaxFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case TaxOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TaxOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TaxOrder.Rate:
                            query = query.OrderBy(q => q.Rate);
                            break;
                        case TaxOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case TaxOrder.UnitOfMeasureId:
                            query = query.OrderBy(q => q.UnitOfMeasureId);
                            break;
                        case TaxOrder.Type:
                            query = query.OrderBy(q => q.Type);
                            break;
                        case TaxOrder.ParentId:
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
                        
                        case TaxOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TaxOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TaxOrder.Rate:
                            query = query.OrderByDescending(q => q.Rate);
                            break;
                        case TaxOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case TaxOrder.UnitOfMeasureId:
                            query = query.OrderByDescending(q => q.UnitOfMeasureId);
                            break;
                        case TaxOrder.Type:
                            query = query.OrderByDescending(q => q.Type);
                            break;
                        case TaxOrder.ParentId:
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

        private async Task<List<Tax>> DynamicSelect(IQueryable<TaxDAO> query, TaxFilter filter)
        {
            List <Tax> Taxs = await query.Select(q => new Tax()
            {
                
                Id = filter.Selects.Contains(TaxSelect.Id) ? q.Id : default(Guid),
                SetOfBookId = filter.Selects.Contains(TaxSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                Code = filter.Selects.Contains(TaxSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(TaxSelect.Name) ? q.Name : default(string),
                Rate = filter.Selects.Contains(TaxSelect.Rate) ? q.Rate : default(Guid?),
                Description = filter.Selects.Contains(TaxSelect.Description) ? q.Description : default(string),
                UnitOfMeasureId = filter.Selects.Contains(TaxSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(Guid?),
                Type = filter.Selects.Contains(TaxSelect.Type) ? q.Type : default(string),
                BusinessGroupId = filter.Selects.Contains(TaxSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                ParentId = filter.Selects.Contains(TaxSelect.Parent) ? q.ParentId : default(Guid?),
            }).ToListAsync();
            return Taxs;
        }

        public async Task<int> Count(TaxFilter filter)
        {
            IQueryable <TaxDAO> TaxDAOs = ERPContext.Tax;
            TaxDAOs = DynamicFilter(TaxDAOs, filter);
            return await TaxDAOs.CountAsync();
        }

        public async Task<List<Tax>> List(TaxFilter filter)
        {
            if (filter == null) return new List<Tax>();
            IQueryable<TaxDAO> TaxDAOs = ERPContext.Tax;
            TaxDAOs = DynamicFilter(TaxDAOs, filter);
            TaxDAOs = DynamicOrder(TaxDAOs, filter);
            var Taxs = await DynamicSelect(TaxDAOs, filter);
            return Taxs;
        }

        public async Task<Tax> Get(Guid Id)
        {
            Tax Tax = await ERPContext.Tax.Where(l => l.Id == Id).Select(TaxDAO => new Tax()
            {
                 
                Id = TaxDAO.Id,
                SetOfBookId = TaxDAO.SetOfBookId,
                Code = TaxDAO.Code,
                Name = TaxDAO.Name,
                Rate = TaxDAO.Rate,
                Description = TaxDAO.Description,
                UnitOfMeasureId = TaxDAO.UnitOfMeasureId,
                Type = TaxDAO.Type,
                BusinessGroupId = TaxDAO.BusinessGroupId,
                ParentId = TaxDAO.ParentId,
            }).FirstOrDefaultAsync();
            return Tax;
        }

        public async Task<bool> Create(Tax Tax)
        {
            TaxDAO TaxDAO = new TaxDAO();
            
            TaxDAO.Id = Tax.Id;
            TaxDAO.SetOfBookId = Tax.SetOfBookId;
            TaxDAO.Code = Tax.Code;
            TaxDAO.Name = Tax.Name;
            TaxDAO.Rate = Tax.Rate;
            TaxDAO.Description = Tax.Description;
            TaxDAO.UnitOfMeasureId = Tax.UnitOfMeasureId;
            TaxDAO.Type = Tax.Type;
            TaxDAO.BusinessGroupId = Tax.BusinessGroupId;
            TaxDAO.ParentId = Tax.ParentId;
            TaxDAO.Disabled = false;
            
            await ERPContext.Tax.AddAsync(TaxDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Tax Tax)
        {
            TaxDAO TaxDAO = ERPContext.Tax.Where(b => b.Id == Tax.Id).FirstOrDefault();
            
            TaxDAO.Id = Tax.Id;
            TaxDAO.SetOfBookId = Tax.SetOfBookId;
            TaxDAO.Code = Tax.Code;
            TaxDAO.Name = Tax.Name;
            TaxDAO.Rate = Tax.Rate;
            TaxDAO.Description = Tax.Description;
            TaxDAO.UnitOfMeasureId = Tax.UnitOfMeasureId;
            TaxDAO.Type = Tax.Type;
            TaxDAO.BusinessGroupId = Tax.BusinessGroupId;
            TaxDAO.ParentId = Tax.ParentId;
            TaxDAO.Disabled = false;
            ERPContext.Tax.Update(TaxDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            TaxDAO TaxDAO = await ERPContext.Tax.Where(x => x.Id == Id).FirstOrDefaultAsync();
            TaxDAO.Disabled = true;
            ERPContext.Tax.Update(TaxDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
