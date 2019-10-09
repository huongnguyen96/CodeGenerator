
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
    public interface IEmployeePositionRepository
    {
        Task<int> Count(EmployeePositionFilter EmployeePositionFilter);
        Task<List<EmployeePosition>> List(EmployeePositionFilter EmployeePositionFilter);
        Task<EmployeePosition> Get(Guid Id);
        Task<bool> Create(EmployeePosition EmployeePosition);
        Task<bool> Update(EmployeePosition EmployeePosition);
        Task<bool> Delete(Guid Id);
        
    }
    public class EmployeePositionRepository : IEmployeePositionRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public EmployeePositionRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EmployeePositionDAO> DynamicFilter(IQueryable<EmployeePositionDAO> query, EmployeePositionFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.EmployeeDetailId != null)
                query = query.Where(q => q.EmployeeDetailId, filter.EmployeeDetailId);
            if (filter.PositionId != null)
                query = query.Where(q => q.PositionId, filter.PositionId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<EmployeePositionDAO> DynamicOrder(IQueryable<EmployeePositionDAO> query,  EmployeePositionFilter filter)
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

        private async Task<List<EmployeePosition>> DynamicSelect(IQueryable<EmployeePositionDAO> query, EmployeePositionFilter filter)
        {
            List <EmployeePosition> EmployeePositions = await query.Select(q => new EmployeePosition()
            {
                
                Id = filter.Selects.Contains(EmployeePositionSelect.Id) ? q.Id : default(Guid),
                EmployeeDetailId = filter.Selects.Contains(EmployeePositionSelect.EmployeeDetail) ? q.EmployeeDetailId : default(Guid),
                PositionId = filter.Selects.Contains(EmployeePositionSelect.Position) ? q.PositionId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(EmployeePositionSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return EmployeePositions;
        }

        public async Task<int> Count(EmployeePositionFilter filter)
        {
            IQueryable <EmployeePositionDAO> EmployeePositionDAOs = ERPContext.EmployeePosition;
            EmployeePositionDAOs = DynamicFilter(EmployeePositionDAOs, filter);
            return await EmployeePositionDAOs.CountAsync();
        }

        public async Task<List<EmployeePosition>> List(EmployeePositionFilter filter)
        {
            if (filter == null) return new List<EmployeePosition>();
            IQueryable<EmployeePositionDAO> EmployeePositionDAOs = ERPContext.EmployeePosition;
            EmployeePositionDAOs = DynamicFilter(EmployeePositionDAOs, filter);
            EmployeePositionDAOs = DynamicOrder(EmployeePositionDAOs, filter);
            var EmployeePositions = await DynamicSelect(EmployeePositionDAOs, filter);
            return EmployeePositions;
        }

        public async Task<EmployeePosition> Get(Guid Id)
        {
            EmployeePosition EmployeePosition = await ERPContext.EmployeePosition.Where(l => l.Id == Id).Select(EmployeePositionDAO => new EmployeePosition()
            {
                 
                Id = EmployeePositionDAO.Id,
                EmployeeDetailId = EmployeePositionDAO.EmployeeDetailId,
                PositionId = EmployeePositionDAO.PositionId,
                BusinessGroupId = EmployeePositionDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return EmployeePosition;
        }

        public async Task<bool> Create(EmployeePosition EmployeePosition)
        {
            EmployeePositionDAO EmployeePositionDAO = new EmployeePositionDAO();
            
            EmployeePositionDAO.Id = EmployeePosition.Id;
            EmployeePositionDAO.EmployeeDetailId = EmployeePosition.EmployeeDetailId;
            EmployeePositionDAO.PositionId = EmployeePosition.PositionId;
            EmployeePositionDAO.BusinessGroupId = EmployeePosition.BusinessGroupId;
            EmployeePositionDAO.Disabled = false;
            
            await ERPContext.EmployeePosition.AddAsync(EmployeePositionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(EmployeePosition EmployeePosition)
        {
            EmployeePositionDAO EmployeePositionDAO = ERPContext.EmployeePosition.Where(b => b.Id == EmployeePosition.Id).FirstOrDefault();
            
            EmployeePositionDAO.Id = EmployeePosition.Id;
            EmployeePositionDAO.EmployeeDetailId = EmployeePosition.EmployeeDetailId;
            EmployeePositionDAO.PositionId = EmployeePosition.PositionId;
            EmployeePositionDAO.BusinessGroupId = EmployeePosition.BusinessGroupId;
            EmployeePositionDAO.Disabled = false;
            ERPContext.EmployeePosition.Update(EmployeePositionDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            EmployeePositionDAO EmployeePositionDAO = await ERPContext.EmployeePosition.Where(x => x.Id == Id).FirstOrDefaultAsync();
            EmployeePositionDAO.Disabled = true;
            ERPContext.EmployeePosition.Update(EmployeePositionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
