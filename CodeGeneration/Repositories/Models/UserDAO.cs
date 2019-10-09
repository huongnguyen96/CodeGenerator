using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class UserDAO
    {
        public UserDAO()
        {
            APPSPermissions = new HashSet<APPSPermissionDAO>();
            AuditLogs = new HashSet<AuditLogDAO>();
            Notifications = new HashSet<NotificationDAO>();
            UserProfiles = new HashSet<UserProfileDAO>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid? EmployeeId { get; set; }
        public long CX { get; set; }
        public bool IsSysUser { get; set; }
        public bool IsActive { get; set; }
        public string Salt { get; set; }

        public virtual EmployeeDAO Employee { get; set; }
        public virtual ICollection<APPSPermissionDAO> APPSPermissions { get; set; }
        public virtual ICollection<AuditLogDAO> AuditLogs { get; set; }
        public virtual ICollection<NotificationDAO> Notifications { get; set; }
        public virtual ICollection<UserProfileDAO> UserProfiles { get; set; }
    }
}
