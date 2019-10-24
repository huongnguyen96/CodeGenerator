
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order_content.order_content_detail
{
    public class OrderContentDetail_OrderContentDTO : DataDTO
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
        public OrderContentDetail_ItemDTO Item { get; set; }
        public OrderContentDetail_OrderDTO Order { get; set; }
        public OrderContentDetail_OrderContentDTO() {}
        public OrderContentDetail_OrderContentDTO(OrderContent OrderContent)
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
            this.Item = new OrderContentDetail_ItemDTO(OrderContent.Item);

            this.Order = new OrderContentDetail_OrderDTO(OrderContent.Order);

        }
    }

    public class OrderContentDetail_OrderContentFilterDTO : FilterDTO
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
