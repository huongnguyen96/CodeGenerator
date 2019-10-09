
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Operation : DataEntity
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public bool Disabled { get; set; }
		
    }

    public class OperationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Name { get; set; }
		public bool? Disabled { get; set; }
		
        public OperationOrder OrderBy {get; set;}
        public OperationSelect Selects {get; set;}
    }

    public enum OperationOrder
    {
        
        Name,
        Disabled,
    }

    public enum OperationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Disabled = E._3,
    }
}
