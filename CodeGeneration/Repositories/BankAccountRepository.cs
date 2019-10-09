
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
    public interface IBankAccountRepository
    {
        Task<int> Count(BankAccountFilter BankAccountFilter);
        Task<List<BankAccount>> List(BankAccountFilter BankAccountFilter);
        Task<BankAccount> Get(Guid Id);
        Task<bool> Create(BankAccount BankAccount);
        Task<bool> Update(BankAccount BankAccount);
        Task<bool> Delete(Guid Id);
        
    }
    public class BankAccountRepository : IBankAccountRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public BankAccountRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<BankAccountDAO> DynamicFilter(IQueryable<BankAccountDAO> query, BankAccountFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BankId != null)
                query = query.Where(q => q.BankId, filter.BankId);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.No != null)
                query = query.Where(q => q.No, filter.No);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.ChartOfAccountId != null)
                query = query.Where(q => q.ChartOfAccountId, filter.ChartOfAccountId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<BankAccountDAO> DynamicOrder(IQueryable<BankAccountDAO> query,  BankAccountFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case BankAccountOrder.No:
                            query = query.OrderBy(q => q.No);
                            break;
                        case BankAccountOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case BankAccountOrder.Description:
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
                        
                        case BankAccountOrder.No:
                            query = query.OrderByDescending(q => q.No);
                            break;
                        case BankAccountOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case BankAccountOrder.Description:
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

        private async Task<List<BankAccount>> DynamicSelect(IQueryable<BankAccountDAO> query, BankAccountFilter filter)
        {
            List <BankAccount> BankAccounts = await query.Select(q => new BankAccount()
            {
                
                Id = filter.Selects.Contains(BankAccountSelect.Id) ? q.Id : default(Guid),
                BankId = filter.Selects.Contains(BankAccountSelect.Bank) ? q.BankId : default(Guid),
                SetOfBookId = filter.Selects.Contains(BankAccountSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                No = filter.Selects.Contains(BankAccountSelect.No) ? q.No : default(string),
                Name = filter.Selects.Contains(BankAccountSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(BankAccountSelect.Description) ? q.Description : default(string),
                ChartOfAccountId = filter.Selects.Contains(BankAccountSelect.ChartOfAccount) ? q.ChartOfAccountId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(BankAccountSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return BankAccounts;
        }

        public async Task<int> Count(BankAccountFilter filter)
        {
            IQueryable <BankAccountDAO> BankAccountDAOs = ERPContext.BankAccount;
            BankAccountDAOs = DynamicFilter(BankAccountDAOs, filter);
            return await BankAccountDAOs.CountAsync();
        }

        public async Task<List<BankAccount>> List(BankAccountFilter filter)
        {
            if (filter == null) return new List<BankAccount>();
            IQueryable<BankAccountDAO> BankAccountDAOs = ERPContext.BankAccount;
            BankAccountDAOs = DynamicFilter(BankAccountDAOs, filter);
            BankAccountDAOs = DynamicOrder(BankAccountDAOs, filter);
            var BankAccounts = await DynamicSelect(BankAccountDAOs, filter);
            return BankAccounts;
        }

        public async Task<BankAccount> Get(Guid Id)
        {
            BankAccount BankAccount = await ERPContext.BankAccount.Where(l => l.Id == Id).Select(BankAccountDAO => new BankAccount()
            {
                 
                Id = BankAccountDAO.Id,
                BankId = BankAccountDAO.BankId,
                SetOfBookId = BankAccountDAO.SetOfBookId,
                No = BankAccountDAO.No,
                Name = BankAccountDAO.Name,
                Description = BankAccountDAO.Description,
                ChartOfAccountId = BankAccountDAO.ChartOfAccountId,
                BusinessGroupId = BankAccountDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return BankAccount;
        }

        public async Task<bool> Create(BankAccount BankAccount)
        {
            BankAccountDAO BankAccountDAO = new BankAccountDAO();
            
            BankAccountDAO.Id = BankAccount.Id;
            BankAccountDAO.BankId = BankAccount.BankId;
            BankAccountDAO.SetOfBookId = BankAccount.SetOfBookId;
            BankAccountDAO.No = BankAccount.No;
            BankAccountDAO.Name = BankAccount.Name;
            BankAccountDAO.Description = BankAccount.Description;
            BankAccountDAO.ChartOfAccountId = BankAccount.ChartOfAccountId;
            BankAccountDAO.BusinessGroupId = BankAccount.BusinessGroupId;
            BankAccountDAO.Disabled = false;
            
            await ERPContext.BankAccount.AddAsync(BankAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(BankAccount BankAccount)
        {
            BankAccountDAO BankAccountDAO = ERPContext.BankAccount.Where(b => b.Id == BankAccount.Id).FirstOrDefault();
            
            BankAccountDAO.Id = BankAccount.Id;
            BankAccountDAO.BankId = BankAccount.BankId;
            BankAccountDAO.SetOfBookId = BankAccount.SetOfBookId;
            BankAccountDAO.No = BankAccount.No;
            BankAccountDAO.Name = BankAccount.Name;
            BankAccountDAO.Description = BankAccount.Description;
            BankAccountDAO.ChartOfAccountId = BankAccount.ChartOfAccountId;
            BankAccountDAO.BusinessGroupId = BankAccount.BusinessGroupId;
            BankAccountDAO.Disabled = false;
            ERPContext.BankAccount.Update(BankAccountDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            BankAccountDAO BankAccountDAO = await ERPContext.BankAccount.Where(x => x.Id == Id).FirstOrDefaultAsync();
            BankAccountDAO.Disabled = true;
            ERPContext.BankAccount.Update(BankAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
