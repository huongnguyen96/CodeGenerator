
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
    public interface ITransformationUnitRepository
    {
        Task<int> Count(TransformationUnitFilter TransformationUnitFilter);
        Task<List<TransformationUnit>> List(TransformationUnitFilter TransformationUnitFilter);
        Task<TransformationUnit> Get(Guid Id);
        Task<bool> Create(TransformationUnit TransformationUnit);
        Task<bool> Update(TransformationUnit TransformationUnit);
        Task<bool> Delete(Guid Id);
        
    }
    public class TransformationUnitRepository : ITransformationUnitRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public TransformationUnitRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<TransformationUnitDAO> DynamicFilter(IQueryable<TransformationUnitDAO> query, TransformationUnitFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.ItemDetailId != null)
                query = query.Where(q => q.ItemDetailId, filter.ItemDetailId);
            if (filter.BaseUnitId != null)
                query = query.Where(q => q.BaseUnitId, filter.BaseUnitId);
            if (filter.Rate != null)
                query = query.Where(q => q.Rate, filter.Rate);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.SalePrice != null)
                query = query.Where(q => q.SalePrice, filter.SalePrice);
            if (filter.PrimaryPrice != null)
                query = query.Where(q => q.PrimaryPrice, filter.PrimaryPrice);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<TransformationUnitDAO> DynamicOrder(IQueryable<TransformationUnitDAO> query,  TransformationUnitFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case TransformationUnitOrder.Rate:
                            query = query.OrderBy(q => q.Rate);
                            break;
                        case TransformationUnitOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case TransformationUnitOrder.SalePrice:
                            query = query.OrderBy(q => q.SalePrice);
                            break;
                        case TransformationUnitOrder.PrimaryPrice:
                            query = query.OrderBy(q => q.PrimaryPrice);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case TransformationUnitOrder.Rate:
                            query = query.OrderByDescending(q => q.Rate);
                            break;
                        case TransformationUnitOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case TransformationUnitOrder.SalePrice:
                            query = query.OrderByDescending(q => q.SalePrice);
                            break;
                        case TransformationUnitOrder.PrimaryPrice:
                            query = query.OrderByDescending(q => q.PrimaryPrice);
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

        private async Task<List<TransformationUnit>> DynamicSelect(IQueryable<TransformationUnitDAO> query, TransformationUnitFilter filter)
        {
            List <TransformationUnit> TransformationUnits = await query.Select(q => new TransformationUnit()
            {
                
                Id = filter.Selects.Contains(TransformationUnitSelect.Id) ? q.Id : default(Guid),
                ItemDetailId = filter.Selects.Contains(TransformationUnitSelect.ItemDetail) ? q.ItemDetailId : default(Guid),
                BaseUnitId = filter.Selects.Contains(TransformationUnitSelect.BaseUnit) ? q.BaseUnitId : default(Guid),
                Rate = filter.Selects.Contains(TransformationUnitSelect.Rate) ? q.Rate : default(decimal),
                Description = filter.Selects.Contains(TransformationUnitSelect.Description) ? q.Description : default(string),
                SalePrice = filter.Selects.Contains(TransformationUnitSelect.SalePrice) ? q.SalePrice : default(decimal),
                PrimaryPrice = filter.Selects.Contains(TransformationUnitSelect.PrimaryPrice) ? q.PrimaryPrice : default(decimal),
                BusinessGroupId = filter.Selects.Contains(TransformationUnitSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return TransformationUnits;
        }

        public async Task<int> Count(TransformationUnitFilter filter)
        {
            IQueryable <TransformationUnitDAO> TransformationUnitDAOs = ERPContext.TransformationUnit;
            TransformationUnitDAOs = DynamicFilter(TransformationUnitDAOs, filter);
            return await TransformationUnitDAOs.CountAsync();
        }

        public async Task<List<TransformationUnit>> List(TransformationUnitFilter filter)
        {
            if (filter == null) return new List<TransformationUnit>();
            IQueryable<TransformationUnitDAO> TransformationUnitDAOs = ERPContext.TransformationUnit;
            TransformationUnitDAOs = DynamicFilter(TransformationUnitDAOs, filter);
            TransformationUnitDAOs = DynamicOrder(TransformationUnitDAOs, filter);
            var TransformationUnits = await DynamicSelect(TransformationUnitDAOs, filter);
            return TransformationUnits;
        }

        public async Task<TransformationUnit> Get(Guid Id)
        {
            TransformationUnit TransformationUnit = await ERPContext.TransformationUnit.Where(l => l.Id == Id).Select(TransformationUnitDAO => new TransformationUnit()
            {
                 
                Id = TransformationUnitDAO.Id,
                ItemDetailId = TransformationUnitDAO.ItemDetailId,
                BaseUnitId = TransformationUnitDAO.BaseUnitId,
                Rate = TransformationUnitDAO.Rate,
                Description = TransformationUnitDAO.Description,
                SalePrice = TransformationUnitDAO.SalePrice,
                PrimaryPrice = TransformationUnitDAO.PrimaryPrice,
                BusinessGroupId = TransformationUnitDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return TransformationUnit;
        }

        public async Task<bool> Create(TransformationUnit TransformationUnit)
        {
            TransformationUnitDAO TransformationUnitDAO = new TransformationUnitDAO();
            
            TransformationUnitDAO.Id = TransformationUnit.Id;
            TransformationUnitDAO.ItemDetailId = TransformationUnit.ItemDetailId;
            TransformationUnitDAO.BaseUnitId = TransformationUnit.BaseUnitId;
            TransformationUnitDAO.Rate = TransformationUnit.Rate;
            TransformationUnitDAO.Description = TransformationUnit.Description;
            TransformationUnitDAO.SalePrice = TransformationUnit.SalePrice;
            TransformationUnitDAO.PrimaryPrice = TransformationUnit.PrimaryPrice;
            TransformationUnitDAO.BusinessGroupId = TransformationUnit.BusinessGroupId;
            TransformationUnitDAO.Disabled = false;
            
            await ERPContext.TransformationUnit.AddAsync(TransformationUnitDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(TransformationUnit TransformationUnit)
        {
            TransformationUnitDAO TransformationUnitDAO = ERPContext.TransformationUnit.Where(b => b.Id == TransformationUnit.Id).FirstOrDefault();
            
            TransformationUnitDAO.Id = TransformationUnit.Id;
            TransformationUnitDAO.ItemDetailId = TransformationUnit.ItemDetailId;
            TransformationUnitDAO.BaseUnitId = TransformationUnit.BaseUnitId;
            TransformationUnitDAO.Rate = TransformationUnit.Rate;
            TransformationUnitDAO.Description = TransformationUnit.Description;
            TransformationUnitDAO.SalePrice = TransformationUnit.SalePrice;
            TransformationUnitDAO.PrimaryPrice = TransformationUnit.PrimaryPrice;
            TransformationUnitDAO.BusinessGroupId = TransformationUnit.BusinessGroupId;
            TransformationUnitDAO.Disabled = false;
            ERPContext.TransformationUnit.Update(TransformationUnitDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            TransformationUnitDAO TransformationUnitDAO = await ERPContext.TransformationUnit.Where(x => x.Id == Id).FirstOrDefaultAsync();
            TransformationUnitDAO.Disabled = true;
            ERPContext.TransformationUnit.Update(TransformationUnitDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
