using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemGroupingDAO
    {
        public ItemGroupingDAO()
        {
            ItemDetail_ItemGroupings = new HashSet<ItemDetail_ItemGroupingDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid LegalEntityId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual ICollection<ItemDetail_ItemGroupingDAO> ItemDetail_ItemGroupings { get; set; }
    }
}
