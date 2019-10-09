
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
    public interface IPositionRepository
    {
        Task<int> Count(PositionFilter PositionFilter);
        Task<List<Position>> List(PositionFilter PositionFilter);
        Task<Position> Get(Guid Id);
        Task<bool> Create(Position Position);
        Task<bool> Update(Position Position);
        Task<bool> Delete(Guid Id);
        
    }
    public class PositionRepository : IPositionRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public PositionRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<PositionDAO> DynamicFilter(IQueryable<PositionDAO> query, PositionFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.LegalEntityId != null)
                query = query.Where(q => q.LegalEntityId, filter.LegalEntityId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<PositionDAO> DynamicOrder(IQueryable<PositionDAO> query,  PositionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case PositionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PositionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case PositionOrder.Description:
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
                        
                        case PositionOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PositionOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case PositionOrder.Description:
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

        private async Task<List<Position>> DynamicSelect(IQueryable<PositionDAO> query, PositionFilter filter)
        {
            List <Position> Positions = await query.Select(q => new Position()
            {
                
                Id = filter.Selects.Contains(PositionSelect.Id) ? q.Id : default(Guid),
                LegalEntityId = filter.Selects.Contains(PositionSelect.LegalEntity) ? q.LegalEntityId : default(Guid),
                Code = filter.Selects.Contains(PositionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PositionSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(PositionSelect.Description) ? q.Description : default(string),
                BusinessGroupId = filter.Selects.Contains(PositionSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return Positions;
        }

        public async Task<int> Count(PositionFilter filter)
        {
            IQueryable <PositionDAO> PositionDAOs = ERPContext.Position;
            PositionDAOs = DynamicFilter(PositionDAOs, filter);
            return await PositionDAOs.CountAsync();
        }

        public async Task<List<Position>> List(PositionFilter filter)
        {
            if (filter == null) return new List<Position>();
            IQueryable<PositionDAO> PositionDAOs = ERPContext.Position;
            PositionDAOs = DynamicFilter(PositionDAOs, filter);
            PositionDAOs = DynamicOrder(PositionDAOs, filter);
            var Positions = await DynamicSelect(PositionDAOs, filter);
            return Positions;
        }

        public async Task<Position> Get(Guid Id)
        {
            Position Position = await ERPContext.Position.Where(l => l.Id == Id).Select(PositionDAO => new Position()
            {
                 
                Id = PositionDAO.Id,
                LegalEntityId = PositionDAO.LegalEntityId,
                Code = PositionDAO.Code,
                Name = PositionDAO.Name,
                Description = PositionDAO.Description,
                BusinessGroupId = PositionDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return Position;
        }

        public async Task<bool> Create(Position Position)
        {
            PositionDAO PositionDAO = new PositionDAO();
            
            PositionDAO.Id = Position.Id;
            PositionDAO.LegalEntityId = Position.LegalEntityId;
            PositionDAO.Code = Position.Code;
            PositionDAO.Name = Position.Name;
            PositionDAO.Description = Position.Description;
            PositionDAO.BusinessGroupId = Position.BusinessGroupId;
            PositionDAO.Disabled = false;
            
            await ERPContext.Position.AddAsync(PositionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Position Position)
        {
            PositionDAO PositionDAO = ERPContext.Position.Where(b => b.Id == Position.Id).FirstOrDefault();
            
            PositionDAO.Id = Position.Id;
            PositionDAO.LegalEntityId = Position.LegalEntityId;
            PositionDAO.Code = Position.Code;
            PositionDAO.Name = Position.Name;
            PositionDAO.Description = Position.Description;
            PositionDAO.BusinessGroupId = Position.BusinessGroupId;
            PositionDAO.Disabled = false;
            ERPContext.Position.Update(PositionDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            PositionDAO PositionDAO = await ERPContext.Position.Where(x => x.Id == Id).FirstOrDefaultAsync();
            PositionDAO.Disabled = true;
            ERPContext.Position.Update(PositionDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
