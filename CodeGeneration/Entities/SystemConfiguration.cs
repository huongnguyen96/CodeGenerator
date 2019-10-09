
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class SystemConfiguration : DataEntity
    {
        public string Key { get; set; }
		public string Value { get; set; }
		
    }

    public class SystemConfigurationFilter : FilterEntity
    {
        public StringFilter Key { get; set; }
		public StringFilter Value { get; set; }
		
        public SystemConfigurationOrder OrderBy {get; set;}
        public SystemConfigurationSelect Selects {get; set;}
    }

    public enum SystemConfigurationOrder
    {
        
        Key,
        Value,
    }

    public enum SystemConfigurationSelect:long
    {
        ALL = E.ALL,
        
        Key = E._1,
        Value = E._2,
    }
}
