
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Supplier : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public List<Item> Items { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }

    public class SupplierFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter ContactPerson { get; set; }
        public StringFilter Address { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public SupplierOrder OrderBy {get; set;}
        public SupplierSelect Selects {get; set;}
    }

    public enum SupplierOrder
    {
        
        Id = 1,
        Name = 2,
        Phone = 3,
        ContactPerson = 4,
        Address = 5,
    }

    public enum SupplierSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Phone = E._3,
        ContactPerson = E._4,
        Address = E._5,
    }
}
