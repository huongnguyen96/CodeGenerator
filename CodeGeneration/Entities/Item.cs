
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Item : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string CodeFromSupplier { get; set; }
		public string CodeFromMarket { get; set; }
		public Guid CharacteristicId { get; set; }
		public Guid UnitOfMeasureId { get; set; }
		public Guid? Weight { get; set; }
		public Guid? Height { get; set; }
		public Guid? Length { get; set; }
		public Guid? Width { get; set; }
		public Guid StatusId { get; set; }
		public string Description { get; set; }
		public double PrimaryPrice { get; set; }
		public Guid? VATId { get; set; }
		public Guid? ImportTaxId { get; set; }
		public Guid? ExportTaxId { get; set; }
		public Guid? NaturalResourceTaxId { get; set; }
		public Guid? EnvironmentTaxId { get; set; }
		public Guid? SpecialConsumptionTaxId { get; set; }
		
    }

    public class ItemFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter CodeFromSupplier { get; set; }
		public StringFilter CodeFromMarket { get; set; }
		public GuidFilter CharacteristicId { get; set; }
		public GuidFilter UnitOfMeasureId { get; set; }
		public GuidFilter Weight { get; set; }
		public GuidFilter Height { get; set; }
		public GuidFilter Length { get; set; }
		public GuidFilter Width { get; set; }
		public GuidFilter StatusId { get; set; }
		public StringFilter Description { get; set; }
		public DoubleFilter PrimaryPrice { get; set; }
		public GuidFilter VATId { get; set; }
		public GuidFilter ImportTaxId { get; set; }
		public GuidFilter ExportTaxId { get; set; }
		public GuidFilter NaturalResourceTaxId { get; set; }
		public GuidFilter EnvironmentTaxId { get; set; }
		public GuidFilter SpecialConsumptionTaxId { get; set; }
		
        public ItemOrder OrderBy {get; set;}
        public ItemSelect Selects {get; set;}
    }

    public enum ItemOrder
    {
        
        Disabled,
        Code,
        Name,
        CodeFromSupplier,
        CodeFromMarket,
        Weight,
        Height,
        Length,
        Width,
        Description,
        PrimaryPrice,
    }

    public enum ItemSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        BusinessGroup = E._5,
        CodeFromSupplier = E._6,
        CodeFromMarket = E._7,
        Characteristic = E._8,
        UnitOfMeasure = E._9,
        Weight = E._10,
        Height = E._11,
        Length = E._12,
        Width = E._13,
        Status = E._14,
        Description = E._15,
        PrimaryPrice = E._16,
        VAT = E._17,
        ImportTax = E._18,
        ExportTax = E._19,
        NaturalResourceTax = E._20,
        EnvironmentTax = E._21,
        SpecialConsumptionTax = E._22,
    }
}
