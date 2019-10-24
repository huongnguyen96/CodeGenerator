
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_master
{
    public class ItemMaster_OrderContentDTO : DataDTO
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
        public ItemMaster_OrderDTO Order { get; set; }
        public ItemMaster_OrderContentDTO() {}
        public ItemMaster_OrderContentDTO(OrderContent OrderContent)
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
            this.Order = new ItemMaster_OrderDTO(OrderContent.Order);

        }
    }

    public class ItemMaster_OrderContentFilterDTO : FilterDTO
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
