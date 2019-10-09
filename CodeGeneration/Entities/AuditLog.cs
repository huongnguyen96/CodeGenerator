
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class AuditLog : DataEntity
    {
        public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string ClassName { get; set; }
		public string MethodName { get; set; }
		public string OldData { get; set; }
		public string NewData { get; set; }
		public DateTime Time { get; set; }
		
    }

    public class AuditLogFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter UserId { get; set; }
		public StringFilter ClassName { get; set; }
		public StringFilter MethodName { get; set; }
		public StringFilter OldData { get; set; }
		public StringFilter NewData { get; set; }
		public DateTimeFilter Time { get; set; }
		
        public AuditLogOrder OrderBy {get; set;}
        public AuditLogSelect Selects {get; set;}
    }

    public enum AuditLogOrder
    {
        
        ClassName,
        MethodName,
        OldData,
        NewData,
        Time,
    }

    public enum AuditLogSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        User = E._2,
        ClassName = E._3,
        MethodName = E._4,
        OldData = E._5,
        NewData = E._6,
        Time = E._7,
    }
}
