
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class APPSPermission : DataEntity
    {
        public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid? BusinessGroupId { get; set; }
		public Guid? SetOfBookId { get; set; }
		public Guid? LegalEntityId { get; set; }
		public Guid? DivisionId { get; set; }
		
    }

    public class APPSPermissionFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter UserId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public GuidFilter DivisionId { get; set; }
		
        public APPSPermissionOrder OrderBy {get; set;}
        public APPSPermissionSelect Selects {get; set;}
    }

    public enum APPSPermissionOrder
    {
        
    }

    public enum APPSPermissionSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        User = E._2,
        BusinessGroup = E._3,
        SetOfBook = E._4,
        LegalEntity = E._5,
        Division = E._6,
    }
}
