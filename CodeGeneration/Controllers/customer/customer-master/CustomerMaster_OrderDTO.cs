
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer.customer_master
{
    public class CustomerMaster_OrderDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long Total { get; set; }
        public long VoucherDiscount { get; set; }
        public long CampaignDiscount { get; set; }
        public CustomerMaster_OrderDTO() {}
        public CustomerMaster_OrderDTO(Order Order)
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

    public class CustomerMaster_OrderFilterDTO : FilterDTO
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
