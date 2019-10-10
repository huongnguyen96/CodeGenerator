using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class UserDAO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
