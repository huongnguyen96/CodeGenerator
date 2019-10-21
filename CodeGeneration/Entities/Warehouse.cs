
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Warehouse : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long PartnerId { get; set; }
        public Partner Partner { get; set; }
        public List<Stock> Stocks { get; set; }
    }

    public class WarehouseFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Address { get; set; }
        public LongFilter PartnerId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public WarehouseOrder OrderBy {get; set;}
        public WarehouseSelect Selects {get; set;}
    }

    public enum WarehouseOrder
    {
        
        Id = 1,
        Name = 2,
        Phone = 3,
        Email = 4,
        Address = 5,
        Partner = 6,
    }

    public enum WarehouseSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Phone = E._3,
        Email = E._4,
        Address = E._5,
        Partner = E._6,
    }
}
