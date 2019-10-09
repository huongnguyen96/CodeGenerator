
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class HROrganization : DataEntity
    {
        public Guid Id { get; set; }
		public string Code { get; set; }
		public string ShortName { get; set; }
		public string Name { get; set; }
		public Guid DivisionId { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class HROrganizationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter ShortName { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter DivisionId { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public HROrganizationOrder OrderBy {get; set;}
        public HROrganizationSelect Selects {get; set;}
    }

    public enum HROrganizationOrder
    {
        
        Code,
        ShortName,
        Name,
        Disabled,
    }

    public enum HROrganizationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        ShortName = E._3,
        Name = E._4,
        Division = E._5,
        Disabled = E._6,
        BusinessGroup = E._7,
    }
}
