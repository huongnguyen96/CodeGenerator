

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDiscountCustomerGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MDiscount;


namespace WG.Controllers.discount_customer_grouping.discount_customer_grouping_master
{
    public class DiscountCustomerGroupingMasterRoute : Root
    {
        public const string FE = "/discount-customer-grouping/discount-customer-grouping-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListDiscount="/single-list-discount";
    }

    public class DiscountCustomerGroupingMasterController : ApiController
    {
        
        
        private IDiscountService DiscountService;
        private IDiscountCustomerGroupingService DiscountCustomerGroupingService;

        public DiscountCustomerGroupingMasterController(
            
            IDiscountService DiscountService,
            IDiscountCustomerGroupingService DiscountCustomerGroupingService
        )
        {
            
            this.DiscountService = DiscountService;
            this.DiscountCustomerGroupingService = DiscountCustomerGroupingService;
        }


        [Route(DiscountCustomerGroupingMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter = ConvertFilterDTOToFilterEntity(DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO);

            return await DiscountCustomerGroupingService.Count(DiscountCustomerGroupingFilter);
        }

        [Route(DiscountCustomerGroupingMasterRoute.List), HttpPost]
        public async Task<List<DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO>> List([FromBody] DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter = ConvertFilterDTOToFilterEntity(DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO);

            List<DiscountCustomerGrouping> DiscountCustomerGroupings = await DiscountCustomerGroupingService.List(DiscountCustomerGroupingFilter);

            return DiscountCustomerGroupings.Select(c => new DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO(c)).ToList();
        }

        [Route(DiscountCustomerGroupingMasterRoute.Get), HttpPost]
        public async Task<DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO> Get([FromBody]DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountCustomerGrouping DiscountCustomerGrouping = await DiscountCustomerGroupingService.Get(DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO.Id);
            return new DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO(DiscountCustomerGrouping);
        }


        public DiscountCustomerGroupingFilter ConvertFilterDTOToFilterEntity(DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO)
        {
            DiscountCustomerGroupingFilter DiscountCustomerGroupingFilter = new DiscountCustomerGroupingFilter();
            DiscountCustomerGroupingFilter.Selects = DiscountCustomerGroupingSelect.ALL;
            
            DiscountCustomerGroupingFilter.Id = new LongFilter{ Equal = DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO.Id };
            DiscountCustomerGroupingFilter.DiscountId = new LongFilter{ Equal = DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO.DiscountId };
            DiscountCustomerGroupingFilter.CustomerGroupingCode = new StringFilter{ StartsWith = DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO.CustomerGroupingCode };
            return DiscountCustomerGroupingFilter;
        }
        
        
        [Route(DiscountCustomerGroupingMasterRoute.SingleListDiscount), HttpPost]
        public async Task<List<DiscountCustomerGroupingMaster_DiscountDTO>> SingleListDiscount([FromBody] DiscountCustomerGroupingMaster_DiscountFilterDTO DiscountCustomerGroupingMaster_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Skip = 0;
            DiscountFilter.Take = 20;
            DiscountFilter.OrderBy = DiscountOrder.Id;
            DiscountFilter.OrderType = OrderType.ASC;
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountCustomerGroupingMaster_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountCustomerGroupingMaster_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountCustomerGroupingMaster_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountCustomerGroupingMaster_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountCustomerGroupingMaster_DiscountFilterDTO.Type };

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);
            List<DiscountCustomerGroupingMaster_DiscountDTO> DiscountCustomerGroupingMaster_DiscountDTOs = Discounts
                .Select(x => new DiscountCustomerGroupingMaster_DiscountDTO(x)).ToList();
            return DiscountCustomerGroupingMaster_DiscountDTOs;
        }

    }
}
