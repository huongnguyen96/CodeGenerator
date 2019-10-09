
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class FeaturePermission : DataEntity
    {
        public Guid Id { get; set; }
		public Guid PermissionId { get; set; }
		public Guid FeatureId { get; set; }
		
    }

    public class FeaturePermissionFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter PermissionId { get; set; }
		public GuidFilter FeatureId { get; set; }
		
        public FeaturePermissionOrder OrderBy {get; set;}
        public FeaturePermissionSelect Selects {get; set;}
    }

    public enum FeaturePermissionOrder
    {
        
    }

    public enum FeaturePermissionSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Permission = E._2,
        Feature = E._3,
    }
}
