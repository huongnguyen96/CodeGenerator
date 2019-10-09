
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
    public interface ISystemConfigurationRepository
    {
        Task<int> Count(SystemConfigurationFilter SystemConfigurationFilter);
        Task<List<SystemConfiguration>> List(SystemConfigurationFilter SystemConfigurationFilter);
        Task<SystemConfiguration> Get(Guid Id);
        Task<bool> Create(SystemConfiguration SystemConfiguration);
        Task<bool> Update(SystemConfiguration SystemConfiguration);
        Task<bool> Delete(Guid Id);
        
    }
    public class SystemConfigurationRepository : ISystemConfigurationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public SystemConfigurationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<SystemConfigurationDAO> DynamicFilter(IQueryable<SystemConfigurationDAO> query, SystemConfigurationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Key != null)
                query = query.Where(q => q.Key, filter.Key);
            if (filter.Value != null)
                query = query.Where(q => q.Value, filter.Value);
            return query;
        }
        private IQueryable<SystemConfigurationDAO> DynamicOrder(IQueryable<SystemConfigurationDAO> query,  SystemConfigurationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case SystemConfigurationOrder.Key:
                            query = query.OrderBy(q => q.Key);
                            break;
                        case SystemConfigurationOrder.Value:
                            query = query.OrderBy(q => q.Value);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case SystemConfigurationOrder.Key:
                            query = query.OrderByDescending(q => q.Key);
                            break;
                        case SystemConfigurationOrder.Value:
                            query = query.OrderByDescending(q => q.Value);
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

        private async Task<List<SystemConfiguration>> DynamicSelect(IQueryable<SystemConfigurationDAO> query, SystemConfigurationFilter filter)
        {
            List <SystemConfiguration> SystemConfigurations = await query.Select(q => new SystemConfiguration()
            {
                
                Key = filter.Selects.Contains(SystemConfigurationSelect.Key) ? q.Key : default(string),
                Value = filter.Selects.Contains(SystemConfigurationSelect.Value) ? q.Value : default(string),
            }).ToListAsync();
            return SystemConfigurations;
        }

        public async Task<int> Count(SystemConfigurationFilter filter)
        {
            IQueryable <SystemConfigurationDAO> SystemConfigurationDAOs = ERPContext.SystemConfiguration;
            SystemConfigurationDAOs = DynamicFilter(SystemConfigurationDAOs, filter);
            return await SystemConfigurationDAOs.CountAsync();
        }

        public async Task<List<SystemConfiguration>> List(SystemConfigurationFilter filter)
        {
            if (filter == null) return new List<SystemConfiguration>();
            IQueryable<SystemConfigurationDAO> SystemConfigurationDAOs = ERPContext.SystemConfiguration;
            SystemConfigurationDAOs = DynamicFilter(SystemConfigurationDAOs, filter);
            SystemConfigurationDAOs = DynamicOrder(SystemConfigurationDAOs, filter);
            var SystemConfigurations = await DynamicSelect(SystemConfigurationDAOs, filter);
            return SystemConfigurations;
        }

        public async Task<SystemConfiguration> Get(Guid Id)
        {
            SystemConfiguration SystemConfiguration = await ERPContext.SystemConfiguration.Where(l => l.Id == Id).Select(SystemConfigurationDAO => new SystemConfiguration()
            {
                 
                Key = SystemConfigurationDAO.Key,
                Value = SystemConfigurationDAO.Value,
            }).FirstOrDefaultAsync();
            return SystemConfiguration;
        }

        public async Task<bool> Create(SystemConfiguration SystemConfiguration)
        {
            SystemConfigurationDAO SystemConfigurationDAO = new SystemConfigurationDAO();
            
            SystemConfigurationDAO.Key = SystemConfiguration.Key;
            SystemConfigurationDAO.Value = SystemConfiguration.Value;
            SystemConfigurationDAO.Disabled = false;
            
            await ERPContext.SystemConfiguration.AddAsync(SystemConfigurationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(SystemConfiguration SystemConfiguration)
        {
            SystemConfigurationDAO SystemConfigurationDAO = ERPContext.SystemConfiguration.Where(b => b.Id == SystemConfiguration.Id).FirstOrDefault();
            
            SystemConfigurationDAO.Key = SystemConfiguration.Key;
            SystemConfigurationDAO.Value = SystemConfiguration.Value;
            SystemConfigurationDAO.Disabled = false;
            ERPContext.SystemConfiguration.Update(SystemConfigurationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            SystemConfigurationDAO SystemConfigurationDAO = await ERPContext.SystemConfiguration.Where(x => x.Id == Id).FirstOrDefaultAsync();
            SystemConfigurationDAO.Disabled = true;
            ERPContext.SystemConfiguration.Update(SystemConfigurationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
