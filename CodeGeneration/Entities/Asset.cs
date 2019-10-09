
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Asset : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid TypeId { get; set; }
		public Guid StatusId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class AssetFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter TypeId { get; set; }
		public GuidFilter StatusId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public AssetOrder OrderBy {get; set;}
        public AssetSelect Selects {get; set;}
    }

    public enum AssetOrder
    {
        
        Disabled,
        Code,
        Name,
    }

    public enum AssetSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        Type = E._5,
        Status = E._6,
        BusinessGroup = E._7,
    }
}
