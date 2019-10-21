using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ImageFileDAO
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
