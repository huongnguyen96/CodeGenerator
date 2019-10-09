
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class UserProfile : DataEntity
    {
        public Guid Id { get; set; }
		public string Key { get; set; }
		public Guid UserId { get; set; }
		public string Value { get; set; }
		
    }

    public class UserProfileFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Key { get; set; }
		public GuidFilter UserId { get; set; }
		public StringFilter Value { get; set; }
		
        public UserProfileOrder OrderBy {get; set;}
        public UserProfileSelect Selects {get; set;}
    }

    public enum UserProfileOrder
    {
        
        Key,
        Value,
    }

    public enum UserProfileSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Key = E._2,
        User = E._3,
        Value = E._4,
    }
}
