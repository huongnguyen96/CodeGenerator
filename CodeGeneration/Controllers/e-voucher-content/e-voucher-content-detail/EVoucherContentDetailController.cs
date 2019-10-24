

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MEVoucherContent;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MEVoucher;


namespace WG.Controllers.e_voucher_content.e_voucher_content_detail
{
    public class EVoucherContentDetailRoute : Root
    {
        public const string FE = "/e-voucher-content/e-voucher-content-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListEVoucher= Default + "/single-list-e-voucher";
    }

    public class EVoucherContentDetailController : ApiController
    {
        
        
        private IEVoucherService EVoucherService;
        private IEVoucherContentService EVoucherContentService;

        public EVoucherContentDetailController(
            
            IEVoucherService EVoucherService,
            IEVoucherContentService EVoucherContentService
        )
        {
            
            this.EVoucherService = EVoucherService;
            this.EVoucherContentService = EVoucherContentService;
        }


        [Route(EVoucherContentDetailRoute.Get), HttpPost]
        public async Task<EVoucherContentDetail_EVoucherContentDTO> Get([FromBody]EVoucherContentDetail_EVoucherContentDTO EVoucherContentDetail_EVoucherContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContent EVoucherContent = await EVoucherContentService.Get(EVoucherContentDetail_EVoucherContentDTO.Id);
            return new EVoucherContentDetail_EVoucherContentDTO(EVoucherContent);
        }


        [Route(EVoucherContentDetailRoute.Create), HttpPost]
        public async Task<ActionResult<EVoucherContentDetail_EVoucherContentDTO>> Create([FromBody] EVoucherContentDetail_EVoucherContentDTO EVoucherContentDetail_EVoucherContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContent EVoucherContent = ConvertDTOToEntity(EVoucherContentDetail_EVoucherContentDTO);

            EVoucherContent = await EVoucherContentService.Create(EVoucherContent);
            EVoucherContentDetail_EVoucherContentDTO = new EVoucherContentDetail_EVoucherContentDTO(EVoucherContent);
            if (EVoucherContent.IsValidated)
                return EVoucherContentDetail_EVoucherContentDTO;
            else
                return BadRequest(EVoucherContentDetail_EVoucherContentDTO);        
        }

        [Route(EVoucherContentDetailRoute.Update), HttpPost]
        public async Task<ActionResult<EVoucherContentDetail_EVoucherContentDTO>> Update([FromBody] EVoucherContentDetail_EVoucherContentDTO EVoucherContentDetail_EVoucherContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContent EVoucherContent = ConvertDTOToEntity(EVoucherContentDetail_EVoucherContentDTO);

            EVoucherContent = await EVoucherContentService.Update(EVoucherContent);
            EVoucherContentDetail_EVoucherContentDTO = new EVoucherContentDetail_EVoucherContentDTO(EVoucherContent);
            if (EVoucherContent.IsValidated)
                return EVoucherContentDetail_EVoucherContentDTO;
            else
                return BadRequest(EVoucherContentDetail_EVoucherContentDTO);        
        }

        [Route(EVoucherContentDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<EVoucherContentDetail_EVoucherContentDTO>> Delete([FromBody] EVoucherContentDetail_EVoucherContentDTO EVoucherContentDetail_EVoucherContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherContent EVoucherContent = ConvertDTOToEntity(EVoucherContentDetail_EVoucherContentDTO);

            EVoucherContent = await EVoucherContentService.Delete(EVoucherContent);
            EVoucherContentDetail_EVoucherContentDTO = new EVoucherContentDetail_EVoucherContentDTO(EVoucherContent);
            if (EVoucherContent.IsValidated)
                return EVoucherContentDetail_EVoucherContentDTO;
            else
                return BadRequest(EVoucherContentDetail_EVoucherContentDTO);        
        }

        public EVoucherContent ConvertDTOToEntity(EVoucherContentDetail_EVoucherContentDTO EVoucherContentDetail_EVoucherContentDTO)
        {
            EVoucherContent EVoucherContent = new EVoucherContent();
            
            EVoucherContent.Id = EVoucherContentDetail_EVoucherContentDTO.Id;
            EVoucherContent.EVourcherId = EVoucherContentDetail_EVoucherContentDTO.EVourcherId;
            EVoucherContent.UsedCode = EVoucherContentDetail_EVoucherContentDTO.UsedCode;
            EVoucherContent.MerchantCode = EVoucherContentDetail_EVoucherContentDTO.MerchantCode;
            EVoucherContent.UsedDate = EVoucherContentDetail_EVoucherContentDTO.UsedDate;
            return EVoucherContent;
        }
        
        
        [Route(EVoucherContentDetailRoute.SingleListEVoucher), HttpPost]
        public async Task<List<EVoucherContentDetail_EVoucherDTO>> SingleListEVoucher([FromBody] EVoucherContentDetail_EVoucherFilterDTO EVoucherContentDetail_EVoucherFilterDTO)
        {
            EVoucherFilter EVoucherFilter = new EVoucherFilter();
            EVoucherFilter.Skip = 0;
            EVoucherFilter.Take = 20;
            EVoucherFilter.OrderBy = EVoucherOrder.Id;
            EVoucherFilter.OrderType = OrderType.ASC;
            EVoucherFilter.Selects = EVoucherSelect.ALL;
            
            EVoucherFilter.Id = new LongFilter{ Equal = EVoucherContentDetail_EVoucherFilterDTO.Id };
            EVoucherFilter.CustomerId = new LongFilter{ Equal = EVoucherContentDetail_EVoucherFilterDTO.CustomerId };
            EVoucherFilter.ProductId = new LongFilter{ Equal = EVoucherContentDetail_EVoucherFilterDTO.ProductId };
            EVoucherFilter.Name = new StringFilter{ StartsWith = EVoucherContentDetail_EVoucherFilterDTO.Name };
            EVoucherFilter.Start = new DateTimeFilter{ Equal = EVoucherContentDetail_EVoucherFilterDTO.Start };
            EVoucherFilter.End = new DateTimeFilter{ Equal = EVoucherContentDetail_EVoucherFilterDTO.End };
            EVoucherFilter.Quantity = new LongFilter{ Equal = EVoucherContentDetail_EVoucherFilterDTO.Quantity };

            List<EVoucher> EVouchers = await EVoucherService.List(EVoucherFilter);
            List<EVoucherContentDetail_EVoucherDTO> EVoucherContentDetail_EVoucherDTOs = EVouchers
                .Select(x => new EVoucherContentDetail_EVoucherDTO(x)).ToList();
            return EVoucherContentDetail_EVoucherDTOs;
        }

    }
}
