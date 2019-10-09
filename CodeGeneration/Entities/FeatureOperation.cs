
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class FeatureOperation : DataEntity
    {
        public Guid Id { get; set; }
		public Guid FeatureId { get; set; }
		public Guid OperationId { get; set; }
		
    }

    public class FeatureOperationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter FeatureId { get; set; }
		public GuidFilter OperationId { get; set; }
		
        public FeatureOperationOrder OrderBy {get; set;}
        public FeatureOperationSelect Selects {get; set;}
    }

    public enum FeatureOperationOrder
    {
        
    }

    public enum FeatureOperationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Feature = E._2,
        Operation = E._3,
    }
}
