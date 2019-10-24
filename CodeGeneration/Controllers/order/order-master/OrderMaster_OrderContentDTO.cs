
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order.order_master
{
    public class OrderMaster_OrderContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long? ItemId { get; set; }
        public string ProductName { get; set; }
        public string FirstVersion { get; set; }
        public string SecondVersion { get; set; }
        public long Price { get; set; }
        public long DiscountPrice { get; set; }
        public long Quantity { get; set; }
        public OrderMaster_ItemDTO Item { get; set; }
        public OrderMaster_OrderContentDTO() {}
        public OrderMaster_OrderContentDTO(OrderContent OrderContent)
        {
            
            this.Id = OrderContent.Id;
            this.OrderId = OrderContent.OrderId;
            this.ItemId = OrderContent.ItemId;
            this.ProductName = OrderContent.ProductName;
            this.FirstVersion = OrderContent.FirstVersion;
            this.SecondVersion = OrderContent.SecondVersion;
            this.Price = OrderContent.Price;
            this.DiscountPrice = OrderContent.DiscountPrice;
            this.Quantity = OrderContent.Quantity;
            this.Item = new OrderMaster_ItemDTO(OrderContent.Item);

        }
    }

    public class OrderMaster_OrderContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public long? ItemId { get; set; }
        public string ProductName { get; set; }
        public string FirstVersion { get; set; }
        public string SecondVersion { get; set; }
        public long? Price { get; set; }
        public long? DiscountPrice { get; set; }
        public long? Quantity { get; set; }
        public OrderContentOrder OrderBy { get; set; }
    }
}
