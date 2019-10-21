

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MImageFile;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.image_file.image_file_detail
{
    public class ImageFileDetailRoute : Root
    {
        public const string FE = "/image-file/image-file-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ImageFileDetailController : ApiController
    {
        
        
        private IImageFileService ImageFileService;

        public ImageFileDetailController(
            
            IImageFileService ImageFileService
        )
        {
            
            this.ImageFileService = ImageFileService;
        }


        [Route(ImageFileDetailRoute.Get), HttpPost]
        public async Task<ImageFileDetail_ImageFileDTO> Get([FromBody]ImageFileDetail_ImageFileDTO ImageFileDetail_ImageFileDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFile ImageFile = await ImageFileService.Get(ImageFileDetail_ImageFileDTO.Id);
            return new ImageFileDetail_ImageFileDTO(ImageFile);
        }


        [Route(ImageFileDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ImageFileDetail_ImageFileDTO>> Create([FromBody] ImageFileDetail_ImageFileDTO ImageFileDetail_ImageFileDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFile ImageFile = ConvertDTOToEntity(ImageFileDetail_ImageFileDTO);

            ImageFile = await ImageFileService.Create(ImageFile);
            ImageFileDetail_ImageFileDTO = new ImageFileDetail_ImageFileDTO(ImageFile);
            if (ImageFile.IsValidated)
                return ImageFileDetail_ImageFileDTO;
            else
                return BadRequest(ImageFileDetail_ImageFileDTO);        
        }

        [Route(ImageFileDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ImageFileDetail_ImageFileDTO>> Update([FromBody] ImageFileDetail_ImageFileDTO ImageFileDetail_ImageFileDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFile ImageFile = ConvertDTOToEntity(ImageFileDetail_ImageFileDTO);

            ImageFile = await ImageFileService.Update(ImageFile);
            ImageFileDetail_ImageFileDTO = new ImageFileDetail_ImageFileDTO(ImageFile);
            if (ImageFile.IsValidated)
                return ImageFileDetail_ImageFileDTO;
            else
                return BadRequest(ImageFileDetail_ImageFileDTO);        
        }

        [Route(ImageFileDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ImageFileDetail_ImageFileDTO>> Delete([FromBody] ImageFileDetail_ImageFileDTO ImageFileDetail_ImageFileDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ImageFile ImageFile = ConvertDTOToEntity(ImageFileDetail_ImageFileDTO);

            ImageFile = await ImageFileService.Delete(ImageFile);
            ImageFileDetail_ImageFileDTO = new ImageFileDetail_ImageFileDTO(ImageFile);
            if (ImageFile.IsValidated)
                return ImageFileDetail_ImageFileDTO;
            else
                return BadRequest(ImageFileDetail_ImageFileDTO);        
        }

        public ImageFile ConvertDTOToEntity(ImageFileDetail_ImageFileDTO ImageFileDetail_ImageFileDTO)
        {
            ImageFile ImageFile = new ImageFile();
            
            ImageFile.Id = ImageFileDetail_ImageFileDTO.Id;
            ImageFile.Path = ImageFileDetail_ImageFileDTO.Path;
            ImageFile.Name = ImageFileDetail_ImageFileDTO.Name;
            return ImageFile;
        }
        
        
    }
}
