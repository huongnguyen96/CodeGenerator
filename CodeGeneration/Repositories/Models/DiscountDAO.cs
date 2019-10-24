using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DiscountDAO
    {
        public DiscountDAO()
        {
            DiscountContents = new HashSet<DiscountContentDAO>();
            Discount_CustomerGroupings = new HashSet<Discount_CustomerGroupingDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DiscountContentDAO> DiscountContents { get; set; }
        public virtual ICollection<Discount_CustomerGroupingDAO> Discount_CustomerGroupings { get; set; }
    }
}
