using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class UnitOfMeasureDAO
    {
        public UnitOfMeasureDAO()
        {
            ItemMaterials = new HashSet<ItemMaterialDAO>();
            Items = new HashSet<ItemDAO>();
            TaxTemplateDetails = new HashSet<TaxTemplateDetailDAO>();
            Taxes = new HashSet<TaxDAO>();
            TransformationUnits = new HashSet<TransformationUnitDAO>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid BusinessGroupId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual ICollection<ItemMaterialDAO> ItemMaterials { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
        public virtual ICollection<TaxTemplateDetailDAO> TaxTemplateDetails { get; set; }
        public virtual ICollection<TaxDAO> Taxes { get; set; }
        public virtual ICollection<TransformationUnitDAO> TransformationUnits { get; set; }
    }
}
