
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer.customer_master
{
    public class CustomerMaster_OrderStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CustomerMaster_OrderStatusDTO() {}
        public CustomerMaster_OrderStatusDTO(OrderStatus OrderStatus)
        {
            
            this.Id = OrderStatus.Id;
            this.Code = OrderStatus.Code;
            this.Name = OrderStatus.Name;
            this.Description = OrderStatus.Description;
        }
    }

    public class CustomerMaster_OrderStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrderStatusOrder OrderBy { get; set; }
    }
}
