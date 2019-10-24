using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class VariationDAO
    {
        public VariationDAO()
        {
            ItemFirstVariations = new HashSet<ItemDAO>();
            ItemSecondVariations = new HashSet<ItemDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }

        public virtual VariationGroupingDAO VariationGrouping { get; set; }
        public virtual ICollection<ItemDAO> ItemFirstVariations { get; set; }
        public virtual ICollection<ItemDAO> ItemSecondVariations { get; set; }
    }
}
