

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MMerchant;


namespace WG.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetailRoute : Root
    {
        public const string FE = "/warehouse/warehouse-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListMerchant= Default + "/single-list-merchant";
    }

    public class WarehouseDetailController : ApiController
    {
        
        
        private IMerchantService MerchantService;
        private IWarehouseService WarehouseService;

        public WarehouseDetailController(
            
            IMerchantService MerchantService,
            IWarehouseService WarehouseService
        )
        {
            
            this.MerchantService = MerchantService;
            this.WarehouseService = WarehouseService;
        }


        [Route(WarehouseDetailRoute.Get), HttpPost]
        public async Task<WarehouseDetail_WarehouseDTO> Get([FromBody]WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = await WarehouseService.Get(WarehouseDetail_WarehouseDTO.Id);
            return new WarehouseDetail_WarehouseDTO(Warehouse);
        }


        [Route(WarehouseDetailRoute.Create), HttpPost]
        public async Task<ActionResult<WarehouseDetail_WarehouseDTO>> Create([FromBody] WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = ConvertDTOToEntity(WarehouseDetail_WarehouseDTO);

            Warehouse = await WarehouseService.Create(Warehouse);
            WarehouseDetail_WarehouseDTO = new WarehouseDetail_WarehouseDTO(Warehouse);
            if (Warehouse.IsValidated)
                return WarehouseDetail_WarehouseDTO;
            else
                return BadRequest(WarehouseDetail_WarehouseDTO);        
        }

        [Route(WarehouseDetailRoute.Update), HttpPost]
        public async Task<ActionResult<WarehouseDetail_WarehouseDTO>> Update([FromBody] WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = ConvertDTOToEntity(WarehouseDetail_WarehouseDTO);

            Warehouse = await WarehouseService.Update(Warehouse);
            WarehouseDetail_WarehouseDTO = new WarehouseDetail_WarehouseDTO(Warehouse);
            if (Warehouse.IsValidated)
                return WarehouseDetail_WarehouseDTO;
            else
                return BadRequest(WarehouseDetail_WarehouseDTO);        
        }

        [Route(WarehouseDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<WarehouseDetail_WarehouseDTO>> Delete([FromBody] WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = ConvertDTOToEntity(WarehouseDetail_WarehouseDTO);

            Warehouse = await WarehouseService.Delete(Warehouse);
            WarehouseDetail_WarehouseDTO = new WarehouseDetail_WarehouseDTO(Warehouse);
            if (Warehouse.IsValidated)
                return WarehouseDetail_WarehouseDTO;
            else
                return BadRequest(WarehouseDetail_WarehouseDTO);        
        }

        public Warehouse ConvertDTOToEntity(WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            Warehouse Warehouse = new Warehouse();
            
            Warehouse.Id = WarehouseDetail_WarehouseDTO.Id;
            Warehouse.Name = WarehouseDetail_WarehouseDTO.Name;
            Warehouse.Phone = WarehouseDetail_WarehouseDTO.Phone;
            Warehouse.Email = WarehouseDetail_WarehouseDTO.Email;
            Warehouse.Address = WarehouseDetail_WarehouseDTO.Address;
            Warehouse.PartnerId = WarehouseDetail_WarehouseDTO.PartnerId;
            return Warehouse;
        }
        
        
        [Route(WarehouseDetailRoute.SingleListMerchant), HttpPost]
        public async Task<List<WarehouseDetail_MerchantDTO>> SingleListMerchant([FromBody] WarehouseDetail_MerchantFilterDTO WarehouseDetail_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Skip = 0;
            MerchantFilter.Take = 20;
            MerchantFilter.OrderBy = MerchantOrder.Id;
            MerchantFilter.OrderType = OrderType.ASC;
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = WarehouseDetail_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = WarehouseDetail_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = WarehouseDetail_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = WarehouseDetail_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = WarehouseDetail_MerchantFilterDTO.Address };

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);
            List<WarehouseDetail_MerchantDTO> WarehouseDetail_MerchantDTOs = Merchants
                .Select(x => new WarehouseDetail_MerchantDTO(x)).ToList();
            return WarehouseDetail_MerchantDTOs;
        }

    }
}
