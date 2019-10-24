
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_type.product_type_detail
{
    public class ProductTypeDetail_ProductDTO : DataDTO
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
        public ProductTypeDetail_BrandDTO Brand { get; set; }
        public ProductTypeDetail_CategoryDTO Category { get; set; }
        public ProductTypeDetail_MerchantDTO Merchant { get; set; }
        public ProductTypeDetail_ProductStatusDTO Status { get; set; }
        public ProductTypeDetail_ProductDTO() {}
        public ProductTypeDetail_ProductDTO(Product Product)
        {
            
            this.Id = Product.Id;
            this.Code = Product.Code;
            this.Name = Product.Name;
            this.Description = Product.Description;
            this.TypeId = Product.TypeId;
            this.StatusId = Product.StatusId;
            this.MerchantId = Product.MerchantId;
            this.CategoryId = Product.CategoryId;
            this.BrandId = Product.BrandId;
            this.WarrantyPolicy = Product.WarrantyPolicy;
            this.ReturnPolicy = Product.ReturnPolicy;
            this.ExpiredDate = Product.ExpiredDate;
            this.ConditionOfUse = Product.ConditionOfUse;
            this.MaximumPurchaseQuantity = Product.MaximumPurchaseQuantity;
            this.Brand = new ProductTypeDetail_BrandDTO(Product.Brand);

            this.Category = new ProductTypeDetail_CategoryDTO(Product.Category);

            this.Merchant = new ProductTypeDetail_MerchantDTO(Product.Merchant);

            this.Status = new ProductTypeDetail_ProductStatusDTO(Product.Status);

        }
    }

    public class ProductTypeDetail_ProductFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? TypeId { get; set; }
        public long? StatusId { get; set; }
        public long? MerchantId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string WarrantyPolicy { get; set; }
        public string ReturnPolicy { get; set; }
        public string ExpiredDate { get; set; }
        public string ConditionOfUse { get; set; }
        public long? MaximumPurchaseQuantity { get; set; }
        public ProductOrder OrderBy { get; set; }
    }
}
