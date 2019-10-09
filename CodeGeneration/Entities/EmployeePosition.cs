
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class EmployeePosition : DataEntity
    {
        public Guid Id { get; set; }
		public Guid EmployeeDetailId { get; set; }
		public Guid PositionId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class EmployeePositionFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter EmployeeDetailId { get; set; }
		public GuidFilter PositionId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public EmployeePositionOrder OrderBy {get; set;}
        public EmployeePositionSelect Selects {get; set;}
    }

    public enum EmployeePositionOrder
    {
        
    }

    public enum EmployeePositionSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        EmployeeDetail = E._2,
        Position = E._3,
        BusinessGroup = E._4,
    }
}
