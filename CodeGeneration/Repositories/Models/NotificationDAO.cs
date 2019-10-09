using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class NotificationDAO
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public long CX { get; set; }
        public bool Unread { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }

        public virtual UserDAO User { get; set; }
    }
}
