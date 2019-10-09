
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
    public interface IEmployee_ProjectOrganizationRepository
    {
        Task<int> Count(Employee_ProjectOrganizationFilter Employee_ProjectOrganizationFilter);
        Task<List<Employee_ProjectOrganization>> List(Employee_ProjectOrganizationFilter Employee_ProjectOrganizationFilter);
        Task<Employee_ProjectOrganization> Get(Guid Id);
        Task<bool> Create(Employee_ProjectOrganization Employee_ProjectOrganization);
        Task<bool> Update(Employee_ProjectOrganization Employee_ProjectOrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class Employee_ProjectOrganizationRepository : IEmployee_ProjectOrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public Employee_ProjectOrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Employee_ProjectOrganizationDAO> DynamicFilter(IQueryable<Employee_ProjectOrganizationDAO> query, Employee_ProjectOrganizationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.EmployeeId != null)
                query = query.Where(q => q.EmployeeId, filter.EmployeeId);
            if (filter.ProjectOrganizationId != null)
                query = query.Where(q => q.ProjectOrganizationId, filter.ProjectOrganizationId);
            return query;
        }
        private IQueryable<Employee_ProjectOrganizationDAO> DynamicOrder(IQueryable<Employee_ProjectOrganizationDAO> query,  Employee_ProjectOrganizationFilter filter)
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

        private async Task<List<Employee_ProjectOrganization>> DynamicSelect(IQueryable<Employee_ProjectOrganizationDAO> query, Employee_ProjectOrganizationFilter filter)
        {
            List <Employee_ProjectOrganization> Employee_ProjectOrganizations = await query.Select(q => new Employee_ProjectOrganization()
            {
                
                EmployeeId = filter.Selects.Contains(Employee_ProjectOrganizationSelect.Employee) ? q.EmployeeId : default(Guid),
                ProjectOrganizationId = filter.Selects.Contains(Employee_ProjectOrganizationSelect.ProjectOrganization) ? q.ProjectOrganizationId : default(Guid),
            }).ToListAsync();
            return Employee_ProjectOrganizations;
        }

        public async Task<int> Count(Employee_ProjectOrganizationFilter filter)
        {
            IQueryable <Employee_ProjectOrganizationDAO> Employee_ProjectOrganizationDAOs = ERPContext.Employee_ProjectOrganization;
            Employee_ProjectOrganizationDAOs = DynamicFilter(Employee_ProjectOrganizationDAOs, filter);
            return await Employee_ProjectOrganizationDAOs.CountAsync();
        }

        public async Task<List<Employee_ProjectOrganization>> List(Employee_ProjectOrganizationFilter filter)
        {
            if (filter == null) return new List<Employee_ProjectOrganization>();
            IQueryable<Employee_ProjectOrganizationDAO> Employee_ProjectOrganizationDAOs = ERPContext.Employee_ProjectOrganization;
            Employee_ProjectOrganizationDAOs = DynamicFilter(Employee_ProjectOrganizationDAOs, filter);
            Employee_ProjectOrganizationDAOs = DynamicOrder(Employee_ProjectOrganizationDAOs, filter);
            var Employee_ProjectOrganizations = await DynamicSelect(Employee_ProjectOrganizationDAOs, filter);
            return Employee_ProjectOrganizations;
        }

        public async Task<Employee_ProjectOrganization> Get(Guid Id)
        {
            Employee_ProjectOrganization Employee_ProjectOrganization = await ERPContext.Employee_ProjectOrganization.Where(l => l.Id == Id).Select(Employee_ProjectOrganizationDAO => new Employee_ProjectOrganization()
            {
                 
                EmployeeId = Employee_ProjectOrganizationDAO.EmployeeId,
                ProjectOrganizationId = Employee_ProjectOrganizationDAO.ProjectOrganizationId,
            }).FirstOrDefaultAsync();
            return Employee_ProjectOrganization;
        }

        public async Task<bool> Create(Employee_ProjectOrganization Employee_ProjectOrganization)
        {
            Employee_ProjectOrganizationDAO Employee_ProjectOrganizationDAO = new Employee_ProjectOrganizationDAO();
            
            Employee_ProjectOrganizationDAO.EmployeeId = Employee_ProjectOrganization.EmployeeId;
            Employee_ProjectOrganizationDAO.ProjectOrganizationId = Employee_ProjectOrganization.ProjectOrganizationId;
            Employee_ProjectOrganizationDAO.Disabled = false;
            
            await ERPContext.Employee_ProjectOrganization.AddAsync(Employee_ProjectOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Employee_ProjectOrganization Employee_ProjectOrganization)
        {
            Employee_ProjectOrganizationDAO Employee_ProjectOrganizationDAO = ERPContext.Employee_ProjectOrganization.Where(b => b.Id == Employee_ProjectOrganization.Id).FirstOrDefault();
            
            Employee_ProjectOrganizationDAO.EmployeeId = Employee_ProjectOrganization.EmployeeId;
            Employee_ProjectOrganizationDAO.ProjectOrganizationId = Employee_ProjectOrganization.ProjectOrganizationId;
            Employee_ProjectOrganizationDAO.Disabled = false;
            ERPContext.Employee_ProjectOrganization.Update(Employee_ProjectOrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            Employee_ProjectOrganizationDAO Employee_ProjectOrganizationDAO = await ERPContext.Employee_ProjectOrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            Employee_ProjectOrganizationDAO.Disabled = true;
            ERPContext.Employee_ProjectOrganization.Update(Employee_ProjectOrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
