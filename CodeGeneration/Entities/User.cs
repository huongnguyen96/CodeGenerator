
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class User : DataEntity
    {
        public Guid Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public Guid? EmployeeId { get; set; }
		public bool IsSysUser { get; set; }
		public bool IsActive { get; set; }
		public string Salt { get; set; }
		
    }

    public class UserFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Username { get; set; }
		public StringFilter Password { get; set; }
		public GuidFilter EmployeeId { get; set; }
		public bool? IsSysUser { get; set; }
		public bool? IsActive { get; set; }
		public StringFilter Salt { get; set; }
		
        public UserOrder OrderBy {get; set;}
        public UserSelect Selects {get; set;}
    }

    public enum UserOrder
    {
        
        Username,
        Password,
        IsSysUser,
        IsActive,
        Salt,
    }

    public enum UserSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Username = E._2,
        Password = E._3,
        Employee = E._4,
        IsSysUser = E._5,
        IsActive = E._6,
        Salt = E._7,
    }
}
