using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDetailDAO
    {
        public ItemDetailDAO()
        {
            ItemDetail_ItemGroupings = new HashSet<ItemDetail_ItemGroupingDAO>();
            ItemDiscounts = new HashSet<ItemDiscountDAO>();
            ItemMaterials = new HashSet<ItemMaterialDAO>();
            TransformationUnits = new HashSet<TransformationUnitDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid ItemId { get; set; }
        public Guid LegalEntityId { get; set; }
        public decimal? DefaultValue { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? ReturnAccountId { get; set; }
        public Guid? SalesAllowancesAccountId { get; set; }
        public Guid? ExpenseAccountId { get; set; }
        public Guid? RevenueAccountId { get; set; }
        public Guid? DiscountAccountId { get; set; }
        public bool IsDiscounted { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual ItemDAO Item { get; set; }
        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual ICollection<ItemDetail_ItemGroupingDAO> ItemDetail_ItemGroupings { get; set; }
        public virtual ICollection<ItemDiscountDAO> ItemDiscounts { get; set; }
        public virtual ICollection<ItemMaterialDAO> ItemMaterials { get; set; }
        public virtual ICollection<TransformationUnitDAO> TransformationUnits { get; set; }
    }
}
