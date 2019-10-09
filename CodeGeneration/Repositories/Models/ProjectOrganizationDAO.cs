using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ProjectOrganizationDAO
    {
        public ProjectOrganizationDAO()
        {
            Employee_ProjectOrganizations = new HashSet<Employee_ProjectOrganizationDAO>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DivisionId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid ManagerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual DivisionDAO Division { get; set; }
        public virtual EmployeeDAO Manager { get; set; }
        public virtual ICollection<Employee_ProjectOrganizationDAO> Employee_ProjectOrganizations { get; set; }
    }
}
