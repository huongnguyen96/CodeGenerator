
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
    public interface ISupplierBankAccountRepository
    {
        Task<int> Count(SupplierBankAccountFilter SupplierBankAccountFilter);
        Task<List<SupplierBankAccount>> List(SupplierBankAccountFilter SupplierBankAccountFilter);
        Task<SupplierBankAccount> Get(Guid Id);
        Task<bool> Create(SupplierBankAccount SupplierBankAccount);
        Task<bool> Update(SupplierBankAccount SupplierBankAccount);
        Task<bool> Delete(Guid Id);
        
    }
    public class SupplierBankAccountRepository : ISupplierBankAccountRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SupplierBankAccountRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SupplierBankAccountDAO> DynamicFilter(IQueryable<SupplierBankAccountDAO> query, SupplierBankAccountFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.SupplierDetailId != null)
                query = query.Where(q => q.SupplierDetailId, filter.SupplierDetailId);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.BankId != null)
                query = query.Where(q => q.BankId, filter.BankId);
            if (filter.AccountNumber != null)
                query = query.Where(q => q.AccountNumber, filter.AccountNumber);
            if (filter.AccountName != null)
                query = query.Where(q => q.AccountName, filter.AccountName);
            if (filter.Branch != null)
                query = query.Where(q => q.Branch, filter.Branch);
            if (filter.ProvinceId != null)
                query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<SupplierBankAccountDAO> DynamicOrder(IQueryable<SupplierBankAccountDAO> query,  SupplierBankAccountFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierBankAccountOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SupplierBankAccountOrder.AccountNumber:
                            query = query.OrderBy(q => q.AccountNumber);
                            break;
                        case SupplierBankAccountOrder.AccountName:
                            query = query.OrderBy(q => q.AccountName);
                            break;
                        case SupplierBankAccountOrder.Branch:
                            query = query.OrderBy(q => q.Branch);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SupplierBankAccountOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SupplierBankAccountOrder.AccountNumber:
                            query = query.OrderByDescending(q => q.AccountNumber);
                            break;
                        case SupplierBankAccountOrder.AccountName:
                            query = query.OrderByDescending(q => q.AccountName);
                            break;
                        case SupplierBankAccountOrder.Branch:
                            query = query.OrderByDescending(q => q.Branch);
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

        private async Task<List<SupplierBankAccount>> DynamicSelect(IQueryable<SupplierBankAccountDAO> query, SupplierBankAccountFilter filter)
        {
            List <SupplierBankAccount> SupplierBankAccounts = await query.Select(q => new SupplierBankAccount()
            {
                
                Id = filter.Selects.Contains(SupplierBankAccountSelect.Id) ? q.Id : default(Guid),
                SupplierDetailId = filter.Selects.Contains(SupplierBankAccountSelect.SupplierDetail) ? q.SupplierDetailId : default(Guid),
                Name = filter.Selects.Contains(SupplierBankAccountSelect.Name) ? q.Name : default(string),
                BankId = filter.Selects.Contains(SupplierBankAccountSelect.Bank) ? q.BankId : default(Guid),
                AccountNumber = filter.Selects.Contains(SupplierBankAccountSelect.AccountNumber) ? q.AccountNumber : default(string),
                AccountName = filter.Selects.Contains(SupplierBankAccountSelect.AccountName) ? q.AccountName : default(string),
                Branch = filter.Selects.Contains(SupplierBankAccountSelect.Branch) ? q.Branch : default(string),
                ProvinceId = filter.Selects.Contains(SupplierBankAccountSelect.Province) ? q.ProvinceId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(SupplierBankAccountSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return SupplierBankAccounts;
        }

        public async Task<int> Count(SupplierBankAccountFilter filter)
        {
            IQueryable <SupplierBankAccountDAO> SupplierBankAccountDAOs = ERPContext.SupplierBankAccount;
            SupplierBankAccountDAOs = DynamicFilter(SupplierBankAccountDAOs, filter);
            return await SupplierBankAccountDAOs.CountAsync();
        }

        public async Task<List<SupplierBankAccount>> List(SupplierBankAccountFilter filter)
        {
            if (filter == null) return new List<SupplierBankAccount>();
            IQueryable<SupplierBankAccountDAO> SupplierBankAccountDAOs = ERPContext.SupplierBankAccount;
            SupplierBankAccountDAOs = DynamicFilter(SupplierBankAccountDAOs, filter);
            SupplierBankAccountDAOs = DynamicOrder(SupplierBankAccountDAOs, filter);
            var SupplierBankAccounts = await DynamicSelect(SupplierBankAccountDAOs, filter);
            return SupplierBankAccounts;
        }

        public async Task<SupplierBankAccount> Get(Guid Id)
        {
            SupplierBankAccount SupplierBankAccount = await ERPContext.SupplierBankAccount.Where(l => l.Id == Id).Select(SupplierBankAccountDAO => new SupplierBankAccount()
            {
                 
                Id = SupplierBankAccountDAO.Id,
                SupplierDetailId = SupplierBankAccountDAO.SupplierDetailId,
                Name = SupplierBankAccountDAO.Name,
                BankId = SupplierBankAccountDAO.BankId,
                AccountNumber = SupplierBankAccountDAO.AccountNumber,
                AccountName = SupplierBankAccountDAO.AccountName,
                Branch = SupplierBankAccountDAO.Branch,
                ProvinceId = SupplierBankAccountDAO.ProvinceId,
                BusinessGroupId = SupplierBankAccountDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return SupplierBankAccount;
        }

        public async Task<bool> Create(SupplierBankAccount SupplierBankAccount)
        {
            SupplierBankAccountDAO SupplierBankAccountDAO = new SupplierBankAccountDAO();
            
            SupplierBankAccountDAO.Id = SupplierBankAccount.Id;
            SupplierBankAccountDAO.SupplierDetailId = SupplierBankAccount.SupplierDetailId;
            SupplierBankAccountDAO.Name = SupplierBankAccount.Name;
            SupplierBankAccountDAO.BankId = SupplierBankAccount.BankId;
            SupplierBankAccountDAO.AccountNumber = SupplierBankAccount.AccountNumber;
            SupplierBankAccountDAO.AccountName = SupplierBankAccount.AccountName;
            SupplierBankAccountDAO.Branch = SupplierBankAccount.Branch;
            SupplierBankAccountDAO.ProvinceId = SupplierBankAccount.ProvinceId;
            SupplierBankAccountDAO.BusinessGroupId = SupplierBankAccount.BusinessGroupId;
            SupplierBankAccountDAO.Disabled = false;
            
            await ERPContext.SupplierBankAccount.AddAsync(SupplierBankAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SupplierBankAccount SupplierBankAccount)
        {
            SupplierBankAccountDAO SupplierBankAccountDAO = ERPContext.SupplierBankAccount.Where(b => b.Id == SupplierBankAccount.Id).FirstOrDefault();
            
            SupplierBankAccountDAO.Id = SupplierBankAccount.Id;
            SupplierBankAccountDAO.SupplierDetailId = SupplierBankAccount.SupplierDetailId;
            SupplierBankAccountDAO.Name = SupplierBankAccount.Name;
            SupplierBankAccountDAO.BankId = SupplierBankAccount.BankId;
            SupplierBankAccountDAO.AccountNumber = SupplierBankAccount.AccountNumber;
            SupplierBankAccountDAO.AccountName = SupplierBankAccount.AccountName;
            SupplierBankAccountDAO.Branch = SupplierBankAccount.Branch;
            SupplierBankAccountDAO.ProvinceId = SupplierBankAccount.ProvinceId;
            SupplierBankAccountDAO.BusinessGroupId = SupplierBankAccount.BusinessGroupId;
            SupplierBankAccountDAO.Disabled = false;
            ERPContext.SupplierBankAccount.Update(SupplierBankAccountDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SupplierBankAccountDAO SupplierBankAccountDAO = await ERPContext.SupplierBankAccount.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SupplierBankAccountDAO.Disabled = true;
            ERPContext.SupplierBankAccount.Update(SupplierBankAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
