
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
    public interface IJobLevelRepository
    {
        Task<int> Count(JobLevelFilter JobLevelFilter);
        Task<List<JobLevel>> List(JobLevelFilter JobLevelFilter);
        Task<JobLevel> Get(Guid Id);
        Task<bool> Create(JobLevel JobLevel);
        Task<bool> Update(JobLevel JobLevel);
        Task<bool> Delete(Guid Id);
        
    }
    public class JobLevelRepository : IJobLevelRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public JobLevelRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<JobLevelDAO> DynamicFilter(IQueryable<JobLevelDAO> query, JobLevelFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Level != null)
                query = query.Where(q => q.Level, filter.Level);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<JobLevelDAO> DynamicOrder(IQueryable<JobLevelDAO> query,  JobLevelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case JobLevelOrder.Level:
                            query = query.OrderBy(q => q.Level);
                            break;
                        case JobLevelOrder.Description:
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
                        
                        case JobLevelOrder.Level:
                            query = query.OrderByDescending(q => q.Level);
                            break;
                        case JobLevelOrder.Description:
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

        private async Task<List<JobLevel>> DynamicSelect(IQueryable<JobLevelDAO> query, JobLevelFilter filter)
        {
            List <JobLevel> JobLevels = await query.Select(q => new JobLevel()
            {
                
                Id = filter.Selects.Contains(JobLevelSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(JobLevelSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Level = filter.Selects.Contains(JobLevelSelect.Level) ? q.Level : default(double),
                Description = filter.Selects.Contains(JobLevelSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return JobLevels;
        }

        public async Task<int> Count(JobLevelFilter filter)
        {
            IQueryable <JobLevelDAO> JobLevelDAOs = ERPContext.JobLevel;
            JobLevelDAOs = DynamicFilter(JobLevelDAOs, filter);
            return await JobLevelDAOs.CountAsync();
        }

        public async Task<List<JobLevel>> List(JobLevelFilter filter)
        {
            if (filter == null) return new List<JobLevel>();
            IQueryable<JobLevelDAO> JobLevelDAOs = ERPContext.JobLevel;
            JobLevelDAOs = DynamicFilter(JobLevelDAOs, filter);
            JobLevelDAOs = DynamicOrder(JobLevelDAOs, filter);
            var JobLevels = await DynamicSelect(JobLevelDAOs, filter);
            return JobLevels;
        }

        public async Task<JobLevel> Get(Guid Id)
        {
            JobLevel JobLevel = await ERPContext.JobLevel.Where(l => l.Id == Id).Select(JobLevelDAO => new JobLevel()
            {
                 
                Id = JobLevelDAO.Id,
                BusinessGroupId = JobLevelDAO.BusinessGroupId,
                Level = JobLevelDAO.Level,
                Description = JobLevelDAO.Description,
            }).FirstOrDefaultAsync();
            return JobLevel;
        }

        public async Task<bool> Create(JobLevel JobLevel)
        {
            JobLevelDAO JobLevelDAO = new JobLevelDAO();
            
            JobLevelDAO.Id = JobLevel.Id;
            JobLevelDAO.BusinessGroupId = JobLevel.BusinessGroupId;
            JobLevelDAO.Level = JobLevel.Level;
            JobLevelDAO.Description = JobLevel.Description;
            JobLevelDAO.Disabled = false;
            
            await ERPContext.JobLevel.AddAsync(JobLevelDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(JobLevel JobLevel)
        {
            JobLevelDAO JobLevelDAO = ERPContext.JobLevel.Where(b => b.Id == JobLevel.Id).FirstOrDefault();
            
            JobLevelDAO.Id = JobLevel.Id;
            JobLevelDAO.BusinessGroupId = JobLevel.BusinessGroupId;
            JobLevelDAO.Level = JobLevel.Level;
            JobLevelDAO.Description = JobLevel.Description;
            JobLevelDAO.Disabled = false;
            ERPContext.JobLevel.Update(JobLevelDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            JobLevelDAO JobLevelDAO = await ERPContext.JobLevel.Where(x => x.Id == Id).FirstOrDefaultAsync();
            JobLevelDAO.Disabled = true;
            ERPContext.JobLevel.Update(JobLevelDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
