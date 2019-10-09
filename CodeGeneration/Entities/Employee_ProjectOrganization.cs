
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Employee_ProjectOrganization : DataEntity
    {
        public Guid EmployeeId { get; set; }
		public Guid ProjectOrganizationId { get; set; }
		
    }

    public class Employee_ProjectOrganizationFilter : FilterEntity
    {
        public GuidFilter EmployeeId { get; set; }
		public GuidFilter ProjectOrganizationId { get; set; }
		
        public Employee_ProjectOrganizationOrder OrderBy {get; set;}
        public Employee_ProjectOrganizationSelect Selects {get; set;}
    }

    public enum Employee_ProjectOrganizationOrder
    {
        
    }

    public enum Employee_ProjectOrganizationSelect:long
    {
        ALL = E.ALL,
        
        Employee = E._1,
        ProjectOrganization = E._2,
    }
}
