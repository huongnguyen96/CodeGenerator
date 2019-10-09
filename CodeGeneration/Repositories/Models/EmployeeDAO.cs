using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EmployeeDAO
    {
        public EmployeeDAO()
        {
            CustomerDetails = new HashSet<CustomerDetailDAO>();
            EmployeeDetails = new HashSet<EmployeeDetailDAO>();
            Employee_HROrganizations = new HashSet<Employee_HROrganizationDAO>();
            Employee_InventoryOrganizationAddresses = new HashSet<Employee_InventoryOrganizationAddressDAO>();
            Employee_ProjectOrganizations = new HashSet<Employee_ProjectOrganizationDAO>();
            ProjectOrganizations = new HashSet<ProjectOrganizationDAO>();
            SupplierDetails = new HashSet<SupplierDetailDAO>();
            Users = new HashSet<UserDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid? JobTitleId { get; set; }
        public Guid? JobLevelId { get; set; }
        public Guid StatusId { get; set; }
        public bool Gender { get; set; }
        public string IdNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueLocation { get; set; }
        public string TaxCode { get; set; }
        public long? Salary { get; set; }
        public double? SalaryFactor { get; set; }
        public long? InsuranceSalary { get; set; }
        public int? NumberDependentPerson { get; set; }
        public DateTime Dob { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual JobLevelDAO JobLevel { get; set; }
        public virtual JobTitleDAO JobTitle { get; set; }
        public virtual EnumMasterDataDAO Status { get; set; }
        public virtual ICollection<CustomerDetailDAO> CustomerDetails { get; set; }
        public virtual ICollection<EmployeeDetailDAO> EmployeeDetails { get; set; }
        public virtual ICollection<Employee_HROrganizationDAO> Employee_HROrganizations { get; set; }
        public virtual ICollection<Employee_InventoryOrganizationAddressDAO> Employee_InventoryOrganizationAddresses { get; set; }
        public virtual ICollection<Employee_ProjectOrganizationDAO> Employee_ProjectOrganizations { get; set; }
        public virtual ICollection<ProjectOrganizationDAO> ProjectOrganizations { get; set; }
        public virtual ICollection<SupplierDetailDAO> SupplierDetails { get; set; }
        public virtual ICollection<UserDAO> Users { get; set; }
    }
}
