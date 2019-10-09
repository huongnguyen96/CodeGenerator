
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
    public interface ICustomerBankAccountRepository
    {
        Task<int> Count(CustomerBankAccountFilter CustomerBankAccountFilter);
        Task<List<CustomerBankAccount>> List(CustomerBankAccountFilter CustomerBankAccountFilter);
        Task<CustomerBankAccount> Get(Guid Id);
        Task<bool> Create(CustomerBankAccount CustomerBankAccount);
        Task<bool> Update(CustomerBankAccount CustomerBankAccount);
        Task<bool> Delete(Guid Id);
        
    }
    public class CustomerBankAccountRepository : ICustomerBankAccountRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CustomerBankAccountRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CustomerBankAccountDAO> DynamicFilter(IQueryable<CustomerBankAccountDAO> query, CustomerBankAccountFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerDetailId != null)
                query = query.Where(q => q.CustomerDetailId, filter.CustomerDetailId);
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
        private IQueryable<CustomerBankAccountDAO> DynamicOrder(IQueryable<CustomerBankAccountDAO> query,  CustomerBankAccountFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CustomerBankAccountOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerBankAccountOrder.AccountNumber:
                            query = query.OrderBy(q => q.AccountNumber);
                            break;
                        case CustomerBankAccountOrder.AccountName:
                            query = query.OrderBy(q => q.AccountName);
                            break;
                        case CustomerBankAccountOrder.Branch:
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
                        
                        case CustomerBankAccountOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerBankAccountOrder.AccountNumber:
                            query = query.OrderByDescending(q => q.AccountNumber);
                            break;
                        case CustomerBankAccountOrder.AccountName:
                            query = query.OrderByDescending(q => q.AccountName);
                            break;
                        case CustomerBankAccountOrder.Branch:
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

        private async Task<List<CustomerBankAccount>> DynamicSelect(IQueryable<CustomerBankAccountDAO> query, CustomerBankAccountFilter filter)
        {
            List <CustomerBankAccount> CustomerBankAccounts = await query.Select(q => new CustomerBankAccount()
            {
                
                Id = filter.Selects.Contains(CustomerBankAccountSelect.Id) ? q.Id : default(Guid),
                CustomerDetailId = filter.Selects.Contains(CustomerBankAccountSelect.CustomerDetail) ? q.CustomerDetailId : default(Guid),
                Name = filter.Selects.Contains(CustomerBankAccountSelect.Name) ? q.Name : default(string),
                BankId = filter.Selects.Contains(CustomerBankAccountSelect.Bank) ? q.BankId : default(Guid),
                AccountNumber = filter.Selects.Contains(CustomerBankAccountSelect.AccountNumber) ? q.AccountNumber : default(string),
                AccountName = filter.Selects.Contains(CustomerBankAccountSelect.AccountName) ? q.AccountName : default(string),
                Branch = filter.Selects.Contains(CustomerBankAccountSelect.Branch) ? q.Branch : default(string),
                ProvinceId = filter.Selects.Contains(CustomerBankAccountSelect.Province) ? q.ProvinceId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CustomerBankAccountSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return CustomerBankAccounts;
        }

        public async Task<int> Count(CustomerBankAccountFilter filter)
        {
            IQueryable <CustomerBankAccountDAO> CustomerBankAccountDAOs = ERPContext.CustomerBankAccount;
            CustomerBankAccountDAOs = DynamicFilter(CustomerBankAccountDAOs, filter);
            return await CustomerBankAccountDAOs.CountAsync();
        }

        public async Task<List<CustomerBankAccount>> List(CustomerBankAccountFilter filter)
        {
            if (filter == null) return new List<CustomerBankAccount>();
            IQueryable<CustomerBankAccountDAO> CustomerBankAccountDAOs = ERPContext.CustomerBankAccount;
            CustomerBankAccountDAOs = DynamicFilter(CustomerBankAccountDAOs, filter);
            CustomerBankAccountDAOs = DynamicOrder(CustomerBankAccountDAOs, filter);
            var CustomerBankAccounts = await DynamicSelect(CustomerBankAccountDAOs, filter);
            return CustomerBankAccounts;
        }

        public async Task<CustomerBankAccount> Get(Guid Id)
        {
            CustomerBankAccount CustomerBankAccount = await ERPContext.CustomerBankAccount.Where(l => l.Id == Id).Select(CustomerBankAccountDAO => new CustomerBankAccount()
            {
                 
                Id = CustomerBankAccountDAO.Id,
                CustomerDetailId = CustomerBankAccountDAO.CustomerDetailId,
                Name = CustomerBankAccountDAO.Name,
                BankId = CustomerBankAccountDAO.BankId,
                AccountNumber = CustomerBankAccountDAO.AccountNumber,
                AccountName = CustomerBankAccountDAO.AccountName,
                Branch = CustomerBankAccountDAO.Branch,
                ProvinceId = CustomerBankAccountDAO.ProvinceId,
                BusinessGroupId = CustomerBankAccountDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return CustomerBankAccount;
        }

        public async Task<bool> Create(CustomerBankAccount CustomerBankAccount)
        {
            CustomerBankAccountDAO CustomerBankAccountDAO = new CustomerBankAccountDAO();
            
            CustomerBankAccountDAO.Id = CustomerBankAccount.Id;
            CustomerBankAccountDAO.CustomerDetailId = CustomerBankAccount.CustomerDetailId;
            CustomerBankAccountDAO.Name = CustomerBankAccount.Name;
            CustomerBankAccountDAO.BankId = CustomerBankAccount.BankId;
            CustomerBankAccountDAO.AccountNumber = CustomerBankAccount.AccountNumber;
            CustomerBankAccountDAO.AccountName = CustomerBankAccount.AccountName;
            CustomerBankAccountDAO.Branch = CustomerBankAccount.Branch;
            CustomerBankAccountDAO.ProvinceId = CustomerBankAccount.ProvinceId;
            CustomerBankAccountDAO.BusinessGroupId = CustomerBankAccount.BusinessGroupId;
            CustomerBankAccountDAO.Disabled = false;
            
            await ERPContext.CustomerBankAccount.AddAsync(CustomerBankAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CustomerBankAccount CustomerBankAccount)
        {
            CustomerBankAccountDAO CustomerBankAccountDAO = ERPContext.CustomerBankAccount.Where(b => b.Id == CustomerBankAccount.Id).FirstOrDefault();
            
            CustomerBankAccountDAO.Id = CustomerBankAccount.Id;
            CustomerBankAccountDAO.CustomerDetailId = CustomerBankAccount.CustomerDetailId;
            CustomerBankAccountDAO.Name = CustomerBankAccount.Name;
            CustomerBankAccountDAO.BankId = CustomerBankAccount.BankId;
            CustomerBankAccountDAO.AccountNumber = CustomerBankAccount.AccountNumber;
            CustomerBankAccountDAO.AccountName = CustomerBankAccount.AccountName;
            CustomerBankAccountDAO.Branch = CustomerBankAccount.Branch;
            CustomerBankAccountDAO.ProvinceId = CustomerBankAccount.ProvinceId;
            CustomerBankAccountDAO.BusinessGroupId = CustomerBankAccount.BusinessGroupId;
            CustomerBankAccountDAO.Disabled = false;
            ERPContext.CustomerBankAccount.Update(CustomerBankAccountDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CustomerBankAccountDAO CustomerBankAccountDAO = await ERPContext.CustomerBankAccount.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CustomerBankAccountDAO.Disabled = true;
            ERPContext.CustomerBankAccount.Update(CustomerBankAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
