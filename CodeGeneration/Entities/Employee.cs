
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Employee : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string DisplayName { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid? JobTitleId { get; set; }
		public Guid? JobLevelId { get; set; }
		public Guid StatusId { get; set; }
		public bool Gender { get; set; }
		public string IdNumber { get; set; }
		public Guid? IssueDate { get; set; }
		public string IssueLocation { get; set; }
		public string TaxCode { get; set; }
		public Guid? Salary { get; set; }
		public Guid? SalaryFactor { get; set; }
		public Guid? InsuranceSalary { get; set; }
		public Guid? NumberDependentPerson { get; set; }
		public DateTime Dob { get; set; }
		
    }

    public class EmployeeFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter DisplayName { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter JobTitleId { get; set; }
		public GuidFilter JobLevelId { get; set; }
		public GuidFilter StatusId { get; set; }
		public bool? Gender { get; set; }
		public StringFilter IdNumber { get; set; }
		public GuidFilter IssueDate { get; set; }
		public StringFilter IssueLocation { get; set; }
		public StringFilter TaxCode { get; set; }
		public GuidFilter Salary { get; set; }
		public GuidFilter SalaryFactor { get; set; }
		public GuidFilter InsuranceSalary { get; set; }
		public GuidFilter NumberDependentPerson { get; set; }
		public DateTimeFilter Dob { get; set; }
		
        public EmployeeOrder OrderBy {get; set;}
        public EmployeeSelect Selects {get; set;}
    }

    public enum EmployeeOrder
    {
        
        Disabled,
        Code,
        DisplayName,
        Gender,
        IdNumber,
        IssueDate,
        IssueLocation,
        TaxCode,
        Salary,
        SalaryFactor,
        InsuranceSalary,
        NumberDependentPerson,
        Dob,
    }

    public enum EmployeeSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        DisplayName = E._4,
        BusinessGroup = E._5,
        JobTitle = E._6,
        JobLevel = E._7,
        Status = E._8,
        Gender = E._9,
        IdNumber = E._10,
        IssueDate = E._11,
        IssueLocation = E._12,
        TaxCode = E._13,
        Salary = E._14,
        SalaryFactor = E._15,
        InsuranceSalary = E._16,
        NumberDependentPerson = E._17,
        Dob = E._18,
    }
}
