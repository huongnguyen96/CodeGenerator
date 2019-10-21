using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DiscountDAO
    {
        public DiscountDAO()
        {
            DiscountCustomerGroupings = new HashSet<DiscountCustomerGroupingDAO>();
            DiscountItems = new HashSet<DiscountItemDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DiscountCustomerGroupingDAO> DiscountCustomerGroupings { get; set; }
        public virtual ICollection<DiscountItemDAO> DiscountItems { get; set; }
    }
}
