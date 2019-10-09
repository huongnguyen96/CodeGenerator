
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class InventoryOrganizationAddress : DataEntity
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public Guid InventoryOrganizationId { get; set; }
		
    }

    public class InventoryOrganizationAddressFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Address { get; set; }
		public GuidFilter InventoryOrganizationId { get; set; }
		
        public InventoryOrganizationAddressOrder OrderBy {get; set;}
        public InventoryOrganizationAddressSelect Selects {get; set;}
    }

    public enum InventoryOrganizationAddressOrder
    {
        
        Name,
        Address,
    }

    public enum InventoryOrganizationAddressSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Address = E._3,
        InventoryOrganization = E._4,
    }
}
