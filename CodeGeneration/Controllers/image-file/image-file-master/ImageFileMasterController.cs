

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MImageFile;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.image_file.image_file_master
{
    public class ImageFileMasterRoute : Root
    {
        public const string FE = "/image-file/image-file-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ImageFileMasterController : ApiController
    {
        
        
        private IImageFileService ImageFileService;

        public ImageFileMasterController(
            
            IImageFileService ImageFileService
        )
        {
            
            this.ImageFileService = ImageFileService;
        }


        [Route(ImageFileMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ImageFileMaster_ImageFileFilterDTO ImageFileMaster_ImageFileFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFileFilter ImageFileFilter = ConvertFilterDTOToFilterEntity(ImageFileMaster_ImageFileFilterDTO);

            return await ImageFileService.Count(ImageFileFilter);
        }

        [Route(ImageFileMasterRoute.List), HttpPost]
        public async Task<List<ImageFileMaster_ImageFileDTO>> List([FromBody] ImageFileMaster_ImageFileFilterDTO ImageFileMaster_ImageFileFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFileFilter ImageFileFilter = ConvertFilterDTOToFilterEntity(ImageFileMaster_ImageFileFilterDTO);

            List<ImageFile> ImageFiles = await ImageFileService.List(ImageFileFilter);

            return ImageFiles.Select(c => new ImageFileMaster_ImageFileDTO(c)).ToList();
        }

        [Route(ImageFileMasterRoute.Get), HttpPost]
        public async Task<ImageFileMaster_ImageFileDTO> Get([FromBody]ImageFileMaster_ImageFileDTO ImageFileMaster_ImageFileDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFile ImageFile = await ImageFileService.Get(ImageFileMaster_ImageFileDTO.Id);
            return new ImageFileMaster_ImageFileDTO(ImageFile);
        }


        public ImageFileFilter ConvertFilterDTOToFilterEntity(ImageFileMaster_ImageFileFilterDTO ImageFileMaster_ImageFileFilterDTO)
        {
            ImageFileFilter ImageFileFilter = new ImageFileFilter();
            
            ImageFileFilter.Id = new LongFilter{ Equal = ImageFileMaster_ImageFileFilterDTO.Id };
            ImageFileFilter.Path = new StringFilter{ StartsWith = ImageFileMaster_ImageFileFilterDTO.Path };
            ImageFileFilter.Name = new StringFilter{ StartsWith = ImageFileMaster_ImageFileFilterDTO.Name };
            return ImageFileFilter;
        }
        
        
    }
}
