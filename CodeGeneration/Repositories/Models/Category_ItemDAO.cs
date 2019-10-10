using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Category_ItemDAO
    {
        public long CategoryId { get; set; }
        public long ItemId { get; set; }

        public virtual CategoryDAO Category { get; set; }
        public virtual ItemDAO Item { get; set; }
    }
}
