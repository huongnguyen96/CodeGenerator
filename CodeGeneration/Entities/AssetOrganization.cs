
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class AssetOrganization : DataEntity
    {
        public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid DivisionId { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class AssetOrganizationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter DivisionId { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public AssetOrganizationOrder OrderBy {get; set;}
        public AssetOrganizationSelect Selects {get; set;}
    }

    public enum AssetOrganizationOrder
    {
        
        Code,
        Name,
        Disabled,
    }

    public enum AssetOrganizationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Division = E._4,
        Disabled = E._5,
        BusinessGroup = E._6,
    }
}
