
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Employee_InventoryOrganizationAddress : DataEntity
    {
        public Guid InventoryOrganizationAddressId { get; set; }
		public Guid EmployeeId { get; set; }
		
    }

    public class Employee_InventoryOrganizationAddressFilter : FilterEntity
    {
        public GuidFilter InventoryOrganizationAddressId { get; set; }
		public GuidFilter EmployeeId { get; set; }
		
        public Employee_InventoryOrganizationAddressOrder OrderBy {get; set;}
        public Employee_InventoryOrganizationAddressSelect Selects {get; set;}
    }

    public enum Employee_InventoryOrganizationAddressOrder
    {
        
    }

    public enum Employee_InventoryOrganizationAddressSelect:long
    {
        ALL = E.ALL,
        
        InventoryOrganizationAddress = E._1,
        Employee = E._2,
    }
}
