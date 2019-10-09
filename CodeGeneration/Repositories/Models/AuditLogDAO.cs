using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class AuditLogDAO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public DateTime Time { get; set; }
        public long CX { get; set; }

        public virtual UserDAO User { get; set; }
    }
}
