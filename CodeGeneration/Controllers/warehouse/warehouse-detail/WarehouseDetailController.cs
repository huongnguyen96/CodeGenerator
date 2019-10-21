

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MPartner;


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
        
        public const string SingleListPartner="/single-list-partner";
    }

    public class WarehouseDetailController : ApiController
    {
        
        
        private IPartnerService PartnerService;
        private IWarehouseService WarehouseService;

        public WarehouseDetailController(
            
            IPartnerService PartnerService,
            IWarehouseService WarehouseService
        )
        {
            
            this.PartnerService = PartnerService;
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
        
        
        [Route(WarehouseDetailRoute.SingleListPartner), HttpPost]
        public async Task<List<WarehouseDetail_PartnerDTO>> SingleListPartner([FromBody] WarehouseDetail_PartnerFilterDTO WarehouseDetail_PartnerFilterDTO)
        {
            PartnerFilter PartnerFilter = new PartnerFilter();
            PartnerFilter.Skip = 0;
            PartnerFilter.Take = 20;
            PartnerFilter.OrderBy = PartnerOrder.Id;
            PartnerFilter.OrderType = OrderType.ASC;
            PartnerFilter.Selects = PartnerSelect.ALL;
            
            PartnerFilter.Id = new LongFilter{ Equal = WarehouseDetail_PartnerFilterDTO.Id };
            PartnerFilter.Name = new StringFilter{ StartsWith = WarehouseDetail_PartnerFilterDTO.Name };
            PartnerFilter.Phone = new StringFilter{ StartsWith = WarehouseDetail_PartnerFilterDTO.Phone };
            PartnerFilter.ContactPerson = new StringFilter{ StartsWith = WarehouseDetail_PartnerFilterDTO.ContactPerson };
            PartnerFilter.Address = new StringFilter{ StartsWith = WarehouseDetail_PartnerFilterDTO.Address };

            List<Partner> Partners = await PartnerService.List(PartnerFilter);
            List<WarehouseDetail_PartnerDTO> WarehouseDetail_PartnerDTOs = Partners
                .Select(x => new WarehouseDetail_PartnerDTO(x)).ToList();
            return WarehouseDetail_PartnerDTOs;
        }

    }
}
