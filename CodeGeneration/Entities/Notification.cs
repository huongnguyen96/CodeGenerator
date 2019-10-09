
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Notification : DataEntity
    {
        public Guid Id { get; set; }
		public DateTime Time { get; set; }
		public bool Unread { get; set; }
		public Guid UserId { get; set; }
		public string Content { get; set; }
		public string URL { get; set; }
		
    }

    public class NotificationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public DateTimeFilter Time { get; set; }
		public bool? Unread { get; set; }
		public GuidFilter UserId { get; set; }
		public StringFilter Content { get; set; }
		public StringFilter URL { get; set; }
		
        public NotificationOrder OrderBy {get; set;}
        public NotificationSelect Selects {get; set;}
    }

    public enum NotificationOrder
    {
        
        Time,
        Unread,
        Content,
        URL,
    }

    public enum NotificationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Time = E._2,
        Unread = E._3,
        User = E._4,
        Content = E._5,
        URL = E._6,
    }
}
