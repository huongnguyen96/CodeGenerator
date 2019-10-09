
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class CustomerContact : DataEntity
    {
        public Guid Id { get; set; }
		public Guid CustomerDetailId { get; set; }
		public Guid? ProvinceId { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string FullName { get; set; }
		public string Description { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class CustomerContactFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter CustomerDetailId { get; set; }
		public GuidFilter ProvinceId { get; set; }
		public StringFilter Phone { get; set; }
		public StringFilter Email { get; set; }
		public StringFilter Address { get; set; }
		public StringFilter FullName { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public CustomerContactOrder OrderBy {get; set;}
        public CustomerContactSelect Selects {get; set;}
    }

    public enum CustomerContactOrder
    {
        
        Phone,
        Email,
        Address,
        FullName,
        Description,
    }

    public enum CustomerContactSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        CustomerDetail = E._2,
        Province = E._3,
        Phone = E._4,
        Email = E._5,
        Address = E._6,
        FullName = E._7,
        Description = E._8,
        BusinessGroup = E._9,
    }
}
