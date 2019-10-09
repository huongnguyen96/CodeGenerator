
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
    public interface IFeatureOperationRepository
    {
        Task<int> Count(FeatureOperationFilter FeatureOperationFilter);
        Task<List<FeatureOperation>> List(FeatureOperationFilter FeatureOperationFilter);
        Task<FeatureOperation> Get(Guid Id);
        Task<bool> Create(FeatureOperation FeatureOperation);
        Task<bool> Update(FeatureOperation FeatureOperation);
        Task<bool> Delete(Guid Id);
        
    }
    public class FeatureOperationRepository : IFeatureOperationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public FeatureOperationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<FeatureOperationDAO> DynamicFilter(IQueryable<FeatureOperationDAO> query, FeatureOperationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.FeatureId != null)
                query = query.Where(q => q.FeatureId, filter.FeatureId);
            if (filter.OperationId != null)
                query = query.Where(q => q.OperationId, filter.OperationId);
            return query;
        }
        private IQueryable<FeatureOperationDAO> DynamicOrder(IQueryable<FeatureOperationDAO> query,  FeatureOperationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
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

        private async Task<List<FeatureOperation>> DynamicSelect(IQueryable<FeatureOperationDAO> query, FeatureOperationFilter filter)
        {
            List <FeatureOperation> FeatureOperations = await query.Select(q => new FeatureOperation()
            {
                
                Id = filter.Selects.Contains(FeatureOperationSelect.Id) ? q.Id : default(Guid),
                FeatureId = filter.Selects.Contains(FeatureOperationSelect.Feature) ? q.FeatureId : default(Guid),
                OperationId = filter.Selects.Contains(FeatureOperationSelect.Operation) ? q.OperationId : default(Guid),
            }).ToListAsync();
            return FeatureOperations;
        }

        public async Task<int> Count(FeatureOperationFilter filter)
        {
            IQueryable <FeatureOperationDAO> FeatureOperationDAOs = ERPContext.FeatureOperation;
            FeatureOperationDAOs = DynamicFilter(FeatureOperationDAOs, filter);
            return await FeatureOperationDAOs.CountAsync();
        }

        public async Task<List<FeatureOperation>> List(FeatureOperationFilter filter)
        {
            if (filter == null) return new List<FeatureOperation>();
            IQueryable<FeatureOperationDAO> FeatureOperationDAOs = ERPContext.FeatureOperation;
            FeatureOperationDAOs = DynamicFilter(FeatureOperationDAOs, filter);
            FeatureOperationDAOs = DynamicOrder(FeatureOperationDAOs, filter);
            var FeatureOperations = await DynamicSelect(FeatureOperationDAOs, filter);
            return FeatureOperations;
        }

        public async Task<FeatureOperation> Get(Guid Id)
        {
            FeatureOperation FeatureOperation = await ERPContext.FeatureOperation.Where(l => l.Id == Id).Select(FeatureOperationDAO => new FeatureOperation()
            {
                 
                Id = FeatureOperationDAO.Id,
                FeatureId = FeatureOperationDAO.FeatureId,
                OperationId = FeatureOperationDAO.OperationId,
            }).FirstOrDefaultAsync();
            return FeatureOperation;
        }

        public async Task<bool> Create(FeatureOperation FeatureOperation)
        {
            FeatureOperationDAO FeatureOperationDAO = new FeatureOperationDAO();
            
            FeatureOperationDAO.Id = FeatureOperation.Id;
            FeatureOperationDAO.FeatureId = FeatureOperation.FeatureId;
            FeatureOperationDAO.OperationId = FeatureOperation.OperationId;
            FeatureOperationDAO.Disabled = false;
            
            await ERPContext.FeatureOperation.AddAsync(FeatureOperationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(FeatureOperation FeatureOperation)
        {
            FeatureOperationDAO FeatureOperationDAO = ERPContext.FeatureOperation.Where(b => b.Id == FeatureOperation.Id).FirstOrDefault();
            
            FeatureOperationDAO.Id = FeatureOperation.Id;
            FeatureOperationDAO.FeatureId = FeatureOperation.FeatureId;
            FeatureOperationDAO.OperationId = FeatureOperation.OperationId;
            FeatureOperationDAO.Disabled = false;
            ERPContext.FeatureOperation.Update(FeatureOperationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            FeatureOperationDAO FeatureOperationDAO = await ERPContext.FeatureOperation.Where(x => x.Id == Id).FirstOrDefaultAsync();
            FeatureOperationDAO.Disabled = true;
            ERPContext.FeatureOperation.Update(FeatureOperationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
