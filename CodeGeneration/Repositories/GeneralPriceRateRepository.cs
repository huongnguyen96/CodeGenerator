
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
    public interface IGeneralPriceRateRepository
    {
        Task<int> Count(GeneralPriceRateFilter GeneralPriceRateFilter);
        Task<List<GeneralPriceRate>> List(GeneralPriceRateFilter GeneralPriceRateFilter);
        Task<GeneralPriceRate> Get(Guid Id);
        Task<bool> Create(GeneralPriceRate GeneralPriceRate);
        Task<bool> Update(GeneralPriceRate GeneralPriceRate);
        Task<bool> Delete(Guid Id);
        
    }
    public class GeneralPriceRateRepository : IGeneralPriceRateRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public GeneralPriceRateRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<GeneralPriceRateDAO> DynamicFilter(IQueryable<GeneralPriceRateDAO> query, GeneralPriceRateFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Price.HasValue)
                query = query.Where(q => q.Price.HasValue && q.Price.Value == filter.Price.Value);
            if (filter.Price != null)
                query = query.Where(q => q.Price, filter.Price);
            return query;
        }
        private IQueryable<GeneralPriceRateDAO> DynamicOrder(IQueryable<GeneralPriceRateDAO> query,  GeneralPriceRateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case GeneralPriceRateOrder.Price:
                            query = query.OrderBy(q => q.Price);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case GeneralPriceRateOrder.Price:
                            query = query.OrderByDescending(q => q.Price);
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

        private async Task<List<GeneralPriceRate>> DynamicSelect(IQueryable<GeneralPriceRateDAO> query, GeneralPriceRateFilter filter)
        {
            List <GeneralPriceRate> GeneralPriceRates = await query.Select(q => new GeneralPriceRate()
            {
                
                Id = filter.Selects.Contains(GeneralPriceRateSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(GeneralPriceRateSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                ItemId = filter.Selects.Contains(GeneralPriceRateSelect.Item) ? q.ItemId : default(Guid),
                UnitOfMeasureId = filter.Selects.Contains(GeneralPriceRateSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(Guid),
                Price = filter.Selects.Contains(GeneralPriceRateSelect.Price) ? q.Price : default(Guid?),
            }).ToListAsync();
            return GeneralPriceRates;
        }

        public async Task<int> Count(GeneralPriceRateFilter filter)
        {
            IQueryable <GeneralPriceRateDAO> GeneralPriceRateDAOs = ERPContext.GeneralPriceRate;
            GeneralPriceRateDAOs = DynamicFilter(GeneralPriceRateDAOs, filter);
            return await GeneralPriceRateDAOs.CountAsync();
        }

        public async Task<List<GeneralPriceRate>> List(GeneralPriceRateFilter filter)
        {
            if (filter == null) return new List<GeneralPriceRate>();
            IQueryable<GeneralPriceRateDAO> GeneralPriceRateDAOs = ERPContext.GeneralPriceRate;
            GeneralPriceRateDAOs = DynamicFilter(GeneralPriceRateDAOs, filter);
            GeneralPriceRateDAOs = DynamicOrder(GeneralPriceRateDAOs, filter);
            var GeneralPriceRates = await DynamicSelect(GeneralPriceRateDAOs, filter);
            return GeneralPriceRates;
        }

        public async Task<GeneralPriceRate> Get(Guid Id)
        {
            GeneralPriceRate GeneralPriceRate = await ERPContext.GeneralPriceRate.Where(l => l.Id == Id).Select(GeneralPriceRateDAO => new GeneralPriceRate()
            {
                 
                Id = GeneralPriceRateDAO.Id,
                BusinessGroupId = GeneralPriceRateDAO.BusinessGroupId,
                ItemId = GeneralPriceRateDAO.ItemId,
                UnitOfMeasureId = GeneralPriceRateDAO.UnitOfMeasureId,
                Price = GeneralPriceRateDAO.Price,
            }).FirstOrDefaultAsync();
            return GeneralPriceRate;
        }

        public async Task<bool> Create(GeneralPriceRate GeneralPriceRate)
        {
            GeneralPriceRateDAO GeneralPriceRateDAO = new GeneralPriceRateDAO();
            
            GeneralPriceRateDAO.Id = GeneralPriceRate.Id;
            GeneralPriceRateDAO.BusinessGroupId = GeneralPriceRate.BusinessGroupId;
            GeneralPriceRateDAO.ItemId = GeneralPriceRate.ItemId;
            GeneralPriceRateDAO.UnitOfMeasureId = GeneralPriceRate.UnitOfMeasureId;
            GeneralPriceRateDAO.Price = GeneralPriceRate.Price;
            GeneralPriceRateDAO.Disabled = false;
            
            await ERPContext.GeneralPriceRate.AddAsync(GeneralPriceRateDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(GeneralPriceRate GeneralPriceRate)
        {
            GeneralPriceRateDAO GeneralPriceRateDAO = ERPContext.GeneralPriceRate.Where(b => b.Id == GeneralPriceRate.Id).FirstOrDefault();
            
            GeneralPriceRateDAO.Id = GeneralPriceRate.Id;
            GeneralPriceRateDAO.BusinessGroupId = GeneralPriceRate.BusinessGroupId;
            GeneralPriceRateDAO.ItemId = GeneralPriceRate.ItemId;
            GeneralPriceRateDAO.UnitOfMeasureId = GeneralPriceRate.UnitOfMeasureId;
            GeneralPriceRateDAO.Price = GeneralPriceRate.Price;
            GeneralPriceRateDAO.Disabled = false;
            ERPContext.GeneralPriceRate.Update(GeneralPriceRateDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            GeneralPriceRateDAO GeneralPriceRateDAO = await ERPContext.GeneralPriceRate.Where(x => x.Id == Id).FirstOrDefaultAsync();
            GeneralPriceRateDAO.Disabled = true;
            ERPContext.GeneralPriceRate.Update(GeneralPriceRateDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
