using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class OrderStatusDAO
    {
        public OrderStatusDAO()
        {
            Orders = new HashSet<OrderDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderDAO> Orders { get; set; }
    }
}
