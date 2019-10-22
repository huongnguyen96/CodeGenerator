

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDiscount;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.discount.discount_master
{
    public class DiscountMasterRoute : Root
    {
        public const string FE = "/discount/discount-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class DiscountMasterController : ApiController
    {
        
        
        private IDiscountService DiscountService;

        public DiscountMasterController(
            
            IDiscountService DiscountService
        )
        {
            
            this.DiscountService = DiscountService;
        }


        [Route(DiscountMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] DiscountMaster_DiscountFilterDTO DiscountMaster_DiscountFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountFilter DiscountFilter = ConvertFilterDTOToFilterEntity(DiscountMaster_DiscountFilterDTO);

            return await DiscountService.Count(DiscountFilter);
        }

        [Route(DiscountMasterRoute.List), HttpPost]
        public async Task<List<DiscountMaster_DiscountDTO>> List([FromBody] DiscountMaster_DiscountFilterDTO DiscountMaster_DiscountFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountFilter DiscountFilter = ConvertFilterDTOToFilterEntity(DiscountMaster_DiscountFilterDTO);

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);

            return Discounts.Select(c => new DiscountMaster_DiscountDTO(c)).ToList();
        }

        [Route(DiscountMasterRoute.Get), HttpPost]
        public async Task<DiscountMaster_DiscountDTO> Get([FromBody]DiscountMaster_DiscountDTO DiscountMaster_DiscountDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Discount Discount = await DiscountService.Get(DiscountMaster_DiscountDTO.Id);
            return new DiscountMaster_DiscountDTO(Discount);
        }


        public DiscountFilter ConvertFilterDTOToFilterEntity(DiscountMaster_DiscountFilterDTO DiscountMaster_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountMaster_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountMaster_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountMaster_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountMaster_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountMaster_DiscountFilterDTO.Type };
            return DiscountFilter;
        }
        
        
    }
}
