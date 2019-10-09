
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
    public interface IProjectOrganizationRepository
    {
        Task<int> Count(ProjectOrganizationFilter ProjectOrganizationFilter);
        Task<List<ProjectOrganization>> List(ProjectOrganizationFilter ProjectOrganizationFilter);
        Task<ProjectOrganization> Get(Guid Id);
        Task<bool> Create(ProjectOrganization ProjectOrganization);
        Task<bool> Update(ProjectOrganization ProjectOrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class ProjectOrganizationRepository : IProjectOrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public ProjectOrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ProjectOrganizationDAO> DynamicFilter(IQueryable<ProjectOrganizationDAO> query, ProjectOrganizationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.DivisionId != null)
                query = query.Where(q => q.DivisionId, filter.DivisionId);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.ManagerId != null)
                query = query.Where(q => q.ManagerId, filter.ManagerId);
            if (filter.StartDate.HasValue)
                query = query.Where(q => q.StartDate.HasValue && q.StartDate.Value == filter.StartDate.Value);
            if (filter.StartDate != null)
                query = query.Where(q => q.StartDate, filter.StartDate);
            if (filter.EndDate.HasValue)
                query = query.Where(q => q.EndDate.HasValue && q.EndDate.Value == filter.EndDate.Value);
            if (filter.EndDate != null)
                query = query.Where(q => q.EndDate, filter.EndDate);
            return query;
        }
        private IQueryable<ProjectOrganizationDAO> DynamicOrder(IQueryable<ProjectOrganizationDAO> query,  ProjectOrganizationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProjectOrganizationOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProjectOrganizationOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProjectOrganizationOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ProjectOrganizationOrder.StartDate:
                            query = query.OrderBy(q => q.StartDate);
                            break;
                        case ProjectOrganizationOrder.EndDate:
                            query = query.OrderBy(q => q.EndDate);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ProjectOrganizationOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProjectOrganizationOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProjectOrganizationOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ProjectOrganizationOrder.StartDate:
                            query = query.OrderByDescending(q => q.StartDate);
                            break;
                        case ProjectOrganizationOrder.EndDate:
                            query = query.OrderByDescending(q => q.EndDate);
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

        private async Task<List<ProjectOrganization>> DynamicSelect(IQueryable<ProjectOrganizationDAO> query, ProjectOrganizationFilter filter)
        {
            List <ProjectOrganization> ProjectOrganizations = await query.Select(q => new ProjectOrganization()
            {
                
                Id = filter.Selects.Contains(ProjectOrganizationSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(ProjectOrganizationSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProjectOrganizationSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(ProjectOrganizationSelect.Description) ? q.Description : default(string),
                DivisionId = filter.Selects.Contains(ProjectOrganizationSelect.Division) ? q.DivisionId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(ProjectOrganizationSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                ManagerId = filter.Selects.Contains(ProjectOrganizationSelect.Manager) ? q.ManagerId : default(Guid),
                StartDate = filter.Selects.Contains(ProjectOrganizationSelect.StartDate) ? q.StartDate : default(Guid?),
                EndDate = filter.Selects.Contains(ProjectOrganizationSelect.EndDate) ? q.EndDate : default(Guid?),
            }).ToListAsync();
            return ProjectOrganizations;
        }

        public async Task<int> Count(ProjectOrganizationFilter filter)
        {
            IQueryable <ProjectOrganizationDAO> ProjectOrganizationDAOs = ERPContext.ProjectOrganization;
            ProjectOrganizationDAOs = DynamicFilter(ProjectOrganizationDAOs, filter);
            return await ProjectOrganizationDAOs.CountAsync();
        }

        public async Task<List<ProjectOrganization>> List(ProjectOrganizationFilter filter)
        {
            if (filter == null) return new List<ProjectOrganization>();
            IQueryable<ProjectOrganizationDAO> ProjectOrganizationDAOs = ERPContext.ProjectOrganization;
            ProjectOrganizationDAOs = DynamicFilter(ProjectOrganizationDAOs, filter);
            ProjectOrganizationDAOs = DynamicOrder(ProjectOrganizationDAOs, filter);
            var ProjectOrganizations = await DynamicSelect(ProjectOrganizationDAOs, filter);
            return ProjectOrganizations;
        }

        public async Task<ProjectOrganization> Get(Guid Id)
        {
            ProjectOrganization ProjectOrganization = await ERPContext.ProjectOrganization.Where(l => l.Id == Id).Select(ProjectOrganizationDAO => new ProjectOrganization()
            {
                 
                Id = ProjectOrganizationDAO.Id,
                Code = ProjectOrganizationDAO.Code,
                Name = ProjectOrganizationDAO.Name,
                Description = ProjectOrganizationDAO.Description,
                DivisionId = ProjectOrganizationDAO.DivisionId,
                BusinessGroupId = ProjectOrganizationDAO.BusinessGroupId,
                ManagerId = ProjectOrganizationDAO.ManagerId,
                StartDate = ProjectOrganizationDAO.StartDate,
                EndDate = ProjectOrganizationDAO.EndDate,
            }).FirstOrDefaultAsync();
            return ProjectOrganization;
        }

        public async Task<bool> Create(ProjectOrganization ProjectOrganization)
        {
            ProjectOrganizationDAO ProjectOrganizationDAO = new ProjectOrganizationDAO();
            
            ProjectOrganizationDAO.Id = ProjectOrganization.Id;
            ProjectOrganizationDAO.Code = ProjectOrganization.Code;
            ProjectOrganizationDAO.Name = ProjectOrganization.Name;
            ProjectOrganizationDAO.Description = ProjectOrganization.Description;
            ProjectOrganizationDAO.DivisionId = ProjectOrganization.DivisionId;
            ProjectOrganizationDAO.BusinessGroupId = ProjectOrganization.BusinessGroupId;
            ProjectOrganizationDAO.ManagerId = ProjectOrganization.ManagerId;
            ProjectOrganizationDAO.StartDate = ProjectOrganization.StartDate;
            ProjectOrganizationDAO.EndDate = ProjectOrganization.EndDate;
            ProjectOrganizationDAO.Disabled = false;
            
            await ERPContext.ProjectOrganization.AddAsync(ProjectOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ProjectOrganization ProjectOrganization)
        {
            ProjectOrganizationDAO ProjectOrganizationDAO = ERPContext.ProjectOrganization.Where(b => b.Id == ProjectOrganization.Id).FirstOrDefault();
            
            ProjectOrganizationDAO.Id = ProjectOrganization.Id;
            ProjectOrganizationDAO.Code = ProjectOrganization.Code;
            ProjectOrganizationDAO.Name = ProjectOrganization.Name;
            ProjectOrganizationDAO.Description = ProjectOrganization.Description;
            ProjectOrganizationDAO.DivisionId = ProjectOrganization.DivisionId;
            ProjectOrganizationDAO.BusinessGroupId = ProjectOrganization.BusinessGroupId;
            ProjectOrganizationDAO.ManagerId = ProjectOrganization.ManagerId;
            ProjectOrganizationDAO.StartDate = ProjectOrganization.StartDate;
            ProjectOrganizationDAO.EndDate = ProjectOrganization.EndDate;
            ProjectOrganizationDAO.Disabled = false;
            ERPContext.ProjectOrganization.Update(ProjectOrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            ProjectOrganizationDAO ProjectOrganizationDAO = await ERPContext.ProjectOrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ProjectOrganizationDAO.Disabled = true;
            ERPContext.ProjectOrganization.Update(ProjectOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
