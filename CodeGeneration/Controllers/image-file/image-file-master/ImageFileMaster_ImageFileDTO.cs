
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.image_file.image_file_master
{
    public class ImageFileMaster_ImageFileDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public ImageFileMaster_ImageFileDTO() {}
        public ImageFileMaster_ImageFileDTO(ImageFile ImageFile)
        {
            
            this.Id = ImageFile.Id;
            this.Path = ImageFile.Path;
            this.Name = ImageFile.Name;
        }
    }

    public class ImageFileMaster_ImageFileFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
