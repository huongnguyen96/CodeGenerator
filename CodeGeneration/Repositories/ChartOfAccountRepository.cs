
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
    public interface IChartOfAccountRepository
    {
        Task<int> Count(ChartOfAccountFilter ChartOfAccountFilter);
        Task<List<ChartOfAccount>> List(ChartOfAccountFilter ChartOfAccountFilter);
        Task<ChartOfAccount> Get(Guid Id);
        Task<bool> Create(ChartOfAccount ChartOfAccount);
        Task<bool> Update(ChartOfAccount ChartOfAccount);
        Task<bool> Delete(Guid Id);
        
    }
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ChartOfAccountRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ChartOfAccountDAO> DynamicFilter(IQueryable<ChartOfAccountDAO> query, ChartOfAccountFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.ParentId.HasValue)
                query = query.Where(q => q.ParentId.HasValue && q.ParentId.Value == filter.ParentId.Value);
            if (filter.ParentId != null)
                query = query.Where(q => q.ParentId, filter.ParentId);
            if (filter.AccountCode != null)
                query = query.Where(q => q.AccountCode, filter.AccountCode);
            if (filter.AliasCode != null)
                query = query.Where(q => q.AliasCode, filter.AliasCode);
            if (filter.AccountName != null)
                query = query.Where(q => q.AccountName, filter.AccountName);
            if (filter.AccountDescription != null)
                query = query.Where(q => q.AccountDescription, filter.AccountDescription);
            if (filter.Characteristic.HasValue)
                query = query.Where(q => q.Characteristic.HasValue && q.Characteristic.Value == filter.Characteristic.Value);
            if (filter.Characteristic != null)
                query = query.Where(q => q.Characteristic, filter.Characteristic);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<ChartOfAccountDAO> DynamicOrder(IQueryable<ChartOfAccountDAO> query,  ChartOfAccountFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ChartOfAccountOrder.ParentId:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        case ChartOfAccountOrder.AccountCode:
                            query = query.OrderBy(q => q.AccountCode);
                            break;
                        case ChartOfAccountOrder.AliasCode:
                            query = query.OrderBy(q => q.AliasCode);
                            break;
                        case ChartOfAccountOrder.AccountName:
                            query = query.OrderBy(q => q.AccountName);
                            break;
                        case ChartOfAccountOrder.AccountDescription:
                            query = query.OrderBy(q => q.AccountDescription);
                            break;
                        case ChartOfAccountOrder.Characteristic:
                            query = query.OrderBy(q => q.Characteristic);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ChartOfAccountOrder.ParentId:
                            query = query.OrderByDescending(q => q.ParentId);
                            break;
                        case ChartOfAccountOrder.AccountCode:
                            query = query.OrderByDescending(q => q.AccountCode);
                            break;
                        case ChartOfAccountOrder.AliasCode:
                            query = query.OrderByDescending(q => q.AliasCode);
                            break;
                        case ChartOfAccountOrder.AccountName:
                            query = query.OrderByDescending(q => q.AccountName);
                            break;
                        case ChartOfAccountOrder.AccountDescription:
                            query = query.OrderByDescending(q => q.AccountDescription);
                            break;
                        case ChartOfAccountOrder.Characteristic:
                            query = query.OrderByDescending(q => q.Characteristic);
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

        private async Task<List<ChartOfAccount>> DynamicSelect(IQueryable<ChartOfAccountDAO> query, ChartOfAccountFilter filter)
        {
            List <ChartOfAccount> ChartOfAccounts = await query.Select(q => new ChartOfAccount()
            {
                
                Id = filter.Selects.Contains(ChartOfAccountSelect.Id) ? q.Id : default(Guid),
                SetOfBookId = filter.Selects.Contains(ChartOfAccountSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                ParentId = filter.Selects.Contains(ChartOfAccountSelect.Parent) ? q.ParentId : default(Guid?),
                AccountCode = filter.Selects.Contains(ChartOfAccountSelect.AccountCode) ? q.AccountCode : default(string),
                AliasCode = filter.Selects.Contains(ChartOfAccountSelect.AliasCode) ? q.AliasCode : default(string),
                AccountName = filter.Selects.Contains(ChartOfAccountSelect.AccountName) ? q.AccountName : default(string),
                AccountDescription = filter.Selects.Contains(ChartOfAccountSelect.AccountDescription) ? q.AccountDescription : default(string),
                Characteristic = filter.Selects.Contains(ChartOfAccountSelect.Characteristic) ? q.Characteristic : default(Guid?),
                BusinessGroupId = filter.Selects.Contains(ChartOfAccountSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return ChartOfAccounts;
        }

        public async Task<int> Count(ChartOfAccountFilter filter)
        {
            IQueryable <ChartOfAccountDAO> ChartOfAccountDAOs = ERPContext.ChartOfAccount;
            ChartOfAccountDAOs = DynamicFilter(ChartOfAccountDAOs, filter);
            return await ChartOfAccountDAOs.CountAsync();
        }

        public async Task<List<ChartOfAccount>> List(ChartOfAccountFilter filter)
        {
            if (filter == null) return new List<ChartOfAccount>();
            IQueryable<ChartOfAccountDAO> ChartOfAccountDAOs = ERPContext.ChartOfAccount;
            ChartOfAccountDAOs = DynamicFilter(ChartOfAccountDAOs, filter);
            ChartOfAccountDAOs = DynamicOrder(ChartOfAccountDAOs, filter);
            var ChartOfAccounts = await DynamicSelect(ChartOfAccountDAOs, filter);
            return ChartOfAccounts;
        }

        public async Task<ChartOfAccount> Get(Guid Id)
        {
            ChartOfAccount ChartOfAccount = await ERPContext.ChartOfAccount.Where(l => l.Id == Id).Select(ChartOfAccountDAO => new ChartOfAccount()
            {
                 
                Id = ChartOfAccountDAO.Id,
                SetOfBookId = ChartOfAccountDAO.SetOfBookId,
                ParentId = ChartOfAccountDAO.ParentId,
                AccountCode = ChartOfAccountDAO.AccountCode,
                AliasCode = ChartOfAccountDAO.AliasCode,
                AccountName = ChartOfAccountDAO.AccountName,
                AccountDescription = ChartOfAccountDAO.AccountDescription,
                Characteristic = ChartOfAccountDAO.Characteristic,
                BusinessGroupId = ChartOfAccountDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return ChartOfAccount;
        }

        public async Task<bool> Create(ChartOfAccount ChartOfAccount)
        {
            ChartOfAccountDAO ChartOfAccountDAO = new ChartOfAccountDAO();
            
            ChartOfAccountDAO.Id = ChartOfAccount.Id;
            ChartOfAccountDAO.SetOfBookId = ChartOfAccount.SetOfBookId;
            ChartOfAccountDAO.ParentId = ChartOfAccount.ParentId;
            ChartOfAccountDAO.AccountCode = ChartOfAccount.AccountCode;
            ChartOfAccountDAO.AliasCode = ChartOfAccount.AliasCode;
            ChartOfAccountDAO.AccountName = ChartOfAccount.AccountName;
            ChartOfAccountDAO.AccountDescription = ChartOfAccount.AccountDescription;
            ChartOfAccountDAO.Characteristic = ChartOfAccount.Characteristic;
            ChartOfAccountDAO.BusinessGroupId = ChartOfAccount.BusinessGroupId;
            ChartOfAccountDAO.Disabled = false;
            
            await ERPContext.ChartOfAccount.AddAsync(ChartOfAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ChartOfAccount ChartOfAccount)
        {
            ChartOfAccountDAO ChartOfAccountDAO = ERPContext.ChartOfAccount.Where(b => b.Id == ChartOfAccount.Id).FirstOrDefault();
            
            ChartOfAccountDAO.Id = ChartOfAccount.Id;
            ChartOfAccountDAO.SetOfBookId = ChartOfAccount.SetOfBookId;
            ChartOfAccountDAO.ParentId = ChartOfAccount.ParentId;
            ChartOfAccountDAO.AccountCode = ChartOfAccount.AccountCode;
            ChartOfAccountDAO.AliasCode = ChartOfAccount.AliasCode;
            ChartOfAccountDAO.AccountName = ChartOfAccount.AccountName;
            ChartOfAccountDAO.AccountDescription = ChartOfAccount.AccountDescription;
            ChartOfAccountDAO.Characteristic = ChartOfAccount.Characteristic;
            ChartOfAccountDAO.BusinessGroupId = ChartOfAccount.BusinessGroupId;
            ChartOfAccountDAO.Disabled = false;
            ERPContext.ChartOfAccount.Update(ChartOfAccountDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ChartOfAccountDAO ChartOfAccountDAO = await ERPContext.ChartOfAccount.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ChartOfAccountDAO.Disabled = true;
            ERPContext.ChartOfAccount.Update(ChartOfAccountDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
