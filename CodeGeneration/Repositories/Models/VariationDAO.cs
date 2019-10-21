using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class VariationDAO
    {
        public VariationDAO()
        {
            UnitFirstVariations = new HashSet<UnitDAO>();
            UnitSecondVariations = new HashSet<UnitDAO>();
            UnitThirdVariations = new HashSet<UnitDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }

        public virtual VariationGroupingDAO VariationGrouping { get; set; }
        public virtual ICollection<UnitDAO> UnitFirstVariations { get; set; }
        public virtual ICollection<UnitDAO> UnitSecondVariations { get; set; }
        public virtual ICollection<UnitDAO> UnitThirdVariations { get; set; }
    }
}
