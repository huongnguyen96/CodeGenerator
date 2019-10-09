
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
    public interface IFiscalYearRepository
    {
        Task<int> Count(FiscalYearFilter FiscalYearFilter);
        Task<List<FiscalYear>> List(FiscalYearFilter FiscalYearFilter);
        Task<FiscalYear> Get(Guid Id);
        Task<bool> Create(FiscalYear FiscalYear);
        Task<bool> Update(FiscalYear FiscalYear);
        Task<bool> Delete(Guid Id);
        
    }
    public class FiscalYearRepository : IFiscalYearRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public FiscalYearRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<FiscalYearDAO> DynamicFilter(IQueryable<FiscalYearDAO> query, FiscalYearFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.StartDate != null)
                query = query.Where(q => q.StartDate, filter.StartDate);
            if (filter.EndDate != null)
                query = query.Where(q => q.EndDate, filter.EndDate);
            if (filter.InventoryValuationMethod != null)
                query = query.Where(q => q.InventoryValuationMethod, filter.InventoryValuationMethod);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<FiscalYearDAO> DynamicOrder(IQueryable<FiscalYearDAO> query,  FiscalYearFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case FiscalYearOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case FiscalYearOrder.StartDate:
                            query = query.OrderBy(q => q.StartDate);
                            break;
                        case FiscalYearOrder.EndDate:
                            query = query.OrderBy(q => q.EndDate);
                            break;
                        case FiscalYearOrder.InventoryValuationMethod:
                            query = query.OrderBy(q => q.InventoryValuationMethod);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case FiscalYearOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case FiscalYearOrder.StartDate:
                            query = query.OrderByDescending(q => q.StartDate);
                            break;
                        case FiscalYearOrder.EndDate:
                            query = query.OrderByDescending(q => q.EndDate);
                            break;
                        case FiscalYearOrder.InventoryValuationMethod:
                            query = query.OrderByDescending(q => q.InventoryValuationMethod);
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

        private async Task<List<FiscalYear>> DynamicSelect(IQueryable<FiscalYearDAO> query, FiscalYearFilter filter)
        {
            List <FiscalYear> FiscalYears = await query.Select(q => new FiscalYear()
            {
                
                Id = filter.Selects.Contains(FiscalYearSelect.Id) ? q.Id : default(Guid),
                SetOfBookId = filter.Selects.Contains(FiscalYearSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                Name = filter.Selects.Contains(FiscalYearSelect.Name) ? q.Name : default(string),
                StartDate = filter.Selects.Contains(FiscalYearSelect.StartDate) ? q.StartDate : default(DateTime),
                EndDate = filter.Selects.Contains(FiscalYearSelect.EndDate) ? q.EndDate : default(DateTime),
                InventoryValuationMethod = filter.Selects.Contains(FiscalYearSelect.InventoryValuationMethod) ? q.InventoryValuationMethod : default(string),
                StatusId = filter.Selects.Contains(FiscalYearSelect.Status) ? q.StatusId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(FiscalYearSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return FiscalYears;
        }

        public async Task<int> Count(FiscalYearFilter filter)
        {
            IQueryable <FiscalYearDAO> FiscalYearDAOs = ERPContext.FiscalYear;
            FiscalYearDAOs = DynamicFilter(FiscalYearDAOs, filter);
            return await FiscalYearDAOs.CountAsync();
        }

        public async Task<List<FiscalYear>> List(FiscalYearFilter filter)
        {
            if (filter == null) return new List<FiscalYear>();
            IQueryable<FiscalYearDAO> FiscalYearDAOs = ERPContext.FiscalYear;
            FiscalYearDAOs = DynamicFilter(FiscalYearDAOs, filter);
            FiscalYearDAOs = DynamicOrder(FiscalYearDAOs, filter);
            var FiscalYears = await DynamicSelect(FiscalYearDAOs, filter);
            return FiscalYears;
        }

        public async Task<FiscalYear> Get(Guid Id)
        {
            FiscalYear FiscalYear = await ERPContext.FiscalYear.Where(l => l.Id == Id).Select(FiscalYearDAO => new FiscalYear()
            {
                 
                Id = FiscalYearDAO.Id,
                SetOfBookId = FiscalYearDAO.SetOfBookId,
                Name = FiscalYearDAO.Name,
                StartDate = FiscalYearDAO.StartDate,
                EndDate = FiscalYearDAO.EndDate,
                InventoryValuationMethod = FiscalYearDAO.InventoryValuationMethod,
                StatusId = FiscalYearDAO.StatusId,
                BusinessGroupId = FiscalYearDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return FiscalYear;
        }

        public async Task<bool> Create(FiscalYear FiscalYear)
        {
            FiscalYearDAO FiscalYearDAO = new FiscalYearDAO();
            
            FiscalYearDAO.Id = FiscalYear.Id;
            FiscalYearDAO.SetOfBookId = FiscalYear.SetOfBookId;
            FiscalYearDAO.Name = FiscalYear.Name;
            FiscalYearDAO.StartDate = FiscalYear.StartDate;
            FiscalYearDAO.EndDate = FiscalYear.EndDate;
            FiscalYearDAO.InventoryValuationMethod = FiscalYear.InventoryValuationMethod;
            FiscalYearDAO.StatusId = FiscalYear.StatusId;
            FiscalYearDAO.BusinessGroupId = FiscalYear.BusinessGroupId;
            FiscalYearDAO.Disabled = false;
            
            await ERPContext.FiscalYear.AddAsync(FiscalYearDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(FiscalYear FiscalYear)
        {
            FiscalYearDAO FiscalYearDAO = ERPContext.FiscalYear.Where(b => b.Id == FiscalYear.Id).FirstOrDefault();
            
            FiscalYearDAO.Id = FiscalYear.Id;
            FiscalYearDAO.SetOfBookId = FiscalYear.SetOfBookId;
            FiscalYearDAO.Name = FiscalYear.Name;
            FiscalYearDAO.StartDate = FiscalYear.StartDate;
            FiscalYearDAO.EndDate = FiscalYear.EndDate;
            FiscalYearDAO.InventoryValuationMethod = FiscalYear.InventoryValuationMethod;
            FiscalYearDAO.StatusId = FiscalYear.StatusId;
            FiscalYearDAO.BusinessGroupId = FiscalYear.BusinessGroupId;
            FiscalYearDAO.Disabled = false;
            ERPContext.FiscalYear.Update(FiscalYearDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            FiscalYearDAO FiscalYearDAO = await ERPContext.FiscalYear.Where(x => x.Id == Id).FirstOrDefaultAsync();
            FiscalYearDAO.Disabled = true;
            ERPContext.FiscalYear.Update(FiscalYearDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
