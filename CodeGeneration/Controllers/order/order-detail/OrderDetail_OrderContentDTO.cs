
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order.order_detail
{
    public class OrderDetail_OrderContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string ItemName { get; set; }
        public string FirstVersion { get; set; }
        public string SecondVersion { get; set; }
        public string ThirdVersion { get; set; }
        public long Price { get; set; }
        public long DiscountPrice { get; set; }
        public OrderDetail_OrderContentDTO() {}
        public OrderDetail_OrderContentDTO(OrderContent OrderContent)
        {
            
            this.Id = OrderContent.Id;
            this.OrderId = OrderContent.OrderId;
            this.ItemName = OrderContent.ItemName;
            this.FirstVersion = OrderContent.FirstVersion;
            this.SecondVersion = OrderContent.SecondVersion;
            this.ThirdVersion = OrderContent.ThirdVersion;
            this.Price = OrderContent.Price;
            this.DiscountPrice = OrderContent.DiscountPrice;
        }
    }

    public class OrderDetail_OrderContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public string ItemName { get; set; }
        public string FirstVersion { get; set; }
        public string SecondVersion { get; set; }
        public string ThirdVersion { get; set; }
        public long? Price { get; set; }
        public long? DiscountPrice { get; set; }
    }
}
