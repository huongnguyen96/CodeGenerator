using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class VariationGroupingDAO
    {
        public VariationGroupingDAO()
        {
            Variations = new HashSet<VariationDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }

        public virtual ProductDAO Product { get; set; }
        public virtual ICollection<VariationDAO> Variations { get; set; }
    }
}
