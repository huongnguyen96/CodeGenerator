﻿using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerDAO
    {
        public CustomerDAO()
        {
            Customer_CustomerGroupings = new HashSet<Customer_CustomerGroupingDAO>();
            EVouchers = new HashSet<EVoucherDAO>();
            Orders = new HashSet<OrderDAO>();
            ShippingAddresses = new HashSet<ShippingAddressDAO>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Customer_CustomerGroupingDAO> Customer_CustomerGroupings { get; set; }
        public virtual ICollection<EVoucherDAO> EVouchers { get; set; }
        public virtual ICollection<OrderDAO> Orders { get; set; }
        public virtual ICollection<ShippingAddressDAO> ShippingAddresses { get; set; }
    }
}
