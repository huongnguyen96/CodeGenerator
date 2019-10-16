
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Warehouse : DataEntity
    {
        
        public long Id { get; set; }
        public long ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public User Manager { get; set; }
    }

    public class WarehouseFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ManagerId { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public WarehouseOrder OrderBy {get; set;}
        public WarehouseSelect Selects {get; set;}
    }

    public enum WarehouseOrder
    {
        
        Id = 1,
        Manager = 2,
        Code = 3,
        Name = 4,
    }

    public enum WarehouseSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Manager = E._2,
        Code = E._3,
        Name = E._4,
    }
}
