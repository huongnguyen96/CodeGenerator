
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class TransformationUnit : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid ItemDetailId { get; set; }
		public Guid BaseUnitId { get; set; }
		public decimal Rate { get; set; }
		public string Description { get; set; }
		public decimal SalePrice { get; set; }
		public decimal PrimaryPrice { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class TransformationUnitFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter ItemDetailId { get; set; }
		public GuidFilter BaseUnitId { get; set; }
		public DecimalFilter Rate { get; set; }
		public StringFilter Description { get; set; }
		public DecimalFilter SalePrice { get; set; }
		public DecimalFilter PrimaryPrice { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public TransformationUnitOrder OrderBy {get; set;}
        public TransformationUnitSelect Selects {get; set;}
    }

    public enum TransformationUnitOrder
    {
        
        Disabled,
        Rate,
        Description,
        SalePrice,
        PrimaryPrice,
    }

    public enum TransformationUnitSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        ItemDetail = E._3,
        BaseUnit = E._4,
        Rate = E._5,
        Description = E._6,
        SalePrice = E._7,
        PrimaryPrice = E._8,
        BusinessGroup = E._9,
    }
}
