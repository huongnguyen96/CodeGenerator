
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
    public interface IJobTitleRepository
    {
        Task<int> Count(JobTitleFilter JobTitleFilter);
        Task<List<JobTitle>> List(JobTitleFilter JobTitleFilter);
        Task<JobTitle> Get(Guid Id);
        Task<bool> Create(JobTitle JobTitle);
        Task<bool> Update(JobTitle JobTitle);
        Task<bool> Delete(Guid Id);
        
    }
    public class JobTitleRepository : IJobTitleRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public JobTitleRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<JobTitleDAO> DynamicFilter(IQueryable<JobTitleDAO> query, JobTitleFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            return query;
        }
        private IQueryable<JobTitleDAO> DynamicOrder(IQueryable<JobTitleDAO> query,  JobTitleFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case JobTitleOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case JobTitleOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case JobTitleOrder.Description:
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
                        
                        case JobTitleOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case JobTitleOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case JobTitleOrder.Description:
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

        private async Task<List<JobTitle>> DynamicSelect(IQueryable<JobTitleDAO> query, JobTitleFilter filter)
        {
            List <JobTitle> JobTitles = await query.Select(q => new JobTitle()
            {
                
                Id = filter.Selects.Contains(JobTitleSelect.Id) ? q.Id : default(Guid),
                BusinessGroupId = filter.Selects.Contains(JobTitleSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                Code = filter.Selects.Contains(JobTitleSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(JobTitleSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(JobTitleSelect.Description) ? q.Description : default(string),
            }).ToListAsync();
            return JobTitles;
        }

        public async Task<int> Count(JobTitleFilter filter)
        {
            IQueryable <JobTitleDAO> JobTitleDAOs = ERPContext.JobTitle;
            JobTitleDAOs = DynamicFilter(JobTitleDAOs, filter);
            return await JobTitleDAOs.CountAsync();
        }

        public async Task<List<JobTitle>> List(JobTitleFilter filter)
        {
            if (filter == null) return new List<JobTitle>();
            IQueryable<JobTitleDAO> JobTitleDAOs = ERPContext.JobTitle;
            JobTitleDAOs = DynamicFilter(JobTitleDAOs, filter);
            JobTitleDAOs = DynamicOrder(JobTitleDAOs, filter);
            var JobTitles = await DynamicSelect(JobTitleDAOs, filter);
            return JobTitles;
        }

        public async Task<JobTitle> Get(Guid Id)
        {
            JobTitle JobTitle = await ERPContext.JobTitle.Where(l => l.Id == Id).Select(JobTitleDAO => new JobTitle()
            {
                 
                Id = JobTitleDAO.Id,
                BusinessGroupId = JobTitleDAO.BusinessGroupId,
                Code = JobTitleDAO.Code,
                Name = JobTitleDAO.Name,
                Description = JobTitleDAO.Description,
            }).FirstOrDefaultAsync();
            return JobTitle;
        }

        public async Task<bool> Create(JobTitle JobTitle)
        {
            JobTitleDAO JobTitleDAO = new JobTitleDAO();
            
            JobTitleDAO.Id = JobTitle.Id;
            JobTitleDAO.BusinessGroupId = JobTitle.BusinessGroupId;
            JobTitleDAO.Code = JobTitle.Code;
            JobTitleDAO.Name = JobTitle.Name;
            JobTitleDAO.Description = JobTitle.Description;
            JobTitleDAO.Disabled = false;
            
            await ERPContext.JobTitle.AddAsync(JobTitleDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(JobTitle JobTitle)
        {
            JobTitleDAO JobTitleDAO = ERPContext.JobTitle.Where(b => b.Id == JobTitle.Id).FirstOrDefault();
            
            JobTitleDAO.Id = JobTitle.Id;
            JobTitleDAO.BusinessGroupId = JobTitle.BusinessGroupId;
            JobTitleDAO.Code = JobTitle.Code;
            JobTitleDAO.Name = JobTitle.Name;
            JobTitleDAO.Description = JobTitle.Description;
            JobTitleDAO.Disabled = false;
            ERPContext.JobTitle.Update(JobTitleDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            JobTitleDAO JobTitleDAO = await ERPContext.JobTitle.Where(x => x.Id == Id).FirstOrDefaultAsync();
            JobTitleDAO.Disabled = true;
            ERPContext.JobTitle.Update(JobTitleDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
