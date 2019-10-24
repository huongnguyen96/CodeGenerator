
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Administrator : DataEntity
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }

    public class AdministratorFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter DisplayName { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public AdministratorOrder OrderBy {get; set;}
        public AdministratorSelect Selects {get; set;}
    }

    public enum AdministratorOrder
    {
        
        Id = 1,
        Username = 2,
        DisplayName = 3,
    }
    
    [Flags]
    public enum AdministratorSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Username = E._2,
        DisplayName = E._3,
    }
}
