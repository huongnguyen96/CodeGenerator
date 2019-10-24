
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order_status.order_status_master
{
    public class OrderStatusMaster_OrderStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrderStatusMaster_OrderStatusDTO() {}
        public OrderStatusMaster_OrderStatusDTO(OrderStatus OrderStatus)
        {
            
            this.Id = OrderStatus.Id;
            this.Code = OrderStatus.Code;
            this.Name = OrderStatus.Name;
            this.Description = OrderStatus.Description;
        }
    }

    public class OrderStatusMaster_OrderStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrderStatusOrder OrderBy { get; set; }
    }
}
