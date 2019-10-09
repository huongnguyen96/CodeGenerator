
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Province : DataEntity
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class ProvinceFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public ProvinceOrder OrderBy {get; set;}
        public ProvinceSelect Selects {get; set;}
    }

    public enum ProvinceOrder
    {
        
        Name,
    }

    public enum ProvinceSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        BusinessGroup = E._3,
    }
}
