
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
    public interface ISetOfBookRepository
    {
        Task<int> Count(SetOfBookFilter SetOfBookFilter);
        Task<List<SetOfBook>> List(SetOfBookFilter SetOfBookFilter);
        Task<SetOfBook> Get(Guid Id);
        Task<bool> Create(SetOfBook SetOfBook);
        Task<bool> Update(SetOfBook SetOfBook);
        Task<bool> Delete(Guid Id);
        
    }
    public class SetOfBookRepository : ISetOfBookRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SetOfBookRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SetOfBookDAO> DynamicFilter(IQueryable<SetOfBookDAO> query, SetOfBookFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.ShortName != null)
                query = query.Where(q => q.ShortName, filter.ShortName);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.CurrencyId != null)
                query = query.Where(q => q.CurrencyId, filter.CurrencyId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.ChartOfAccountTemplateId.HasValue)
                query = query.Where(q => q.ChartOfAccountTemplateId.HasValue && q.ChartOfAccountTemplateId.Value == filter.ChartOfAccountTemplateId.Value);
            if (filter.ChartOfAccountTemplateId != null)
                query = query.Where(q => q.ChartOfAccountTemplateId, filter.ChartOfAccountTemplateId);
            if (filter.EnvironmentTaxTemplateId.HasValue)
                query = query.Where(q => q.EnvironmentTaxTemplateId.HasValue && q.EnvironmentTaxTemplateId.Value == filter.EnvironmentTaxTemplateId.Value);
            if (filter.EnvironmentTaxTemplateId != null)
                query = query.Where(q => q.EnvironmentTaxTemplateId, filter.EnvironmentTaxTemplateId);
            if (filter.ExportTaxTemplateId.HasValue)
                query = query.Where(q => q.ExportTaxTemplateId.HasValue && q.ExportTaxTemplateId.Value == filter.ExportTaxTemplateId.Value);
            if (filter.ExportTaxTemplateId != null)
                query = query.Where(q => q.ExportTaxTemplateId, filter.ExportTaxTemplateId);
            if (filter.ImportTaxTemplateId.HasValue)
                query = query.Where(q => q.ImportTaxTemplateId.HasValue && q.ImportTaxTemplateId.Value == filter.ImportTaxTemplateId.Value);
            if (filter.ImportTaxTemplateId != null)
                query = query.Where(q => q.ImportTaxTemplateId, filter.ImportTaxTemplateId);
            if (filter.NaturalResourceTaxTemplateId.HasValue)
                query = query.Where(q => q.NaturalResourceTaxTemplateId.HasValue && q.NaturalResourceTaxTemplateId.Value == filter.NaturalResourceTaxTemplateId.Value);
            if (filter.NaturalResourceTaxTemplateId != null)
                query = query.Where(q => q.NaturalResourceTaxTemplateId, filter.NaturalResourceTaxTemplateId);
            if (filter.SpecialConsumptionTaxTemplateId.HasValue)
                query = query.Where(q => q.SpecialConsumptionTaxTemplateId.HasValue && q.SpecialConsumptionTaxTemplateId.Value == filter.SpecialConsumptionTaxTemplateId.Value);
            if (filter.SpecialConsumptionTaxTemplateId != null)
                query = query.Where(q => q.SpecialConsumptionTaxTemplateId, filter.SpecialConsumptionTaxTemplateId);
            if (filter.ValueAddedTaxTemplateId.HasValue)
                query = query.Where(q => q.ValueAddedTaxTemplateId.HasValue && q.ValueAddedTaxTemplateId.Value == filter.ValueAddedTaxTemplateId.Value);
            if (filter.ValueAddedTaxTemplateId != null)
                query = query.Where(q => q.ValueAddedTaxTemplateId, filter.ValueAddedTaxTemplateId);
            return query;
        }
        private IQueryable<SetOfBookDAO> DynamicOrder(IQueryable<SetOfBookDAO> query,  SetOfBookFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SetOfBookOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SetOfBookOrder.ShortName:
                            query = query.OrderBy(q => q.ShortName);
                            break;
                        case SetOfBookOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SetOfBookOrder.ChartOfAccountTemplateId:
                            query = query.OrderBy(q => q.ChartOfAccountTemplateId);
                            break;
                        case SetOfBookOrder.EnvironmentTaxTemplateId:
                            query = query.OrderBy(q => q.EnvironmentTaxTemplateId);
                            break;
                        case SetOfBookOrder.ExportTaxTemplateId:
                            query = query.OrderBy(q => q.ExportTaxTemplateId);
                            break;
                        case SetOfBookOrder.ImportTaxTemplateId:
                            query = query.OrderBy(q => q.ImportTaxTemplateId);
                            break;
                        case SetOfBookOrder.NaturalResourceTaxTemplateId:
                            query = query.OrderBy(q => q.NaturalResourceTaxTemplateId);
                            break;
                        case SetOfBookOrder.SpecialConsumptionTaxTemplateId:
                            query = query.OrderBy(q => q.SpecialConsumptionTaxTemplateId);
                            break;
                        case SetOfBookOrder.ValueAddedTaxTemplateId:
                            query = query.OrderBy(q => q.ValueAddedTaxTemplateId);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SetOfBookOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SetOfBookOrder.ShortName:
                            query = query.OrderByDescending(q => q.ShortName);
                            break;
                        case SetOfBookOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SetOfBookOrder.ChartOfAccountTemplateId:
                            query = query.OrderByDescending(q => q.ChartOfAccountTemplateId);
                            break;
                        case SetOfBookOrder.EnvironmentTaxTemplateId:
                            query = query.OrderByDescending(q => q.EnvironmentTaxTemplateId);
                            break;
                        case SetOfBookOrder.ExportTaxTemplateId:
                            query = query.OrderByDescending(q => q.ExportTaxTemplateId);
                            break;
                        case SetOfBookOrder.ImportTaxTemplateId:
                            query = query.OrderByDescending(q => q.ImportTaxTemplateId);
                            break;
                        case SetOfBookOrder.NaturalResourceTaxTemplateId:
                            query = query.OrderByDescending(q => q.NaturalResourceTaxTemplateId);
                            break;
                        case SetOfBookOrder.SpecialConsumptionTaxTemplateId:
                            query = query.OrderByDescending(q => q.SpecialConsumptionTaxTemplateId);
                            break;
                        case SetOfBookOrder.ValueAddedTaxTemplateId:
                            query = query.OrderByDescending(q => q.ValueAddedTaxTemplateId);
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

        private async Task<List<SetOfBook>> DynamicSelect(IQueryable<SetOfBookDAO> query, SetOfBookFilter filter)
        {
            List <SetOfBook> SetOfBooks = await query.Select(q => new SetOfBook()
            {
                
                Id = filter.Selects.Contains(SetOfBookSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(SetOfBookSelect.Code) ? q.Code : default(string),
                ShortName = filter.Selects.Contains(SetOfBookSelect.ShortName) ? q.ShortName : default(string),
                Name = filter.Selects.Contains(SetOfBookSelect.Name) ? q.Name : default(string),
                BusinessGroupId = filter.Selects.Contains(SetOfBookSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                CurrencyId = filter.Selects.Contains(SetOfBookSelect.Currency) ? q.CurrencyId : default(Guid),
                ChartOfAccountTemplateId = filter.Selects.Contains(SetOfBookSelect.ChartOfAccountTemplate) ? q.ChartOfAccountTemplateId : default(Guid?),
                EnvironmentTaxTemplateId = filter.Selects.Contains(SetOfBookSelect.EnvironmentTaxTemplate) ? q.EnvironmentTaxTemplateId : default(Guid?),
                ExportTaxTemplateId = filter.Selects.Contains(SetOfBookSelect.ExportTaxTemplate) ? q.ExportTaxTemplateId : default(Guid?),
                ImportTaxTemplateId = filter.Selects.Contains(SetOfBookSelect.ImportTaxTemplate) ? q.ImportTaxTemplateId : default(Guid?),
                NaturalResourceTaxTemplateId = filter.Selects.Contains(SetOfBookSelect.NaturalResourceTaxTemplate) ? q.NaturalResourceTaxTemplateId : default(Guid?),
                SpecialConsumptionTaxTemplateId = filter.Selects.Contains(SetOfBookSelect.SpecialConsumptionTaxTemplate) ? q.SpecialConsumptionTaxTemplateId : default(Guid?),
                ValueAddedTaxTemplateId = filter.Selects.Contains(SetOfBookSelect.ValueAddedTaxTemplate) ? q.ValueAddedTaxTemplateId : default(Guid?),
            }).ToListAsync();
            return SetOfBooks;
        }

        public async Task<int> Count(SetOfBookFilter filter)
        {
            IQueryable <SetOfBookDAO> SetOfBookDAOs = ERPContext.SetOfBook;
            SetOfBookDAOs = DynamicFilter(SetOfBookDAOs, filter);
            return await SetOfBookDAOs.CountAsync();
        }

        public async Task<List<SetOfBook>> List(SetOfBookFilter filter)
        {
            if (filter == null) return new List<SetOfBook>();
            IQueryable<SetOfBookDAO> SetOfBookDAOs = ERPContext.SetOfBook;
            SetOfBookDAOs = DynamicFilter(SetOfBookDAOs, filter);
            SetOfBookDAOs = DynamicOrder(SetOfBookDAOs, filter);
            var SetOfBooks = await DynamicSelect(SetOfBookDAOs, filter);
            return SetOfBooks;
        }

        public async Task<SetOfBook> Get(Guid Id)
        {
            SetOfBook SetOfBook = await ERPContext.SetOfBook.Where(l => l.Id == Id).Select(SetOfBookDAO => new SetOfBook()
            {
                 
                Id = SetOfBookDAO.Id,
                Code = SetOfBookDAO.Code,
                ShortName = SetOfBookDAO.ShortName,
                Name = SetOfBookDAO.Name,
                BusinessGroupId = SetOfBookDAO.BusinessGroupId,
                CurrencyId = SetOfBookDAO.CurrencyId,
                ChartOfAccountTemplateId = SetOfBookDAO.ChartOfAccountTemplateId,
                EnvironmentTaxTemplateId = SetOfBookDAO.EnvironmentTaxTemplateId,
                ExportTaxTemplateId = SetOfBookDAO.ExportTaxTemplateId,
                ImportTaxTemplateId = SetOfBookDAO.ImportTaxTemplateId,
                NaturalResourceTaxTemplateId = SetOfBookDAO.NaturalResourceTaxTemplateId,
                SpecialConsumptionTaxTemplateId = SetOfBookDAO.SpecialConsumptionTaxTemplateId,
                ValueAddedTaxTemplateId = SetOfBookDAO.ValueAddedTaxTemplateId,
            }).FirstOrDefaultAsync();
            return SetOfBook;
        }

        public async Task<bool> Create(SetOfBook SetOfBook)
        {
            SetOfBookDAO SetOfBookDAO = new SetOfBookDAO();
            
            SetOfBookDAO.Id = SetOfBook.Id;
            SetOfBookDAO.Code = SetOfBook.Code;
            SetOfBookDAO.ShortName = SetOfBook.ShortName;
            SetOfBookDAO.Name = SetOfBook.Name;
            SetOfBookDAO.BusinessGroupId = SetOfBook.BusinessGroupId;
            SetOfBookDAO.CurrencyId = SetOfBook.CurrencyId;
            SetOfBookDAO.ChartOfAccountTemplateId = SetOfBook.ChartOfAccountTemplateId;
            SetOfBookDAO.EnvironmentTaxTemplateId = SetOfBook.EnvironmentTaxTemplateId;
            SetOfBookDAO.ExportTaxTemplateId = SetOfBook.ExportTaxTemplateId;
            SetOfBookDAO.ImportTaxTemplateId = SetOfBook.ImportTaxTemplateId;
            SetOfBookDAO.NaturalResourceTaxTemplateId = SetOfBook.NaturalResourceTaxTemplateId;
            SetOfBookDAO.SpecialConsumptionTaxTemplateId = SetOfBook.SpecialConsumptionTaxTemplateId;
            SetOfBookDAO.ValueAddedTaxTemplateId = SetOfBook.ValueAddedTaxTemplateId;
            SetOfBookDAO.Disabled = false;
            
            await ERPContext.SetOfBook.AddAsync(SetOfBookDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SetOfBook SetOfBook)
        {
            SetOfBookDAO SetOfBookDAO = ERPContext.SetOfBook.Where(b => b.Id == SetOfBook.Id).FirstOrDefault();
            
            SetOfBookDAO.Id = SetOfBook.Id;
            SetOfBookDAO.Code = SetOfBook.Code;
            SetOfBookDAO.ShortName = SetOfBook.ShortName;
            SetOfBookDAO.Name = SetOfBook.Name;
            SetOfBookDAO.BusinessGroupId = SetOfBook.BusinessGroupId;
            SetOfBookDAO.CurrencyId = SetOfBook.CurrencyId;
            SetOfBookDAO.ChartOfAccountTemplateId = SetOfBook.ChartOfAccountTemplateId;
            SetOfBookDAO.EnvironmentTaxTemplateId = SetOfBook.EnvironmentTaxTemplateId;
            SetOfBookDAO.ExportTaxTemplateId = SetOfBook.ExportTaxTemplateId;
            SetOfBookDAO.ImportTaxTemplateId = SetOfBook.ImportTaxTemplateId;
            SetOfBookDAO.NaturalResourceTaxTemplateId = SetOfBook.NaturalResourceTaxTemplateId;
            SetOfBookDAO.SpecialConsumptionTaxTemplateId = SetOfBook.SpecialConsumptionTaxTemplateId;
            SetOfBookDAO.ValueAddedTaxTemplateId = SetOfBook.ValueAddedTaxTemplateId;
            SetOfBookDAO.Disabled = false;
            ERPContext.SetOfBook.Update(SetOfBookDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SetOfBookDAO SetOfBookDAO = await ERPContext.SetOfBook.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SetOfBookDAO.Disabled = true;
            ERPContext.SetOfBook.Update(SetOfBookDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
