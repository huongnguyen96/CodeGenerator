using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EmployeePositionDAO
    {
        public Guid Id { get; set; }
        public Guid EmployeeDetailId { get; set; }
        public Guid PositionId { get; set; }
        public long CX { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual EmployeeDetailDAO EmployeeDetail { get; set; }
        public virtual PositionDAO Position { get; set; }
    }
}
