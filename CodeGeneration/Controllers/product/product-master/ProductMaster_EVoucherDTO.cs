
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product.product_master
{
    public class ProductMaster_EVoucherDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long Quantity { get; set; }
        public ProductMaster_CustomerDTO Customer { get; set; }
        public ProductMaster_EVoucherDTO() {}
        public ProductMaster_EVoucherDTO(EVoucher EVoucher)
        {
            
            this.Id = EVoucher.Id;
            this.CustomerId = EVoucher.CustomerId;
            this.ProductId = EVoucher.ProductId;
            this.Name = EVoucher.Name;
            this.Start = EVoucher.Start;
            this.End = EVoucher.End;
            this.Quantity = EVoucher.Quantity;
            this.Customer = new ProductMaster_CustomerDTO(EVoucher.Customer);

        }
    }

    public class ProductMaster_EVoucherFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public long? Quantity { get; set; }
        public EVoucherOrder OrderBy { get; set; }
    }
}
