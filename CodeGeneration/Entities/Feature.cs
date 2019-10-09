
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Feature : DataEntity
    {
        public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public bool Disabled { get; set; }
		
    }

    public class FeatureFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public bool? Disabled { get; set; }
		
        public FeatureOrder OrderBy {get; set;}
        public FeatureSelect Selects {get; set;}
    }

    public enum FeatureOrder
    {
        
        Code,
        Name,
        Disabled,
    }

    public enum FeatureSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Disabled = E._4,
    }
}
