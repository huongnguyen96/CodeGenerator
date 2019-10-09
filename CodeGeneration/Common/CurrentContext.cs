using System;
using System.Collections.Generic;

namespace Common
{
    public interface ICurrentContext : IServiceScoped
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
        bool IsSuperAdmin { get; set; }
        int TimeZone { get; set; }
        string Language { get; set; }
        string RootPath { get; set; }

    }
    public class CurrentContext : ICurrentContext
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int TimeZone { get; set; }
        public string Language { get; set; }
        public string RootPath { get; set; }
    }
}
