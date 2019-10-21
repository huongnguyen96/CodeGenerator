
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order_content.order_content_detail
{
    public class OrderContentDetail_OrderDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long Total { get; set; }
        public long VoucherDiscount { get; set; }
        public long CampaignDiscount { get; set; }
        public OrderContentDetail_OrderDTO() {}
        public OrderContentDetail_OrderDTO(Order Order)
        {
            
            this.Id = Order.Id;
            this.CustomerId = Order.CustomerId;
            this.CreatedDate = Order.CreatedDate;
            this.VoucherCode = Order.VoucherCode;
            this.Total = Order.Total;
            this.VoucherDiscount = Order.VoucherDiscount;
            this.CampaignDiscount = Order.CampaignDiscount;
        }
    }

    public class OrderContentDetail_OrderFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long? Total { get; set; }
        public long? VoucherDiscount { get; set; }
        public long? CampaignDiscount { get; set; }
    }
}
