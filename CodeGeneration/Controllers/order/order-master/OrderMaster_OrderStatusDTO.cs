
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order.order_master
{
    public class OrderMaster_OrderStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrderMaster_OrderStatusDTO() {}
        public OrderMaster_OrderStatusDTO(OrderStatus OrderStatus)
        {
            
            this.Id = OrderStatus.Id;
            this.Code = OrderStatus.Code;
            this.Name = OrderStatus.Name;
            this.Description = OrderStatus.Description;
        }
    }

    public class OrderMaster_OrderStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrderStatusOrder OrderBy { get; set; }
    }
}
