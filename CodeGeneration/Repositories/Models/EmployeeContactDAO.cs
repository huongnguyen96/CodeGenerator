using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EmployeeContactDAO
    {
        public Guid Id { get; set; }
        public Guid EmployeeDetailId { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual EmployeeDetailDAO EmployeeDetail { get; set; }
    }
}
