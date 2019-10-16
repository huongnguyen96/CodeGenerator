using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDAO
    {
        public ItemDAO()
        {
            Category_Items = new HashSet<Category_ItemDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Category_ItemDAO> Category_Items { get; set; }
    }
}
