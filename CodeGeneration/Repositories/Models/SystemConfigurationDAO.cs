using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class SystemConfigurationDAO
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long CX { get; set; }
    }
}
