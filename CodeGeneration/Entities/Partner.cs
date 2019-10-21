
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Partner : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public List<Item> Items { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }

    public class PartnerFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter ContactPerson { get; set; }
        public StringFilter Address { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public PartnerOrder OrderBy {get; set;}
        public PartnerSelect Selects {get; set;}
    }

    public enum PartnerOrder
    {
        
        Id = 1,
        Name = 2,
        Phone = 3,
        ContactPerson = 4,
        Address = 5,
    }

    public enum PartnerSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Phone = E._3,
        ContactPerson = E._4,
        Address = E._5,
    }
}
