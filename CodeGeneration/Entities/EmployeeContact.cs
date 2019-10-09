
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class EmployeeContact : DataEntity
    {
        public Guid Id { get; set; }
		public Guid EmployeeDetailId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class EmployeeContactFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter EmployeeDetailId { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Email { get; set; }
		public StringFilter Phone { get; set; }
		public StringFilter Address { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public EmployeeContactOrder OrderBy {get; set;}
        public EmployeeContactSelect Selects {get; set;}
    }

    public enum EmployeeContactOrder
    {
        
        Name,
        Email,
        Phone,
        Address,
        Description,
    }

    public enum EmployeeContactSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        EmployeeDetail = E._2,
        Name = E._3,
        Email = E._4,
        Phone = E._5,
        Address = E._6,
        Description = E._7,
        BusinessGroup = E._8,
    }
}
