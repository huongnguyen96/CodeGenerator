
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.order_status.order_status_detail
{
    public class OrderStatusDetail_CustomerDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public OrderStatusDetail_CustomerDTO() {}
        public OrderStatusDetail_CustomerDTO(Customer Customer)
        {
            
            this.Id = Customer.Id;
            this.Username = Customer.Username;
            this.DisplayName = Customer.DisplayName;
            this.PhoneNumber = Customer.PhoneNumber;
            this.Email = Customer.Email;
        }
    }

    public class OrderStatusDetail_CustomerFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public CustomerOrder OrderBy { get; set; }
    }
}
