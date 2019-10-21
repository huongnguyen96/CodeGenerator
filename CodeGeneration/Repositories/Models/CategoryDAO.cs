using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CategoryDAO
    {
        public CategoryDAO()
        {
            Brands = new HashSet<BrandDAO>();
            InverseParent = new HashSet<CategoryDAO>();
            Items = new HashSet<ItemDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }

        public virtual CategoryDAO Parent { get; set; }
        public virtual ICollection<BrandDAO> Brands { get; set; }
        public virtual ICollection<CategoryDAO> InverseParent { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
    }
}
