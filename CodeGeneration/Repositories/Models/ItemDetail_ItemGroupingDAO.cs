using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDetail_ItemGroupingDAO
    {
        public Guid ItemDetaiId { get; set; }
        public Guid ItemGroupingId { get; set; }
        public long CX { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual ItemDetailDAO ItemDetai { get; set; }
        public virtual ItemGroupingDAO ItemGrouping { get; set; }
    }
}
