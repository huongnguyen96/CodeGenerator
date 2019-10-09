using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDAO
    {
        public ItemDAO()
        {
            ItemDetails = new HashSet<ItemDetailDAO>();
            ItemMaterials = new HashSet<ItemMaterialDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid BusinessGroupId { get; set; }
        public string CodeFromSupplier { get; set; }
        public string CodeFromMarket { get; set; }
        public Guid CharacteristicId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public Guid StatusId { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public double PrimaryPrice { get; set; }
        public Guid? VATId { get; set; }
        public Guid? ImportTaxId { get; set; }
        public Guid? ExportTaxId { get; set; }
        public Guid? NaturalResourceTaxId { get; set; }
        public Guid? EnvironmentTaxId { get; set; }
        public Guid? SpecialConsumptionTaxId { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual EnumMasterDataDAO Characteristic { get; set; }
        public virtual EnumMasterDataDAO Status { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
        public virtual ICollection<ItemDetailDAO> ItemDetails { get; set; }
        public virtual ICollection<ItemMaterialDAO> ItemMaterials { get; set; }
    }
}
