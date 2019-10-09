using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class OperationDAO
    {
        public OperationDAO()
        {
            FeatureOperations = new HashSet<FeatureOperationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }

        public virtual ICollection<FeatureOperationDAO> FeatureOperations { get; set; }
    }
}
