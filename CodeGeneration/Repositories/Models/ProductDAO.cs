using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ProductDAO
    {
        public ProductDAO()
        {
            EVouchers = new HashSet<EVoucherDAO>();
            Items = new HashSet<ItemDAO>();
            Product_MerchantAddresses = new HashSet<Product_MerchantAddressDAO>();
            Product_PaymentMethods = new HashSet<Product_PaymentMethodDAO>();
            VariationGroupings = new HashSet<VariationGroupingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long TypeId { get; set; }
        public long StatusId { get; set; }
        public long MerchantId { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string WarrantyPolicy { get; set; }
        public string ReturnPolicy { get; set; }
        public string ExpiredDate { get; set; }
        public string ConditionOfUse { get; set; }
        public long? MaximumPurchaseQuantity { get; set; }

        public virtual BrandDAO Brand { get; set; }
        public virtual CategoryDAO Category { get; set; }
        public virtual MerchantDAO Merchant { get; set; }
        public virtual ProductStatusDAO Status { get; set; }
        public virtual ProductTypeDAO Type { get; set; }
        public virtual ICollection<EVoucherDAO> EVouchers { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
        public virtual ICollection<Product_MerchantAddressDAO> Product_MerchantAddresses { get; set; }
        public virtual ICollection<Product_PaymentMethodDAO> Product_PaymentMethods { get; set; }
        public virtual ICollection<VariationGroupingDAO> VariationGroupings { get; set; }
    }
}
