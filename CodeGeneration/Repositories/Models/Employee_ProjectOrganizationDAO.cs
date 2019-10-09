using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Employee_ProjectOrganizationDAO
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectOrganizationId { get; set; }
        public long CX { get; set; }

        public virtual EmployeeDAO Employee { get; set; }
        public virtual ProjectOrganizationDAO ProjectOrganization { get; set; }
    }
}
