
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
    public interface IUnitOfMeasureRepository
    {
        Task<int> Count(UnitOfMeasureFilter UnitOfMeasureFilter);
        Task<List<UnitOfMeasure>> List(UnitOfMeasureFilter UnitOfMeasureFilter);
        Task<UnitOfMeasure> Get(Guid Id);
        Task<bool> Create(UnitOfMeasure UnitOfMeasure);
        Task<bool> Update(UnitOfMeasure UnitOfMeasure);
        Task<bool> Delete(Guid Id);
        
    }
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public UnitOfMeasureRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<UnitOfMeasureDAO> DynamicFilter(IQueryable<UnitOfMeasureDAO> query, UnitOfMeasureFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Type != null)
                query = query.Where(q => q.Type, filter.Type);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<UnitOfMeasureDAO> DynamicOrder(IQueryable<UnitOfMeasureDAO> query,  UnitOfMeasureFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case UnitOfMeasureOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case UnitOfMeasureOrder.Type:
                            query = query.OrderBy(q => q.Type);
                            break;
                        case UnitOfMeasureOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case UnitOfMeasureOrder.Description:
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
                        
                        case UnitOfMeasureOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case UnitOfMeasureOrder.Type:
                            query = query.OrderByDescending(q => q.Type);
                            break;
                        case UnitOfMeasureOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case UnitOfMeasureOrder.Description:
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

        private async Task<List<UnitOfMeasure>> DynamicSelect(IQueryable<UnitOfMeasureDAO> query, UnitOfMeasureFilter filter)
        {
            List <UnitOfMeasure> UnitOfMeasures = await query.Select(q => new UnitOfMeasure()
            {
                
                Id = filter.Selects.Contains(UnitOfMeasureSelect.Id) ? q.Id : default(Guid),
                Name = filter.Selects.Contains(UnitOfMeasureSelect.Name) ? q.Name : default(string),
                Type = filter.Selects.Contains(UnitOfMeasureSelect.Type) ? q.Type : default(string),
                BusinessGroupId = filter.Selects.Contains(UnitOfMeasureSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Code = filter.Selects.Contains(UnitOfMeasureSelect.Code) ? q.Code : default(string),
                Description = filter.Selects.Contains(UnitOfMeasureSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return UnitOfMeasures;
        }

        public async Task<int> Count(UnitOfMeasureFilter filter)
        {
            IQueryable <UnitOfMeasureDAO> UnitOfMeasureDAOs = ERPContext.UnitOfMeasure;
            UnitOfMeasureDAOs = DynamicFilter(UnitOfMeasureDAOs, filter);
            return await UnitOfMeasureDAOs.CountAsync();
        }

        public async Task<List<UnitOfMeasure>> List(UnitOfMeasureFilter filter)
        {
            if (filter == null) return new List<UnitOfMeasure>();
            IQueryable<UnitOfMeasureDAO> UnitOfMeasureDAOs = ERPContext.UnitOfMeasure;
            UnitOfMeasureDAOs = DynamicFilter(UnitOfMeasureDAOs, filter);
            UnitOfMeasureDAOs = DynamicOrder(UnitOfMeasureDAOs, filter);
            var UnitOfMeasures = await DynamicSelect(UnitOfMeasureDAOs, filter);
            return UnitOfMeasures;
        }

        public async Task<UnitOfMeasure> Get(Guid Id)
        {
            UnitOfMeasure UnitOfMeasure = await ERPContext.UnitOfMeasure.Where(l => l.Id == Id).Select(UnitOfMeasureDAO => new UnitOfMeasure()
            {
                 
                Id = UnitOfMeasureDAO.Id,
                Name = UnitOfMeasureDAO.Name,
                Type = UnitOfMeasureDAO.Type,
                BusinessGroupId = UnitOfMeasureDAO.BusinessGroupId,
                Code = UnitOfMeasureDAO.Code,
                Description = UnitOfMeasureDAO.Description,
            }).FirstOrDefaultAsync();
            return UnitOfMeasure;
        }

        public async Task<bool> Create(UnitOfMeasure UnitOfMeasure)
        {
            UnitOfMeasureDAO UnitOfMeasureDAO = new UnitOfMeasureDAO();
            
            UnitOfMeasureDAO.Id = UnitOfMeasure.Id;
            UnitOfMeasureDAO.Name = UnitOfMeasure.Name;
            UnitOfMeasureDAO.Type = UnitOfMeasure.Type;
            UnitOfMeasureDAO.BusinessGroupId = UnitOfMeasure.BusinessGroupId;
            UnitOfMeasureDAO.Code = UnitOfMeasure.Code;
            UnitOfMeasureDAO.Description = UnitOfMeasure.Description;
            UnitOfMeasureDAO.Disabled = false;
            
            await ERPContext.UnitOfMeasure.AddAsync(UnitOfMeasureDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(UnitOfMeasure UnitOfMeasure)
        {
            UnitOfMeasureDAO UnitOfMeasureDAO = ERPContext.UnitOfMeasure.Where(b => b.Id == UnitOfMeasure.Id).FirstOrDefault();
            
            UnitOfMeasureDAO.Id = UnitOfMeasure.Id;
            UnitOfMeasureDAO.Name = UnitOfMeasure.Name;
            UnitOfMeasureDAO.Type = UnitOfMeasure.Type;
            UnitOfMeasureDAO.BusinessGroupId = UnitOfMeasure.BusinessGroupId;
            UnitOfMeasureDAO.Code = UnitOfMeasure.Code;
            UnitOfMeasureDAO.Description = UnitOfMeasure.Description;
            UnitOfMeasureDAO.Disabled = false;
            ERPContext.UnitOfMeasure.Update(UnitOfMeasureDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            UnitOfMeasureDAO UnitOfMeasureDAO = await ERPContext.UnitOfMeasure.Where(x => x.Id == Id).FirstOrDefaultAsync();
            UnitOfMeasureDAO.Disabled = true;
            ERPContext.UnitOfMeasure.Update(UnitOfMeasureDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
