
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Category : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public Category Parent { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> InverseParent { get; set; }
        public List<Product> Products { get; set; }
    }

    public class CategoryFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter ParentId { get; set; }
        public StringFilter Icon { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public CategoryOrder OrderBy {get; set;}
        public CategorySelect Selects {get; set;}
    }

    public enum CategoryOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
        Parent = 4,
        Icon = 5,
    }
    
    [Flags]
    public enum CategorySelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Parent = E._4,
        Icon = E._5,
    }
}
