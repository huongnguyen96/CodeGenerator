using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class BrandDAO
    {
        public BrandDAO()
        {
            Items = new HashSet<ItemDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }

        public virtual CategoryDAO Category { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
    }
}
