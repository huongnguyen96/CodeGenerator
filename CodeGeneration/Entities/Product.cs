
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Product : DataEntity
    {
        
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
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public Merchant Merchant { get; set; }
        public ProductStatus Status { get; set; }
        public ProductType Type { get; set; }
        public List<EVoucher> EVouchers { get; set; }
        public List<Item> Items { get; set; }
        public List<Product_MerchantAddress> Product_MerchantAddresses { get; set; }
        public List<Product_PaymentMethod> Product_PaymentMethods { get; set; }
        public List<VariationGrouping> VariationGroupings { get; set; }
    }

    public class ProductFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public LongFilter TypeId { get; set; }
        public LongFilter StatusId { get; set; }
        public LongFilter MerchantId { get; set; }
        public LongFilter CategoryId { get; set; }
        public LongFilter BrandId { get; set; }
        public StringFilter WarrantyPolicy { get; set; }
        public StringFilter ReturnPolicy { get; set; }
        public StringFilter ExpiredDate { get; set; }
        public StringFilter ConditionOfUse { get; set; }
        public LongFilter MaximumPurchaseQuantity { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ProductOrder OrderBy {get; set;}
        public ProductSelect Selects {get; set;}
    }

    public enum ProductOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
        Description = 4,
        Type = 5,
        Status = 6,
        Merchant = 7,
        Category = 8,
        Brand = 9,
        WarrantyPolicy = 10,
        ReturnPolicy = 11,
        ExpiredDate = 12,
        ConditionOfUse = 13,
        MaximumPurchaseQuantity = 14,
    }
    
    [Flags]
    public enum ProductSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Description = E._4,
        Type = E._5,
        Status = E._6,
        Merchant = E._7,
        Category = E._8,
        Brand = E._9,
        WarrantyPolicy = E._10,
        ReturnPolicy = E._11,
        ExpiredDate = E._12,
        ConditionOfUse = E._13,
        MaximumPurchaseQuantity = E._14,
    }
}
