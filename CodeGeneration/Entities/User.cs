
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class User : DataEntity
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }

    public class UserFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter Password { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public UserOrder OrderBy {get; set;}
        public UserSelect Selects {get; set;}
    }

    public enum UserOrder
    {
        
        Id = 1,
        Username = 2,
        Password = 3,
    }

    public enum UserSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Username = E._2,
        Password = E._3,
    }
}
