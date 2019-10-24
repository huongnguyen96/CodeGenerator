
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ImageFile : DataEntity
    {
        
        public long Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }

    public class ImageFileFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Path { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ImageFileOrder OrderBy {get; set;}
        public ImageFileSelect Selects {get; set;}
    }

    public enum ImageFileOrder
    {
        
        Id = 1,
        Path = 2,
        Name = 3,
    }
    
    [Flags]
    public enum ImageFileSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Path = E._2,
        Name = E._3,
    }
}
