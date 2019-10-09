using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Employee_HROrganizationDAO
    {
        public Guid HROrganizationId { get; set; }
        public Guid EmployeeId { get; set; }
        public long CX { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual EmployeeDAO Employee { get; set; }
        public virtual HROrganizationDAO HROrganization { get; set; }
    }
}
