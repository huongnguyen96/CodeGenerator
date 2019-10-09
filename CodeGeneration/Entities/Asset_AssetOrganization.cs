
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Asset_AssetOrganization : DataEntity
    {
        public Guid AssetOrganizationId { get; set; }
		public Guid AssetId { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid? OwnerId { get; set; }
		public Guid? FromDate { get; set; }
		public Guid? ToDate { get; set; }
		
    }

    public class Asset_AssetOrganizationFilter : FilterEntity
    {
        public GuidFilter AssetOrganizationId { get; set; }
		public GuidFilter AssetId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter OwnerId { get; set; }
		public GuidFilter FromDate { get; set; }
		public GuidFilter ToDate { get; set; }
		
        public Asset_AssetOrganizationOrder OrderBy {get; set;}
        public Asset_AssetOrganizationSelect Selects {get; set;}
    }

    public enum Asset_AssetOrganizationOrder
    {
        
        FromDate,
        ToDate,
    }

    public enum Asset_AssetOrganizationSelect:long
    {
        ALL = E.ALL,
        
        AssetOrganization = E._1,
        Asset = E._2,
        BusinessGroup = E._3,
        Owner = E._4,
        FromDate = E._5,
        ToDate = E._6,
    }
}
