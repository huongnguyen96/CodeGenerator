
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
    public interface IAuditLogRepository
    {
        Task<int> Count(AuditLogFilter AuditLogFilter);
        Task<List<AuditLog>> List(AuditLogFilter AuditLogFilter);
        Task<AuditLog> Get(Guid Id);
        Task<bool> Create(AuditLog AuditLog);
        Task<bool> Update(AuditLog AuditLog);
        Task<bool> Delete(Guid Id);
        
    }
    public class AuditLogRepository : IAuditLogRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public AuditLogRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<AuditLogDAO> DynamicFilter(IQueryable<AuditLogDAO> query, AuditLogFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.ClassName != null)
                query = query.Where(q => q.ClassName, filter.ClassName);
            if (filter.MethodName != null)
                query = query.Where(q => q.MethodName, filter.MethodName);
            if (filter.OldData != null)
                query = query.Where(q => q.OldData, filter.OldData);
            if (filter.NewData != null)
                query = query.Where(q => q.NewData, filter.NewData);
            if (filter.Time != null)
                query = query.Where(q => q.Time, filter.Time);
            return query;
        }
        private IQueryable<AuditLogDAO> DynamicOrder(IQueryable<AuditLogDAO> query,  AuditLogFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case AuditLogOrder.ClassName:
                            query = query.OrderBy(q => q.ClassName);
                            break;
                        case AuditLogOrder.MethodName:
                            query = query.OrderBy(q => q.MethodName);
                            break;
                        case AuditLogOrder.OldData:
                            query = query.OrderBy(q => q.OldData);
                            break;
                        case AuditLogOrder.NewData:
                            query = query.OrderBy(q => q.NewData);
                            break;
                        case AuditLogOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case AuditLogOrder.ClassName:
                            query = query.OrderByDescending(q => q.ClassName);
                            break;
                        case AuditLogOrder.MethodName:
                            query = query.OrderByDescending(q => q.MethodName);
                            break;
                        case AuditLogOrder.OldData:
                            query = query.OrderByDescending(q => q.OldData);
                            break;
                        case AuditLogOrder.NewData:
                            query = query.OrderByDescending(q => q.NewData);
                            break;
                        case AuditLogOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
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

        private async Task<List<AuditLog>> DynamicSelect(IQueryable<AuditLogDAO> query, AuditLogFilter filter)
        {
            List <AuditLog> AuditLogs = await query.Select(q => new AuditLog()
            {
                
                Id = filter.Selects.Contains(AuditLogSelect.Id) ? q.Id : default(Guid),
                UserId = filter.Selects.Contains(AuditLogSelect.User) ? q.UserId : default(Guid),
                ClassName = filter.Selects.Contains(AuditLogSelect.ClassName) ? q.ClassName : default(string),
                MethodName = filter.Selects.Contains(AuditLogSelect.MethodName) ? q.MethodName : default(string),
                OldData = filter.Selects.Contains(AuditLogSelect.OldData) ? q.OldData : default(string),
                NewData = filter.Selects.Contains(AuditLogSelect.NewData) ? q.NewData : default(string),
                Time = filter.Selects.Contains(AuditLogSelect.Time) ? q.Time : default(DateTime),
            }).ToListAsync();
            return AuditLogs;
        }

        public async Task<int> Count(AuditLogFilter filter)
        {
            IQueryable <AuditLogDAO> AuditLogDAOs = ERPContext.AuditLog;
            AuditLogDAOs = DynamicFilter(AuditLogDAOs, filter);
            return await AuditLogDAOs.CountAsync();
        }

        public async Task<List<AuditLog>> List(AuditLogFilter filter)
        {
            if (filter == null) return new List<AuditLog>();
            IQueryable<AuditLogDAO> AuditLogDAOs = ERPContext.AuditLog;
            AuditLogDAOs = DynamicFilter(AuditLogDAOs, filter);
            AuditLogDAOs = DynamicOrder(AuditLogDAOs, filter);
            var AuditLogs = await DynamicSelect(AuditLogDAOs, filter);
            return AuditLogs;
        }

        public async Task<AuditLog> Get(Guid Id)
        {
            AuditLog AuditLog = await ERPContext.AuditLog.Where(l => l.Id == Id).Select(AuditLogDAO => new AuditLog()
            {
                 
                Id = AuditLogDAO.Id,
                UserId = AuditLogDAO.UserId,
                ClassName = AuditLogDAO.ClassName,
                MethodName = AuditLogDAO.MethodName,
                OldData = AuditLogDAO.OldData,
                NewData = AuditLogDAO.NewData,
                Time = AuditLogDAO.Time,
            }).FirstOrDefaultAsync();
            return AuditLog;
        }

        public async Task<bool> Create(AuditLog AuditLog)
        {
            AuditLogDAO AuditLogDAO = new AuditLogDAO();
            
            AuditLogDAO.Id = AuditLog.Id;
            AuditLogDAO.UserId = AuditLog.UserId;
            AuditLogDAO.ClassName = AuditLog.ClassName;
            AuditLogDAO.MethodName = AuditLog.MethodName;
            AuditLogDAO.OldData = AuditLog.OldData;
            AuditLogDAO.NewData = AuditLog.NewData;
            AuditLogDAO.Time = AuditLog.Time;
            AuditLogDAO.Disabled = false;
            
            await ERPContext.AuditLog.AddAsync(AuditLogDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(AuditLog AuditLog)
        {
            AuditLogDAO AuditLogDAO = ERPContext.AuditLog.Where(b => b.Id == AuditLog.Id).FirstOrDefault();
            
            AuditLogDAO.Id = AuditLog.Id;
            AuditLogDAO.UserId = AuditLog.UserId;
            AuditLogDAO.ClassName = AuditLog.ClassName;
            AuditLogDAO.MethodName = AuditLog.MethodName;
            AuditLogDAO.OldData = AuditLog.OldData;
            AuditLogDAO.NewData = AuditLog.NewData;
            AuditLogDAO.Time = AuditLog.Time;
            AuditLogDAO.Disabled = false;
            ERPContext.AuditLog.Update(AuditLogDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            AuditLogDAO AuditLogDAO = await ERPContext.AuditLog.Where(x => x.Id == Id).FirstOrDefaultAsync();
            AuditLogDAO.Disabled = true;
            ERPContext.AuditLog.Update(AuditLogDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
