
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
    public interface IEmployeeRepository
    {
        Task<int> Count(EmployeeFilter EmployeeFilter);
        Task<List<Employee>> List(EmployeeFilter EmployeeFilter);
        Task<Employee> Get(Guid Id);
        Task<bool> Create(Employee Employee);
        Task<bool> Update(Employee Employee);
        Task<bool> Delete(Guid Id);
        
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public EmployeeRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<EmployeeDAO> DynamicFilter(IQueryable<EmployeeDAO> query, EmployeeFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Disabled.HasValue)
                query = query.Where(q => q.Disabled == filter.Disabled.Value);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.DisplayName != null)
                query = query.Where(q => q.DisplayName, filter.DisplayName);
            if (filter.BusinessGroupId != null)
                query = query.Where(q => q.BusinessGroupId, filter.BusinessGroupId);
            if (filter.JobTitleId.HasValue)
                query = query.Where(q => q.JobTitleId.HasValue && q.JobTitleId.Value == filter.JobTitleId.Value);
            if (filter.JobTitleId != null)
                query = query.Where(q => q.JobTitleId, filter.JobTitleId);
            if (filter.JobLevelId.HasValue)
                query = query.Where(q => q.JobLevelId.HasValue && q.JobLevelId.Value == filter.JobLevelId.Value);
            if (filter.JobLevelId != null)
                query = query.Where(q => q.JobLevelId, filter.JobLevelId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Gender.HasValue)
                query = query.Where(q => q.Gender == filter.Gender.Value);
            if (filter.IdNumber != null)
                query = query.Where(q => q.IdNumber, filter.IdNumber);
            if (filter.IssueDate.HasValue)
                query = query.Where(q => q.IssueDate.HasValue && q.IssueDate.Value == filter.IssueDate.Value);
            if (filter.IssueDate != null)
                query = query.Where(q => q.IssueDate, filter.IssueDate);
            if (filter.IssueLocation != null)
                query = query.Where(q => q.IssueLocation, filter.IssueLocation);
            if (filter.TaxCode != null)
                query = query.Where(q => q.TaxCode, filter.TaxCode);
            if (filter.Salary.HasValue)
                query = query.Where(q => q.Salary.HasValue && q.Salary.Value == filter.Salary.Value);
            if (filter.Salary != null)
                query = query.Where(q => q.Salary, filter.Salary);
            if (filter.SalaryFactor.HasValue)
                query = query.Where(q => q.SalaryFactor.HasValue && q.SalaryFactor.Value == filter.SalaryFactor.Value);
            if (filter.SalaryFactor != null)
                query = query.Where(q => q.SalaryFactor, filter.SalaryFactor);
            if (filter.InsuranceSalary.HasValue)
                query = query.Where(q => q.InsuranceSalary.HasValue && q.InsuranceSalary.Value == filter.InsuranceSalary.Value);
            if (filter.InsuranceSalary != null)
                query = query.Where(q => q.InsuranceSalary, filter.InsuranceSalary);
            if (filter.NumberDependentPerson.HasValue)
                query = query.Where(q => q.NumberDependentPerson.HasValue && q.NumberDependentPerson.Value == filter.NumberDependentPerson.Value);
            if (filter.NumberDependentPerson != null)
                query = query.Where(q => q.NumberDependentPerson, filter.NumberDependentPerson);
            if (filter.Dob != null)
                query = query.Where(q => q.Dob, filter.Dob);
            return query;
        }
        private IQueryable<EmployeeDAO> DynamicOrder(IQueryable<EmployeeDAO> query,  EmployeeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case EmployeeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case EmployeeOrder.DisplayName:
                            query = query.OrderBy(q => q.DisplayName);
                            break;
                        case EmployeeOrder.JobTitleId:
                            query = query.OrderBy(q => q.JobTitleId);
                            break;
                        case EmployeeOrder.JobLevelId:
                            query = query.OrderBy(q => q.JobLevelId);
                            break;
                        case EmployeeOrder.IdNumber:
                            query = query.OrderBy(q => q.IdNumber);
                            break;
                        case EmployeeOrder.IssueDate:
                            query = query.OrderBy(q => q.IssueDate);
                            break;
                        case EmployeeOrder.IssueLocation:
                            query = query.OrderBy(q => q.IssueLocation);
                            break;
                        case EmployeeOrder.TaxCode:
                            query = query.OrderBy(q => q.TaxCode);
                            break;
                        case EmployeeOrder.Salary:
                            query = query.OrderBy(q => q.Salary);
                            break;
                        case EmployeeOrder.SalaryFactor:
                            query = query.OrderBy(q => q.SalaryFactor);
                            break;
                        case EmployeeOrder.InsuranceSalary:
                            query = query.OrderBy(q => q.InsuranceSalary);
                            break;
                        case EmployeeOrder.NumberDependentPerson:
                            query = query.OrderBy(q => q.NumberDependentPerson);
                            break;
                        case EmployeeOrder.Dob:
                            query = query.OrderBy(q => q.Dob);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case EmployeeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case EmployeeOrder.DisplayName:
                            query = query.OrderByDescending(q => q.DisplayName);
                            break;
                        case EmployeeOrder.JobTitleId:
                            query = query.OrderByDescending(q => q.JobTitleId);
                            break;
                        case EmployeeOrder.JobLevelId:
                            query = query.OrderByDescending(q => q.JobLevelId);
                            break;
                        case EmployeeOrder.IdNumber:
                            query = query.OrderByDescending(q => q.IdNumber);
                            break;
                        case EmployeeOrder.IssueDate:
                            query = query.OrderByDescending(q => q.IssueDate);
                            break;
                        case EmployeeOrder.IssueLocation:
                            query = query.OrderByDescending(q => q.IssueLocation);
                            break;
                        case EmployeeOrder.TaxCode:
                            query = query.OrderByDescending(q => q.TaxCode);
                            break;
                        case EmployeeOrder.Salary:
                            query = query.OrderByDescending(q => q.Salary);
                            break;
                        case EmployeeOrder.SalaryFactor:
                            query = query.OrderByDescending(q => q.SalaryFactor);
                            break;
                        case EmployeeOrder.InsuranceSalary:
                            query = query.OrderByDescending(q => q.InsuranceSalary);
                            break;
                        case EmployeeOrder.NumberDependentPerson:
                            query = query.OrderByDescending(q => q.NumberDependentPerson);
                            break;
                        case EmployeeOrder.Dob:
                            query = query.OrderByDescending(q => q.Dob);
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

        private async Task<List<Employee>> DynamicSelect(IQueryable<EmployeeDAO> query, EmployeeFilter filter)
        {
            List <Employee> Employees = await query.Select(q => new Employee()
            {
                
                Id = filter.Selects.Contains(EmployeeSelect.Id) ? q.Id : default(Guid),
                Code = filter.Selects.Contains(EmployeeSelect.Code) ? q.Code : default(string),
                DisplayName = filter.Selects.Contains(EmployeeSelect.DisplayName) ? q.DisplayName : default(string),
                BusinessGroupId = filter.Selects.Contains(EmployeeSelect.BusinessGroup) ? q.BusinessGroupId : default(Guid),
                JobTitleId = filter.Selects.Contains(EmployeeSelect.JobTitle) ? q.JobTitleId : default(Guid?),
                JobLevelId = filter.Selects.Contains(EmployeeSelect.JobLevel) ? q.JobLevelId : default(Guid?),
                StatusId = filter.Selects.Contains(EmployeeSelect.Status) ? q.StatusId : default(Guid),
                IdNumber = filter.Selects.Contains(EmployeeSelect.IdNumber) ? q.IdNumber : default(string),
                IssueDate = filter.Selects.Contains(EmployeeSelect.IssueDate) ? q.IssueDate : default(Guid?),
                IssueLocation = filter.Selects.Contains(EmployeeSelect.IssueLocation) ? q.IssueLocation : default(string),
                TaxCode = filter.Selects.Contains(EmployeeSelect.TaxCode) ? q.TaxCode : default(string),
                Salary = filter.Selects.Contains(EmployeeSelect.Salary) ? q.Salary : default(Guid?),
                SalaryFactor = filter.Selects.Contains(EmployeeSelect.SalaryFactor) ? q.SalaryFactor : default(Guid?),
                InsuranceSalary = filter.Selects.Contains(EmployeeSelect.InsuranceSalary) ? q.InsuranceSalary : default(Guid?),
                NumberDependentPerson = filter.Selects.Contains(EmployeeSelect.NumberDependentPerson) ? q.NumberDependentPerson : default(Guid?),
                Dob = filter.Selects.Contains(EmployeeSelect.Dob) ? q.Dob : default(DateTime),
            }).ToListAsync();
            return Employees;
        }

        public async Task<int> Count(EmployeeFilter filter)
        {
            IQueryable <EmployeeDAO> EmployeeDAOs = ERPContext.Employee;
            EmployeeDAOs = DynamicFilter(EmployeeDAOs, filter);
            return await EmployeeDAOs.CountAsync();
        }

        public async Task<List<Employee>> List(EmployeeFilter filter)
        {
            if (filter == null) return new List<Employee>();
            IQueryable<EmployeeDAO> EmployeeDAOs = ERPContext.Employee;
            EmployeeDAOs = DynamicFilter(EmployeeDAOs, filter);
            EmployeeDAOs = DynamicOrder(EmployeeDAOs, filter);
            var Employees = await DynamicSelect(EmployeeDAOs, filter);
            return Employees;
        }

        public async Task<Employee> Get(Guid Id)
        {
            Employee Employee = await ERPContext.Employee.Where(l => l.Id == Id).Select(EmployeeDAO => new Employee()
            {
                 
                Id = EmployeeDAO.Id,
                Code = EmployeeDAO.Code,
                DisplayName = EmployeeDAO.DisplayName,
                BusinessGroupId = EmployeeDAO.BusinessGroupId,
                JobTitleId = EmployeeDAO.JobTitleId,
                JobLevelId = EmployeeDAO.JobLevelId,
                StatusId = EmployeeDAO.StatusId,
                IdNumber = EmployeeDAO.IdNumber,
                IssueDate = EmployeeDAO.IssueDate,
                IssueLocation = EmployeeDAO.IssueLocation,
                TaxCode = EmployeeDAO.TaxCode,
                Salary = EmployeeDAO.Salary,
                SalaryFactor = EmployeeDAO.SalaryFactor,
                InsuranceSalary = EmployeeDAO.InsuranceSalary,
                NumberDependentPerson = EmployeeDAO.NumberDependentPerson,
                Dob = EmployeeDAO.Dob,
            }).FirstOrDefaultAsync();
            return Employee;
        }

        public async Task<bool> Create(Employee Employee)
        {
            EmployeeDAO EmployeeDAO = new EmployeeDAO();
            
            EmployeeDAO.Id = Employee.Id;
            EmployeeDAO.Code = Employee.Code;
            EmployeeDAO.DisplayName = Employee.DisplayName;
            EmployeeDAO.BusinessGroupId = Employee.BusinessGroupId;
            EmployeeDAO.JobTitleId = Employee.JobTitleId;
            EmployeeDAO.JobLevelId = Employee.JobLevelId;
            EmployeeDAO.StatusId = Employee.StatusId;
            EmployeeDAO.IdNumber = Employee.IdNumber;
            EmployeeDAO.IssueDate = Employee.IssueDate;
            EmployeeDAO.IssueLocation = Employee.IssueLocation;
            EmployeeDAO.TaxCode = Employee.TaxCode;
            EmployeeDAO.Salary = Employee.Salary;
            EmployeeDAO.SalaryFactor = Employee.SalaryFactor;
            EmployeeDAO.InsuranceSalary = Employee.InsuranceSalary;
            EmployeeDAO.NumberDependentPerson = Employee.NumberDependentPerson;
            EmployeeDAO.Dob = Employee.Dob;
            EmployeeDAO.Disabled = false;
            
            await ERPContext.Employee.AddAsync(EmployeeDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Employee Employee)
        {
            EmployeeDAO EmployeeDAO = ERPContext.Employee.Where(b => b.Id == Employee.Id).FirstOrDefault();
            
            EmployeeDAO.Id = Employee.Id;
            EmployeeDAO.Code = Employee.Code;
            EmployeeDAO.DisplayName = Employee.DisplayName;
            EmployeeDAO.BusinessGroupId = Employee.BusinessGroupId;
            EmployeeDAO.JobTitleId = Employee.JobTitleId;
            EmployeeDAO.JobLevelId = Employee.JobLevelId;
            EmployeeDAO.StatusId = Employee.StatusId;
            EmployeeDAO.IdNumber = Employee.IdNumber;
            EmployeeDAO.IssueDate = Employee.IssueDate;
            EmployeeDAO.IssueLocation = Employee.IssueLocation;
            EmployeeDAO.TaxCode = Employee.TaxCode;
            EmployeeDAO.Salary = Employee.Salary;
            EmployeeDAO.SalaryFactor = Employee.SalaryFactor;
            EmployeeDAO.InsuranceSalary = Employee.InsuranceSalary;
            EmployeeDAO.NumberDependentPerson = Employee.NumberDependentPerson;
            EmployeeDAO.Dob = Employee.Dob;
            EmployeeDAO.Disabled = false;
            ERPContext.Employee.Update(EmployeeDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            EmployeeDAO EmployeeDAO = await ERPContext.Employee.Where(x => x.Id == Id).FirstOrDefaultAsync();
            EmployeeDAO.Disabled = true;
            ERPContext.Employee.Update(EmployeeDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
