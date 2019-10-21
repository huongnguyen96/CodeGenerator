

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDiscount;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.discount.discount_detail
{
    public class DiscountDetailRoute : Root
    {
        public const string FE = "/discount/discount-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class DiscountDetailController : ApiController
    {
        
        
        private IDiscountService DiscountService;

        public DiscountDetailController(
            
            IDiscountService DiscountService
        )
        {
            
            this.DiscountService = DiscountService;
        }


        [Route(DiscountDetailRoute.Get), HttpPost]
        public async Task<DiscountDetail_DiscountDTO> Get([FromBody]DiscountDetail_DiscountDTO DiscountDetail_DiscountDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Discount Discount = await DiscountService.Get(DiscountDetail_DiscountDTO.Id);
            return new DiscountDetail_DiscountDTO(Discount);
        }


        [Route(DiscountDetailRoute.Create), HttpPost]
        public async Task<ActionResult<DiscountDetail_DiscountDTO>> Create([FromBody] DiscountDetail_DiscountDTO DiscountDetail_DiscountDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Discount Discount = ConvertDTOToEntity(DiscountDetail_DiscountDTO);

            Discount = await DiscountService.Create(Discount);
            DiscountDetail_DiscountDTO = new DiscountDetail_DiscountDTO(Discount);
            if (Discount.IsValidated)
                return DiscountDetail_DiscountDTO;
            else
                return BadRequest(DiscountDetail_DiscountDTO);        
        }

        [Route(DiscountDetailRoute.Update), HttpPost]
        public async Task<ActionResult<DiscountDetail_DiscountDTO>> Update([FromBody] DiscountDetail_DiscountDTO DiscountDetail_DiscountDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Discount Discount = ConvertDTOToEntity(DiscountDetail_DiscountDTO);

            Discount = await DiscountService.Update(Discount);
            DiscountDetail_DiscountDTO = new DiscountDetail_DiscountDTO(Discount);
            if (Discount.IsValidated)
                return DiscountDetail_DiscountDTO;
            else
                return BadRequest(DiscountDetail_DiscountDTO);        
        }

        [Route(DiscountDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<DiscountDetail_DiscountDTO>> Delete([FromBody] DiscountDetail_DiscountDTO DiscountDetail_DiscountDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Discount Discount = ConvertDTOToEntity(DiscountDetail_DiscountDTO);

            Discount = await DiscountService.Delete(Discount);
            DiscountDetail_DiscountDTO = new DiscountDetail_DiscountDTO(Discount);
            if (Discount.IsValidated)
                return DiscountDetail_DiscountDTO;
            else
                return BadRequest(DiscountDetail_DiscountDTO);        
        }

        public Discount ConvertDTOToEntity(DiscountDetail_DiscountDTO DiscountDetail_DiscountDTO)
        {
            Discount Discount = new Discount();
            
            Discount.Id = DiscountDetail_DiscountDTO.Id;
            Discount.Name = DiscountDetail_DiscountDTO.Name;
            Discount.Start = DiscountDetail_DiscountDTO.Start;
            Discount.End = DiscountDetail_DiscountDTO.End;
            Discount.Type = DiscountDetail_DiscountDTO.Type;
            return Discount;
        }
        
        
    }
}
