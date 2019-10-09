using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class FeatureDAO
    {
        public FeatureDAO()
        {
            FeatureOperations = new HashSet<FeatureOperationDAO>();
            FeaturePermissions = new HashSet<FeaturePermissionDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }

        public virtual ICollection<FeatureOperationDAO> FeatureOperations { get; set; }
        public virtual ICollection<FeaturePermissionDAO> FeaturePermissions { get; set; }
    }
}
