
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.ward.ward_detail
{
    public class WardDetail_CustomerDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public WardDetail_CustomerDTO() {}
        public WardDetail_CustomerDTO(Customer Customer)
        {
            
            this.Id = Customer.Id;
            this.Username = Customer.Username;
            this.DisplayName = Customer.DisplayName;
            this.PhoneNumber = Customer.PhoneNumber;
            this.Email = Customer.Email;
        }
    }

    public class WardDetail_CustomerFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public CustomerOrder OrderBy { get; set; }
    }
}
