
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
    public interface ICurrencyRepository
    {
        Task<int> Count(CurrencyFilter CurrencyFilter);
        Task<List<Currency>> List(CurrencyFilter CurrencyFilter);
        Task<Currency> Get(Guid Id);
        Task<bool> Create(Currency Currency);
        Task<bool> Update(Currency Currency);
        Task<bool> Delete(Guid Id);
        
    }
    public class CurrencyRepository : ICurrencyRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CurrencyRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CurrencyDAO> DynamicFilter(IQueryable<CurrencyDAO> query, CurrencyFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Sequence != null)
                query = query.Where(q => q.Sequence, filter.Sequence);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<CurrencyDAO> DynamicOrder(IQueryable<CurrencyDAO> query,  CurrencyFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CurrencyOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CurrencyOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CurrencyOrder.Sequence:
                            query = query.OrderBy(q => q.Sequence);
                            break;
                        case CurrencyOrder.Description:
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
                        
                        case CurrencyOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CurrencyOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CurrencyOrder.Sequence:
                            query = query.OrderByDescending(q => q.Sequence);
                            break;
                        case CurrencyOrder.Description:
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

        private async Task<List<Currency>> DynamicSelect(IQueryable<CurrencyDAO> query, CurrencyFilter filter)
        {
            List <Currency> Currencys = await query.Select(q => new Currency()
            {
                
                Id = filter.Selects.Contains(CurrencySelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CurrencySelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Code = filter.Selects.Contains(CurrencySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CurrencySelect.Name) ? q.Name : default(string),
                Sequence = filter.Selects.Contains(CurrencySelect.Sequence) ? q.Sequence : default(int),
                Description = filter.Selects.Contains(CurrencySelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return Currencys;
        }

        public async Task<int> Count(CurrencyFilter filter)
        {
            IQueryable <CurrencyDAO> CurrencyDAOs = ERPContext.Currency;
            CurrencyDAOs = DynamicFilter(CurrencyDAOs, filter);
            return await CurrencyDAOs.CountAsync();
        }

        public async Task<List<Currency>> List(CurrencyFilter filter)
        {
            if (filter == null) return new List<Currency>();
            IQueryable<CurrencyDAO> CurrencyDAOs = ERPContext.Currency;
            CurrencyDAOs = DynamicFilter(CurrencyDAOs, filter);
            CurrencyDAOs = DynamicOrder(CurrencyDAOs, filter);
            var Currencys = await DynamicSelect(CurrencyDAOs, filter);
            return Currencys;
        }

        public async Task<Currency> Get(Guid Id)
        {
            Currency Currency = await ERPContext.Currency.Where(l => l.Id == Id).Select(CurrencyDAO => new Currency()
            {
                 
                Id = CurrencyDAO.Id,
                BusinessGroupId = CurrencyDAO.BusinessGroupId,
                Code = CurrencyDAO.Code,
                Name = CurrencyDAO.Name,
                Sequence = CurrencyDAO.Sequence,
                Description = CurrencyDAO.Description,
            }).FirstOrDefaultAsync();
            return Currency;
        }

        public async Task<bool> Create(Currency Currency)
        {
            CurrencyDAO CurrencyDAO = new CurrencyDAO();
            
            CurrencyDAO.Id = Currency.Id;
            CurrencyDAO.BusinessGroupId = Currency.BusinessGroupId;
            CurrencyDAO.Code = Currency.Code;
            CurrencyDAO.Name = Currency.Name;
            CurrencyDAO.Sequence = Currency.Sequence;
            CurrencyDAO.Description = Currency.Description;
            CurrencyDAO.Disabled = false;
            
            await ERPContext.Currency.AddAsync(CurrencyDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Currency Currency)
        {
            CurrencyDAO CurrencyDAO = ERPContext.Currency.Where(b => b.Id == Currency.Id).FirstOrDefault();
            
            CurrencyDAO.Id = Currency.Id;
            CurrencyDAO.BusinessGroupId = Currency.BusinessGroupId;
            CurrencyDAO.Code = Currency.Code;
            CurrencyDAO.Name = Currency.Name;
            CurrencyDAO.Sequence = Currency.Sequence;
            CurrencyDAO.Description = Currency.Description;
            CurrencyDAO.Disabled = false;
            ERPContext.Currency.Update(CurrencyDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CurrencyDAO CurrencyDAO = await ERPContext.Currency.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CurrencyDAO.Disabled = true;
            ERPContext.Currency.Update(CurrencyDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
