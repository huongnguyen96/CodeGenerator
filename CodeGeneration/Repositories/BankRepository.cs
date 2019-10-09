
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
    public interface IBankRepository
    {
        Task<int> Count(BankFilter BankFilter);
        Task<List<Bank>> List(BankFilter BankFilter);
        Task<Bank> Get(Guid Id);
        Task<bool> Create(Bank Bank);
        Task<bool> Update(Bank Bank);
        Task<bool> Delete(Guid Id);
        
    }
    public class BankRepository : IBankRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public BankRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<BankDAO> DynamicFilter(IQueryable<BankDAO> query, BankFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<BankDAO> DynamicOrder(IQueryable<BankDAO> query,  BankFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case BankOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case BankOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case BankOrder.Description:
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
                        
                        case BankOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case BankOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case BankOrder.Description:
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

        private async Task<List<Bank>> DynamicSelect(IQueryable<BankDAO> query, BankFilter filter)
        {
            List <Bank> Banks = await query.Select(q => new Bank()
            {
                
                Id = filter.Selects.Contains(BankSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(BankSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(BankSelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(BankSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Description = filter.Selects.Contains(BankSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return Banks;
        }

        public async Task<int> Count(BankFilter filter)
        {
            IQueryable <BankDAO> BankDAOs = ERPContext.Bank;
            BankDAOs = DynamicFilter(BankDAOs, filter);
            return await BankDAOs.CountAsync();
        }

        public async Task<List<Bank>> List(BankFilter filter)
        {
            if (filter == null) return new List<Bank>();
            IQueryable<BankDAO> BankDAOs = ERPContext.Bank;
            BankDAOs = DynamicFilter(BankDAOs, filter);
            BankDAOs = DynamicOrder(BankDAOs, filter);
            var Banks = await DynamicSelect(BankDAOs, filter);
            return Banks;
        }

        public async Task<Bank> Get(Guid Id)
        {
            Bank Bank = await ERPContext.Bank.Where(l => l.Id == Id).Select(BankDAO => new Bank()
            {
                 
                Id = BankDAO.Id,
                Code = BankDAO.Code,
                Name = BankDAO.Name,
                BusinessGroupId = BankDAO.BusinessGroupId,
                Description = BankDAO.Description,
            }).FirstOrDefaultAsync();
            return Bank;
        }

        public async Task<bool> Create(Bank Bank)
        {
            BankDAO BankDAO = new BankDAO();
            
            BankDAO.Id = Bank.Id;
            BankDAO.Code = Bank.Code;
            BankDAO.Name = Bank.Name;
            BankDAO.BusinessGroupId = Bank.BusinessGroupId;
            BankDAO.Description = Bank.Description;
            BankDAO.Disabled = false;
            
            await ERPContext.Bank.AddAsync(BankDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Bank Bank)
        {
            BankDAO BankDAO = ERPContext.Bank.Where(b => b.Id == Bank.Id).FirstOrDefault();
            
            BankDAO.Id = Bank.Id;
            BankDAO.Code = Bank.Code;
            BankDAO.Name = Bank.Name;
            BankDAO.BusinessGroupId = Bank.BusinessGroupId;
            BankDAO.Description = Bank.Description;
            BankDAO.Disabled = false;
            ERPContext.Bank.Update(BankDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            BankDAO BankDAO = await ERPContext.Bank.Where(x => x.Id == Id).FirstOrDefaultAsync();
            BankDAO.Disabled = true;
            ERPContext.Bank.Update(BankDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
