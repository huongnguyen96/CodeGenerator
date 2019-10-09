using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class UserProfileDAO
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public Guid UserId { get; set; }
        public long CX { get; set; }
        public string Value { get; set; }

        public virtual UserDAO User { get; set; }
    }
}
