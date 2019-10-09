using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class FeatureOperationDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid FeatureId { get; set; }
        public Guid OperationId { get; set; }

        public virtual FeatureDAO Feature { get; set; }
        public virtual OperationDAO Operation { get; set; }
    }
}
