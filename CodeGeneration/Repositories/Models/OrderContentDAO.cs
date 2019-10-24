using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class OrderContentDAO
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

        public virtual ItemDAO Item { get; set; }
        public virtual OrderDAO Order { get; set; }
    }
}
