
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Employee_HROrganization : DataEntity
    {
        public Guid HROrganizationId { get; set; }
		public Guid EmployeeId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class Employee_HROrganizationFilter : FilterEntity
    {
        public GuidFilter HROrganizationId { get; set; }
		public GuidFilter EmployeeId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public Employee_HROrganizationOrder OrderBy {get; set;}
        public Employee_HROrganizationSelect Selects {get; set;}
    }

    public enum Employee_HROrganizationOrder
    {
        
    }

    public enum Employee_HROrganizationSelect:long
    {
        ALL = E.ALL,
        
        HROrganization = E._1,
        Employee = E._2,
        BusinessGroup = E._3,
    }
}
