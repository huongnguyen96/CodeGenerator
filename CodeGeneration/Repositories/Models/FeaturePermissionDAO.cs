using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class FeaturePermissionDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid PermissionId { get; set; }
        public Guid FeatureId { get; set; }

        public virtual FeatureDAO Feature { get; set; }
    }
}
