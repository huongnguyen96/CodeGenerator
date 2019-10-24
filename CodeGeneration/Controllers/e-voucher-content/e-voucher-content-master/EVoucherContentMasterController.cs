

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MEVoucherContent;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MEVoucher;


namespace WG.Controllers.e_voucher_content.e_voucher_content_master
{
    public class EVoucherContentMasterRoute : Root
    {
        public const string FE = "/e-voucher-content/e-voucher-content-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListEVoucher= Default + "/single-list-e-voucher";
    }

    public class EVoucherContentMasterController : ApiController
    {
        
        
        private IEVoucherService EVoucherService;
        private IEVoucherContentService EVoucherContentService;

        public EVoucherContentMasterController(
            
            IEVoucherService EVoucherService,
            IEVoucherContentService EVoucherContentService
        )
        {
            
            this.EVoucherService = EVoucherService;
            this.EVoucherContentService = EVoucherContentService;
        }


        [Route(EVoucherContentMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] EVoucherContentMaster_EVoucherContentFilterDTO EVoucherContentMaster_EVoucherContentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContentFilter EVoucherContentFilter = ConvertFilterDTOToFilterEntity(EVoucherContentMaster_EVoucherContentFilterDTO);

            return await EVoucherContentService.Count(EVoucherContentFilter);
        }

        [Route(EVoucherContentMasterRoute.List), HttpPost]
        public async Task<List<EVoucherContentMaster_EVoucherContentDTO>> List([FromBody] EVoucherContentMaster_EVoucherContentFilterDTO EVoucherContentMaster_EVoucherContentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContentFilter EVoucherContentFilter = ConvertFilterDTOToFilterEntity(EVoucherContentMaster_EVoucherContentFilterDTO);

            List<EVoucherContent> EVoucherContents = await EVoucherContentService.List(EVoucherContentFilter);

            return EVoucherContents.Select(c => new EVoucherContentMaster_EVoucherContentDTO(c)).ToList();
        }

        [Route(EVoucherContentMasterRoute.Get), HttpPost]
        public async Task<EVoucherContentMaster_EVoucherContentDTO> Get([FromBody]EVoucherContentMaster_EVoucherContentDTO EVoucherContentMaster_EVoucherContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContent EVoucherContent = await EVoucherContentService.Get(EVoucherContentMaster_EVoucherContentDTO.Id);
            return new EVoucherContentMaster_EVoucherContentDTO(EVoucherContent);
        }


        public EVoucherContentFilter ConvertFilterDTOToFilterEntity(EVoucherContentMaster_EVoucherContentFilterDTO EVoucherContentMaster_EVoucherContentFilterDTO)
        {
            EVoucherContentFilter EVoucherContentFilter = new EVoucherContentFilter();
            EVoucherContentFilter.Selects = EVoucherContentSelect.ALL;
            
            EVoucherContentFilter.Id = new LongFilter{ Equal = EVoucherContentMaster_EVoucherContentFilterDTO.Id };
            EVoucherContentFilter.EVourcherId = new LongFilter{ Equal = EVoucherContentMaster_EVoucherContentFilterDTO.EVourcherId };
            EVoucherContentFilter.UsedCode = new StringFilter{ StartsWith = EVoucherContentMaster_EVoucherContentFilterDTO.UsedCode };
            EVoucherContentFilter.MerchantCode = new StringFilter{ StartsWith = EVoucherContentMaster_EVoucherContentFilterDTO.MerchantCode };
            EVoucherContentFilter.UsedDate = new DateTimeFilter{ Equal = EVoucherContentMaster_EVoucherContentFilterDTO.UsedDate };
            return EVoucherContentFilter;
        }
        
        
        [Route(EVoucherContentMasterRoute.SingleListEVoucher), HttpPost]
        public async Task<List<EVoucherContentMaster_EVoucherDTO>> SingleListEVoucher([FromBody] EVoucherContentMaster_EVoucherFilterDTO EVoucherContentMaster_EVoucherFilterDTO)
        {
            EVoucherFilter EVoucherFilter = new EVoucherFilter();
            EVoucherFilter.Skip = 0;
            EVoucherFilter.Take = 20;
            EVoucherFilter.OrderBy = EVoucherOrder.Id;
            EVoucherFilter.OrderType = OrderType.ASC;
            EVoucherFilter.Selects = EVoucherSelect.ALL;
            
            EVoucherFilter.Id = new LongFilter{ Equal = EVoucherContentMaster_EVoucherFilterDTO.Id };
            EVoucherFilter.CustomerId = new LongFilter{ Equal = EVoucherContentMaster_EVoucherFilterDTO.CustomerId };
            EVoucherFilter.ProductId = new LongFilter{ Equal = EVoucherContentMaster_EVoucherFilterDTO.ProductId };
            EVoucherFilter.Name = new StringFilter{ StartsWith = EVoucherContentMaster_EVoucherFilterDTO.Name };
            EVoucherFilter.Start = new DateTimeFilter{ Equal = EVoucherContentMaster_EVoucherFilterDTO.Start };
            EVoucherFilter.End = new DateTimeFilter{ Equal = EVoucherContentMaster_EVoucherFilterDTO.End };
            EVoucherFilter.Quantity = new LongFilter{ Equal = EVoucherContentMaster_EVoucherFilterDTO.Quantity };

            List<EVoucher> EVouchers = await EVoucherService.List(EVoucherFilter);
            List<EVoucherContentMaster_EVoucherDTO> EVoucherContentMaster_EVoucherDTOs = EVouchers
                .Select(x => new EVoucherContentMaster_EVoucherDTO(x)).ToList();
            return EVoucherContentMaster_EVoucherDTOs;
        }

    }
}
