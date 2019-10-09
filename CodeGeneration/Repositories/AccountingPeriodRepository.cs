
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
    public interface IAccountingPeriodRepository
    {
        Task<int> Count(AccountingPeriodFilter AccountingPeriodFilter);
        Task<List<AccountingPeriod>> List(AccountingPeriodFilter AccountingPeriodFilter);
        Task<AccountingPeriod> Get(Guid Id);
        Task<bool> Create(AccountingPeriod AccountingPeriod);
        Task<bool> Update(AccountingPeriod AccountingPeriod);
        Task<bool> Delete(Guid Id);
        
    }
    public class AccountingPeriodRepository : IAccountingPeriodRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public AccountingPeriodRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<AccountingPeriodDAO> DynamicFilter(IQueryable<AccountingPeriodDAO> query, AccountingPeriodFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.FiscalYearId != null)
                query = query.Where(q => q.FiscalYearId, filter.FiscalYearId);
            if (filter.StartPeriod != null)
                query = query.Where(q => q.StartPeriod, filter.StartPeriod);
            if (filter.EndPeriod != null)
                query = query.Where(q => q.EndPeriod, filter.EndPeriod);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<AccountingPeriodDAO> DynamicOrder(IQueryable<AccountingPeriodDAO> query,  AccountingPeriodFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case AccountingPeriodOrder.StartPeriod:
                            query = query.OrderBy(q => q.StartPeriod);
                            break;
                        case AccountingPeriodOrder.EndPeriod:
                            query = query.OrderBy(q => q.EndPeriod);
                            break;
                        case AccountingPeriodOrder.Description:
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
                        
                        case AccountingPeriodOrder.StartPeriod:
                            query = query.OrderByDescending(q => q.StartPeriod);
                            break;
                        case AccountingPeriodOrder.EndPeriod:
                            query = query.OrderByDescending(q => q.EndPeriod);
                            break;
                        case AccountingPeriodOrder.Description:
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

        private async Task<List<AccountingPeriod>> DynamicSelect(IQueryable<AccountingPeriodDAO> query, AccountingPeriodFilter filter)
        {
            List <AccountingPeriod> AccountingPeriods = await query.Select(q => new AccountingPeriod()
            {
                
                Id = filter.Selects.Contains(AccountingPeriodSelect.Id) ? q.Id : default(Guid),
                FiscalYearId = filter.Selects.Contains(AccountingPeriodSelect.FiscalYear) ? q.FiscalYearId : default(Guid),
                StartPeriod = filter.Selects.Contains(AccountingPeriodSelect.StartPeriod) ? q.StartPeriod : default(DateTime),
                EndPeriod = filter.Selects.Contains(AccountingPeriodSelect.EndPeriod) ? q.EndPeriod : default(DateTime),
                StatusId = filter.Selects.Contains(AccountingPeriodSelect.Status) ? q.StatusId : default(Guid),
                Description = filter.Selects.Contains(AccountingPeriodSelect.Description) ? q.Description : default(string),
                BusinessGroupId = filter.Selects.Contains(AccountingPeriodSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return AccountingPeriods;
        }

        public async Task<int> Count(AccountingPeriodFilter filter)
        {
            IQueryable <AccountingPeriodDAO> AccountingPeriodDAOs = ERPContext.AccountingPeriod;
            AccountingPeriodDAOs = DynamicFilter(AccountingPeriodDAOs, filter);
            return await AccountingPeriodDAOs.CountAsync();
        }

        public async Task<List<AccountingPeriod>> List(AccountingPeriodFilter filter)
        {
            if (filter == null) return new List<AccountingPeriod>();
            IQueryable<AccountingPeriodDAO> AccountingPeriodDAOs = ERPContext.AccountingPeriod;
            AccountingPeriodDAOs = DynamicFilter(AccountingPeriodDAOs, filter);
            AccountingPeriodDAOs = DynamicOrder(AccountingPeriodDAOs, filter);
            var AccountingPeriods = await DynamicSelect(AccountingPeriodDAOs, filter);
            return AccountingPeriods;
        }

        public async Task<AccountingPeriod> Get(Guid Id)
        {
            AccountingPeriod AccountingPeriod = await ERPContext.AccountingPeriod.Where(l => l.Id == Id).Select(AccountingPeriodDAO => new AccountingPeriod()
            {
                 
                Id = AccountingPeriodDAO.Id,
                FiscalYearId = AccountingPeriodDAO.FiscalYearId,
                StartPeriod = AccountingPeriodDAO.StartPeriod,
                EndPeriod = AccountingPeriodDAO.EndPeriod,
                StatusId = AccountingPeriodDAO.StatusId,
                Description = AccountingPeriodDAO.Description,
                BusinessGroupId = AccountingPeriodDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return AccountingPeriod;
        }

        public async Task<bool> Create(AccountingPeriod AccountingPeriod)
        {
            AccountingPeriodDAO AccountingPeriodDAO = new AccountingPeriodDAO();
            
            AccountingPeriodDAO.Id = AccountingPeriod.Id;
            AccountingPeriodDAO.FiscalYearId = AccountingPeriod.FiscalYearId;
            AccountingPeriodDAO.StartPeriod = AccountingPeriod.StartPeriod;
            AccountingPeriodDAO.EndPeriod = AccountingPeriod.EndPeriod;
            AccountingPeriodDAO.StatusId = AccountingPeriod.StatusId;
            AccountingPeriodDAO.Description = AccountingPeriod.Description;
            AccountingPeriodDAO.BusinessGroupId = AccountingPeriod.BusinessGroupId;
            AccountingPeriodDAO.Disabled = false;
            
            await ERPContext.AccountingPeriod.AddAsync(AccountingPeriodDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(AccountingPeriod AccountingPeriod)
        {
            AccountingPeriodDAO AccountingPeriodDAO = ERPContext.AccountingPeriod.Where(b => b.Id == AccountingPeriod.Id).FirstOrDefault();
            
            AccountingPeriodDAO.Id = AccountingPeriod.Id;
            AccountingPeriodDAO.FiscalYearId = AccountingPeriod.FiscalYearId;
            AccountingPeriodDAO.StartPeriod = AccountingPeriod.StartPeriod;
            AccountingPeriodDAO.EndPeriod = AccountingPeriod.EndPeriod;
            AccountingPeriodDAO.StatusId = AccountingPeriod.StatusId;
            AccountingPeriodDAO.Description = AccountingPeriod.Description;
            AccountingPeriodDAO.BusinessGroupId = AccountingPeriod.BusinessGroupId;
            AccountingPeriodDAO.Disabled = false;
            ERPContext.AccountingPeriod.Update(AccountingPeriodDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            AccountingPeriodDAO AccountingPeriodDAO = await ERPContext.AccountingPeriod.Where(x => x.Id == Id).FirstOrDefaultAsync();
            AccountingPeriodDAO.Disabled = true;
            ERPContext.AccountingPeriod.Update(AccountingPeriodDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
