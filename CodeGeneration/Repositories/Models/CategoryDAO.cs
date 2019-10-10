using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CategoryDAO
    {
        public CategoryDAO()
        {
            Category_Items = new HashSet<Category_ItemDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Category_ItemDAO> Category_Items { get; set; }
    }
}
