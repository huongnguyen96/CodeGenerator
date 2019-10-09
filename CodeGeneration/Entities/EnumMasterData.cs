
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class EnumMasterData : DataEntity
    {
        public Guid Id { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class EnumMasterDataFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Key { get; set; }
		public StringFilter Value { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public EnumMasterDataOrder OrderBy {get; set;}
        public EnumMasterDataSelect Selects {get; set;}
    }

    public enum EnumMasterDataOrder
    {
        
        Key,
        Value,
    }

    public enum EnumMasterDataSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Key = E._2,
        Value = E._3,
        BusinessGroup = E._4,
    }
}
