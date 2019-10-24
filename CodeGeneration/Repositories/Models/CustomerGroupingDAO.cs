using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerGroupingDAO
    {
        public CustomerGroupingDAO()
        {
            Customer_CustomerGroupings = new HashSet<Customer_CustomerGroupingDAO>();
            Discount_CustomerGroupings = new HashSet<Discount_CustomerGroupingDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer_CustomerGroupingDAO> Customer_CustomerGroupings { get; set; }
        public virtual ICollection<Discount_CustomerGroupingDAO> Discount_CustomerGroupings { get; set; }
    }
}
