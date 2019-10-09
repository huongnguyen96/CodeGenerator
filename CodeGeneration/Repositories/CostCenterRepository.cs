
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
    public interface ICostCenterRepository
    {
        Task<int> Count(CostCenterFilter CostCenterFilter);
        Task<List<CostCenter>> List(CostCenterFilter CostCenterFilter);
        Task<CostCenter> Get(Guid Id);
        Task<bool> Create(CostCenter CostCenter);
        Task<bool> Update(CostCenter CostCenter);
        Task<bool> Delete(Guid Id);
        
    }
    public class CostCenterRepository : ICostCenterRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public CostCenterRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<CostCenterDAO> DynamicFilter(IQueryable<CostCenterDAO> query, CostCenterFilter filter)
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
            if (filter.ParentId.HasValue)
                query = query.Where(q => q.ParentId.HasValue && q.ParentId.Value == filter.ParentId.Value);
            if (filter.ParentId != null)
                query = query.Where(q => q.ParentId, filter.ParentId);
            if (filter.ChartOfAccountId.HasValue)
                query = query.Where(q => q.ChartOfAccountId.HasValue && q.ChartOfAccountId.Value == filter.ChartOfAccountId.Value);
            if (filter.ChartOfAccountId != null)
                query = query.Where(q => q.ChartOfAccountId, filter.ChartOfAccountId);
            if (filter.SetOfBookId != null)
                query = query.Where(q => q.SetOfBookId, filter.SetOfBookId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.ValidFrom.HasValue)
                query = query.Where(q => q.ValidFrom.HasValue && q.ValidFrom.Value == filter.ValidFrom.Value);
            if (filter.ValidFrom != null)
                query = query.Where(q => q.ValidFrom, filter.ValidFrom);
            if (filter.ValidTo.HasValue)
                query = query.Where(q => q.ValidTo.HasValue && q.ValidTo.Value == filter.ValidTo.Value);
            if (filter.ValidTo != null)
                query = query.Where(q => q.ValidTo, filter.ValidTo);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<CostCenterDAO> DynamicOrder(IQueryable<CostCenterDAO> query,  CostCenterFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case CostCenterOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CostCenterOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CostCenterOrder.ParentId:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        case CostCenterOrder.ChartOfAccountId:
                            query = query.OrderBy(q => q.ChartOfAccountId);
                            break;
                        case CostCenterOrder.ValidFrom:
                            query = query.OrderBy(q => q.ValidFrom);
                            break;
                        case CostCenterOrder.ValidTo:
                            query = query.OrderBy(q => q.ValidTo);
                            break;
                        case CostCenterOrder.Description:
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
                        
                        case CostCenterOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CostCenterOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CostCenterOrder.ParentId:
                            query = query.OrderByDescending(q => q.ParentId);
                            break;
                        case CostCenterOrder.ChartOfAccountId:
                            query = query.OrderByDescending(q => q.ChartOfAccountId);
                            break;
                        case CostCenterOrder.ValidFrom:
                            query = query.OrderByDescending(q => q.ValidFrom);
                            break;
                        case CostCenterOrder.ValidTo:
                            query = query.OrderByDescending(q => q.ValidTo);
                            break;
                        case CostCenterOrder.Description:
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

        private async Task<List<CostCenter>> DynamicSelect(IQueryable<CostCenterDAO> query, CostCenterFilter filter)
        {
            List <CostCenter> CostCenters = await query.Select(q => new CostCenter()
            {
                
                Id = filter.Selects.Contains(CostCenterSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(CostCenterSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CostCenterSelect.Name) ? q.Name : default(string),
                ParentId = filter.Selects.Contains(CostCenterSelect.Parent) ? q.ParentId : default(Guid?),
                ChartOfAccountId = filter.Selects.Contains(CostCenterSelect.ChartOfAccount) ? q.ChartOfAccountId : default(Guid?),
                SetOfBookId = filter.Selects.Contains(CostCenterSelect.SetOfBook) ? q.SetOfBookId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(CostCenterSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                ValidFrom = filter.Selects.Contains(CostCenterSelect.ValidFrom) ? q.ValidFrom : default(Guid?),
                ValidTo = filter.Selects.Contains(CostCenterSelect.ValidTo) ? q.ValidTo : default(Guid?),
                Description = filter.Selects.Contains(CostCenterSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return CostCenters;
        }

        public async Task<int> Count(CostCenterFilter filter)
        {
            IQueryable <CostCenterDAO> CostCenterDAOs = ERPContext.CostCenter;
            CostCenterDAOs = DynamicFilter(CostCenterDAOs, filter);
            return await CostCenterDAOs.CountAsync();
        }

        public async Task<List<CostCenter>> List(CostCenterFilter filter)
        {
            if (filter == null) return new List<CostCenter>();
            IQueryable<CostCenterDAO> CostCenterDAOs = ERPContext.CostCenter;
            CostCenterDAOs = DynamicFilter(CostCenterDAOs, filter);
            CostCenterDAOs = DynamicOrder(CostCenterDAOs, filter);
            var CostCenters = await DynamicSelect(CostCenterDAOs, filter);
            return CostCenters;
        }

        public async Task<CostCenter> Get(Guid Id)
        {
            CostCenter CostCenter = await ERPContext.CostCenter.Where(l => l.Id == Id).Select(CostCenterDAO => new CostCenter()
            {
                 
                Id = CostCenterDAO.Id,
                Code = CostCenterDAO.Code,
                Name = CostCenterDAO.Name,
                ParentId = CostCenterDAO.ParentId,
                ChartOfAccountId = CostCenterDAO.ChartOfAccountId,
                SetOfBookId = CostCenterDAO.SetOfBookId,
                BusinessGroupId = CostCenterDAO.BusinessGroupId,
                ValidFrom = CostCenterDAO.ValidFrom,
                ValidTo = CostCenterDAO.ValidTo,
                Description = CostCenterDAO.Description,
            }).FirstOrDefaultAsync();
            return CostCenter;
        }

        public async Task<bool> Create(CostCenter CostCenter)
        {
            CostCenterDAO CostCenterDAO = new CostCenterDAO();
            
            CostCenterDAO.Id = CostCenter.Id;
            CostCenterDAO.Code = CostCenter.Code;
            CostCenterDAO.Name = CostCenter.Name;
            CostCenterDAO.ParentId = CostCenter.ParentId;
            CostCenterDAO.ChartOfAccountId = CostCenter.ChartOfAccountId;
            CostCenterDAO.SetOfBookId = CostCenter.SetOfBookId;
            CostCenterDAO.BusinessGroupId = CostCenter.BusinessGroupId;
            CostCenterDAO.ValidFrom = CostCenter.ValidFrom;
            CostCenterDAO.ValidTo = CostCenter.ValidTo;
            CostCenterDAO.Description = CostCenter.Description;
            CostCenterDAO.Disabled = false;
            
            await ERPContext.CostCenter.AddAsync(CostCenterDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(CostCenter CostCenter)
        {
            CostCenterDAO CostCenterDAO = ERPContext.CostCenter.Where(b => b.Id == CostCenter.Id).FirstOrDefault();
            
            CostCenterDAO.Id = CostCenter.Id;
            CostCenterDAO.Code = CostCenter.Code;
            CostCenterDAO.Name = CostCenter.Name;
            CostCenterDAO.ParentId = CostCenter.ParentId;
            CostCenterDAO.ChartOfAccountId = CostCenter.ChartOfAccountId;
            CostCenterDAO.SetOfBookId = CostCenter.SetOfBookId;
            CostCenterDAO.BusinessGroupId = CostCenter.BusinessGroupId;
            CostCenterDAO.ValidFrom = CostCenter.ValidFrom;
            CostCenterDAO.ValidTo = CostCenter.ValidTo;
            CostCenterDAO.Description = CostCenter.Description;
            CostCenterDAO.Disabled = false;
            ERPContext.CostCenter.Update(CostCenterDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            CostCenterDAO CostCenterDAO = await ERPContext.CostCenter.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CostCenterDAO.Disabled = true;
            ERPContext.CostCenter.Update(CostCenterDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
