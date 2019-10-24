
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order_status.order_status_master
{
    public class OrderStatusMaster_OrderDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long Total { get; set; }
        public long VoucherDiscount { get; set; }
        public long CampaignDiscount { get; set; }
        public long StatusId { get; set; }
        public OrderStatusMaster_CustomerDTO Customer { get; set; }
        public OrderStatusMaster_OrderDTO() {}
        public OrderStatusMaster_OrderDTO(Order Order)
        {
            
            this.Id = Order.Id;
            this.CustomerId = Order.CustomerId;
            this.CreatedDate = Order.CreatedDate;
            this.VoucherCode = Order.VoucherCode;
            this.Total = Order.Total;
            this.VoucherDiscount = Order.VoucherDiscount;
            this.CampaignDiscount = Order.CampaignDiscount;
            this.StatusId = Order.StatusId;
            this.Customer = new OrderStatusMaster_CustomerDTO(Order.Customer);

        }
    }

    public class OrderStatusMaster_OrderFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long? Total { get; set; }
        public long? VoucherDiscount { get; set; }
        public long? CampaignDiscount { get; set; }
        public long? StatusId { get; set; }
        public OrderOrder OrderBy { get; set; }
    }
}
