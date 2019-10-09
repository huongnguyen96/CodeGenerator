
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class SupplierContact : DataEntity
    {
        public Guid Id { get; set; }
		public Guid SupplierDetailId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid? ProvinceId { get; set; }
		
    }

    public class SupplierContactFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter SupplierDetailId { get; set; }
		public StringFilter FullName { get; set; }
		public StringFilter Email { get; set; }
		public StringFilter Phone { get; set; }
		public StringFilter Address { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter ProvinceId { get; set; }
		
        public SupplierContactOrder OrderBy {get; set;}
        public SupplierContactSelect Selects {get; set;}
    }

    public enum SupplierContactOrder
    {
        
        FullName,
        Email,
        Phone,
        Address,
        Description,
    }

    public enum SupplierContactSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        SupplierDetail = E._2,
        FullName = E._3,
        Email = E._4,
        Phone = E._5,
        Address = E._6,
        Description = E._7,
        BusinessGroup = E._8,
        Province = E._9,
    }
}
