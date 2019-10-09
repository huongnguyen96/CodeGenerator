
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
    public interface IOperationRepository
    {
        Task<int> Count(OperationFilter OperationFilter);
        Task<List<Operation>> List(OperationFilter OperationFilter);
        Task<Operation> Get(Guid Id);
        Task<bool> Create(Operation Operation);
        Task<bool> Update(Operation Operation);
        Task<bool> Delete(Guid Id);
        
    }
    public class OperationRepository : IOperationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public OperationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<OperationDAO> DynamicFilter(IQueryable<OperationDAO> query, OperationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            return query;
        }
        private IQueryable<OperationDAO> DynamicOrder(IQueryable<OperationDAO> query,  OperationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case OperationOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case OperationOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
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

        private async Task<List<Operation>> DynamicSelect(IQueryable<OperationDAO> query, OperationFilter filter)
        {
            List <Operation> Operations = await query.Select(q => new Operation()
            {
                
                Id = filter.Selects.Contains(OperationSelect.Id) ? q.Id : default(Guid),
                Name = filter.Selects.Contains(OperationSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Operations;
        }

        public async Task<int> Count(OperationFilter filter)
        {
            IQueryable <OperationDAO> OperationDAOs = ERPContext.Operation;
            OperationDAOs = DynamicFilter(OperationDAOs, filter);
            return await OperationDAOs.CountAsync();
        }

        public async Task<List<Operation>> List(OperationFilter filter)
        {
            if (filter == null) return new List<Operation>();
            IQueryable<OperationDAO> OperationDAOs = ERPContext.Operation;
            OperationDAOs = DynamicFilter(OperationDAOs, filter);
            OperationDAOs = DynamicOrder(OperationDAOs, filter);
            var Operations = await DynamicSelect(OperationDAOs, filter);
            return Operations;
        }

        public async Task<Operation> Get(Guid Id)
        {
            Operation Operation = await ERPContext.Operation.Where(l => l.Id == Id).Select(OperationDAO => new Operation()
            {
                 
                Id = OperationDAO.Id,
                Name = OperationDAO.Name,
            }).FirstOrDefaultAsync();
            return Operation;
        }

        public async Task<bool> Create(Operation Operation)
        {
            OperationDAO OperationDAO = new OperationDAO();
            
            OperationDAO.Id = Operation.Id;
            OperationDAO.Name = Operation.Name;
            OperationDAO.Disabled = false;
            
            await ERPContext.Operation.AddAsync(OperationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Operation Operation)
        {
            OperationDAO OperationDAO = ERPContext.Operation.Where(b => b.Id == Operation.Id).FirstOrDefault();
            
            OperationDAO.Id = Operation.Id;
            OperationDAO.Name = Operation.Name;
            OperationDAO.Disabled = false;
            ERPContext.Operation.Update(OperationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            OperationDAO OperationDAO = await ERPContext.Operation.Where(x => x.Id == Id).FirstOrDefaultAsync();
            OperationDAO.Disabled = true;
            ERPContext.Operation.Update(OperationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
