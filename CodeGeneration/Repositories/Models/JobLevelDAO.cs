using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class JobLevelDAO
    {
        public JobLevelDAO()
        {
            Employees = new HashSet<EmployeeDAO>();
        }

        public Guid Id { get; set; }
        public Guid BusinessGroupId { get; set; }
        public double Level { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Description { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
    }
}
