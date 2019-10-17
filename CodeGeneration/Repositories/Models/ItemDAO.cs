using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDAO
    {
        public ItemDAO()
        {
            Category_Items = new HashSet<Category_ItemDAO>();
            ItemStocks = new HashSet<ItemStockDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public long TypeId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string Description { get; set; }
        public long? StatusId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long SupplierId { get; set; }

        public virtual ItemStatusDAO Status { get; set; }
        public virtual SupplierDAO Supplier { get; set; }
        public virtual ItemTypeDAO Type { get; set; }
        public virtual ItemUnitOfMeasureDAO UnitOfMeasure { get; set; }
        public virtual ICollection<Category_ItemDAO> Category_Items { get; set; }
        public virtual ICollection<ItemStockDAO> ItemStocks { get; set; }
    }
}
