
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.image_file.image_file_detail
{
    public class ImageFileDetail_ImageFileDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public ImageFileDetail_ImageFileDTO() {}
        public ImageFileDetail_ImageFileDTO(ImageFile ImageFile)
        {
            
            this.Id = ImageFile.Id;
            this.Path = ImageFile.Path;
            this.Name = ImageFile.Name;
        }
    }

    public class ImageFileDetail_ImageFileFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public ImageFileOrder OrderBy { get; set; }
    }
}
