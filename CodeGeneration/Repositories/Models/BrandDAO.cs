using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class BrandDAO
    {
        public BrandDAO()
        {
            Products = new HashSet<ProductDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }

        public virtual CategoryDAO Category { get; set; }
        public virtual ICollection<ProductDAO> Products { get; set; }
    }
}
