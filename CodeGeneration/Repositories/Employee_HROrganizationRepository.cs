
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
    public interface IEmployee_HROrganizationRepository
    {
        Task<int> Count(Employee_HROrganizationFilter Employee_HROrganizationFilter);
        Task<List<Employee_HROrganization>> List(Employee_HROrganizationFilter Employee_HROrganizationFilter);
        Task<Employee_HROrganization> Get(Guid Id);
        Task<bool> Create(Employee_HROrganization Employee_HROrganization);
        Task<bool> Update(Employee_HROrganization Employee_HROrganization);
        Task<bool> Delete(Guid Id);
        
    }
    public class Employee_HROrganizationRepository : IEmployee_HROrganizationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public Employee_HROrganizationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<Employee_HROrganizationDAO> DynamicFilter(IQueryable<Employee_HROrganizationDAO> query, Employee_HROrganizationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.HROrganizationId != null)
                query = query.Where(q => q.HROrganizationId, filter.HROrganizationId);
            if (filter.EmployeeId != null)
                query = query.Where(q => q.EmployeeId, filter.EmployeeId);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            return query;
        }
        private IQueryable<Employee_HROrganizationDAO> DynamicOrder(IQueryable<Employee_HROrganizationDAO> query,  Employee_HROrganizationFilter filter)
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

        private async Task<List<Employee_HROrganization>> DynamicSelect(IQueryable<Employee_HROrganizationDAO> query, Employee_HROrganizationFilter filter)
        {
            List <Employee_HROrganization> Employee_HROrganizations = await query.Select(q => new Employee_HROrganization()
            {
                
                HROrganizationId = filter.Selects.Contains(Employee_HROrganizationSelect.HROrganization) ? q.HROrganizationId : default(Guid),
                EmployeeId = filter.Selects.Contains(Employee_HROrganizationSelect.Employee) ? q.EmployeeId : default(Guid),
                BusinessGroupId = filter.Selects.Contains(Employee_HROrganizationSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
            }).ToListAsync();
            return Employee_HROrganizations;
        }

        public async Task<int> Count(Employee_HROrganizationFilter filter)
        {
            IQueryable <Employee_HROrganizationDAO> Employee_HROrganizationDAOs = ERPContext.Employee_HROrganization;
            Employee_HROrganizationDAOs = DynamicFilter(Employee_HROrganizationDAOs, filter);
            return await Employee_HROrganizationDAOs.CountAsync();
        }

        public async Task<List<Employee_HROrganization>> List(Employee_HROrganizationFilter filter)
        {
            if (filter == null) return new List<Employee_HROrganization>();
            IQueryable<Employee_HROrganizationDAO> Employee_HROrganizationDAOs = ERPContext.Employee_HROrganization;
            Employee_HROrganizationDAOs = DynamicFilter(Employee_HROrganizationDAOs, filter);
            Employee_HROrganizationDAOs = DynamicOrder(Employee_HROrganizationDAOs, filter);
            var Employee_HROrganizations = await DynamicSelect(Employee_HROrganizationDAOs, filter);
            return Employee_HROrganizations;
        }

        public async Task<Employee_HROrganization> Get(Guid Id)
        {
            Employee_HROrganization Employee_HROrganization = await ERPContext.Employee_HROrganization.Where(l => l.Id == Id).Select(Employee_HROrganizationDAO => new Employee_HROrganization()
            {
                 
                HROrganizationId = Employee_HROrganizationDAO.HROrganizationId,
                EmployeeId = Employee_HROrganizationDAO.EmployeeId,
                BusinessGroupId = Employee_HROrganizationDAO.BusinessGroupId,
            }).FirstOrDefaultAsync();
            return Employee_HROrganization;
        }

        public async Task<bool> Create(Employee_HROrganization Employee_HROrganization)
        {
            Employee_HROrganizationDAO Employee_HROrganizationDAO = new Employee_HROrganizationDAO();
            
            Employee_HROrganizationDAO.HROrganizationId = Employee_HROrganization.HROrganizationId;
            Employee_HROrganizationDAO.EmployeeId = Employee_HROrganization.EmployeeId;
            Employee_HROrganizationDAO.BusinessGroupId = Employee_HROrganization.BusinessGroupId;
            Employee_HROrganizationDAO.Disabled = false;
            
            await ERPContext.Employee_HROrganization.AddAsync(Employee_HROrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Employee_HROrganization Employee_HROrganization)
        {
            Employee_HROrganizationDAO Employee_HROrganizationDAO = ERPContext.Employee_HROrganization.Where(b => b.Id == Employee_HROrganization.Id).FirstOrDefault();
            
            Employee_HROrganizationDAO.HROrganizationId = Employee_HROrganization.HROrganizationId;
            Employee_HROrganizationDAO.EmployeeId = Employee_HROrganization.EmployeeId;
            Employee_HROrganizationDAO.BusinessGroupId = Employee_HROrganization.BusinessGroupId;
            Employee_HROrganizationDAO.Disabled = false;
            ERPContext.Employee_HROrganization.Update(Employee_HROrganizationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            Employee_HROrganizationDAO Employee_HROrganizationDAO = await ERPContext.Employee_HROrganization.Where(x => x.Id == Id).FirstOrDefaultAsync();
            Employee_HROrganizationDAO.Disabled = true;
            ERPContext.Employee_HROrganization.Update(Employee_HROrganizationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
