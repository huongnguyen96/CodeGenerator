using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDAO
    {
        public ItemDAO()
        {
            VariationGroupings = new HashSet<VariationGroupingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public long TypeId { get; set; }
        public long StatusId { get; set; }
        public long PartnerId { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }

        public virtual BrandDAO Brand { get; set; }
        public virtual CategoryDAO Category { get; set; }
        public virtual PartnerDAO Partner { get; set; }
        public virtual ItemStatusDAO Status { get; set; }
        public virtual ItemTypeDAO Type { get; set; }
        public virtual ICollection<VariationGroupingDAO> VariationGroupings { get; set; }
    }
}
