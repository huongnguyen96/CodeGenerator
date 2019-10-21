

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDiscountCustomerGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MDiscount;


namespace WG.Controllers.discount_customer_grouping.discount_customer_grouping_detail
{
    public class DiscountCustomerGroupingDetailRoute : Root
    {
        public const string FE = "/discount-customer-grouping/discount-customer-grouping-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListDiscount="/single-list-discount";
    }

    public class DiscountCustomerGroupingDetailController : ApiController
    {
        
        
        private IDiscountService DiscountService;
        private IDiscountCustomerGroupingService DiscountCustomerGroupingService;

        public DiscountCustomerGroupingDetailController(
            
            IDiscountService DiscountService,
            IDiscountCustomerGroupingService DiscountCustomerGroupingService
        )
        {
            
            this.DiscountService = DiscountService;
            this.DiscountCustomerGroupingService = DiscountCustomerGroupingService;
        }


        [Route(DiscountCustomerGroupingDetailRoute.Get), HttpPost]
        public async Task<DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO> Get([FromBody]DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGrouping DiscountCustomerGrouping = await DiscountCustomerGroupingService.Get(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO.Id);
            return new DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO(DiscountCustomerGrouping);
        }


        [Route(DiscountCustomerGroupingDetailRoute.Create), HttpPost]
        public async Task<ActionResult<DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO>> Create([FromBody] DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGrouping DiscountCustomerGrouping = ConvertDTOToEntity(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO);

            DiscountCustomerGrouping = await DiscountCustomerGroupingService.Create(DiscountCustomerGrouping);
            DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO = new DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO(DiscountCustomerGrouping);
            if (DiscountCustomerGrouping.IsValidated)
                return DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO;
            else
                return BadRequest(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO);        
        }

        [Route(DiscountCustomerGroupingDetailRoute.Update), HttpPost]
        public async Task<ActionResult<DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO>> Update([FromBody] DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGrouping DiscountCustomerGrouping = ConvertDTOToEntity(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO);

            DiscountCustomerGrouping = await DiscountCustomerGroupingService.Update(DiscountCustomerGrouping);
            DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO = new DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO(DiscountCustomerGrouping);
            if (DiscountCustomerGrouping.IsValidated)
                return DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO;
            else
                return BadRequest(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO);        
        }

        [Route(DiscountCustomerGroupingDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO>> Delete([FromBody] DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGrouping DiscountCustomerGrouping = ConvertDTOToEntity(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO);

            DiscountCustomerGrouping = await DiscountCustomerGroupingService.Delete(DiscountCustomerGrouping);
            DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO = new DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO(DiscountCustomerGrouping);
            if (DiscountCustomerGrouping.IsValidated)
                return DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO;
            else
                return BadRequest(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO);        
        }

        public DiscountCustomerGrouping ConvertDTOToEntity(DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO)
        {
            DiscountCustomerGrouping DiscountCustomerGrouping = new DiscountCustomerGrouping();
            
            DiscountCustomerGrouping.Id = DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO.Id;
            DiscountCustomerGrouping.DiscountId = DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO.DiscountId;
            DiscountCustomerGrouping.CustomerGroupingCode = DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO.CustomerGroupingCode;
            return DiscountCustomerGrouping;
        }
        
        
        [Route(DiscountCustomerGroupingDetailRoute.SingleListDiscount), HttpPost]
        public async Task<List<DiscountCustomerGroupingDetail_DiscountDTO>> SingleListDiscount([FromBody] DiscountCustomerGroupingDetail_DiscountFilterDTO DiscountCustomerGroupingDetail_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Skip = 0;
            DiscountFilter.Take = 20;
            DiscountFilter.OrderBy = DiscountOrder.Id;
            DiscountFilter.OrderType = OrderType.ASC;
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountCustomerGroupingDetail_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountCustomerGroupingDetail_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountCustomerGroupingDetail_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountCustomerGroupingDetail_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountCustomerGroupingDetail_DiscountFilterDTO.Type };

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);
            List<DiscountCustomerGroupingDetail_DiscountDTO> DiscountCustomerGroupingDetail_DiscountDTOs = Discounts
                .Select(x => new DiscountCustomerGroupingDetail_DiscountDTO(x)).ToList();
            return DiscountCustomerGroupingDetail_DiscountDTOs;
        }

    }
}
