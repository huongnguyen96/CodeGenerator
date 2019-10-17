

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MSupplier;


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
        
        public const string SingleListSupplier="/single-list-supplier";
    }

    public class WarehouseDetailController : ApiController
    {
        
        
        private ISupplierService SupplierService;
        private IWarehouseService WarehouseService;

        public WarehouseDetailController(
            
            ISupplierService SupplierService,
            IWarehouseService WarehouseService
        )
        {
            
            this.SupplierService = SupplierService;
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
            Warehouse.SupplierId = WarehouseDetail_WarehouseDTO.SupplierId;
            return Warehouse;
        }
        
        
        [Route(WarehouseDetailRoute.SingleListSupplier), HttpPost]
        public async Task<List<WarehouseDetail_SupplierDTO>> SingleListSupplier([FromBody] WarehouseDetail_SupplierFilterDTO WarehouseDetail_SupplierFilterDTO)
        {
            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            
            SupplierFilter.Id = new LongFilter{ Equal = WarehouseDetail_SupplierFilterDTO.Id };
            SupplierFilter.Name = new StringFilter{ StartsWith = WarehouseDetail_SupplierFilterDTO.Name };
            SupplierFilter.Phone = new StringFilter{ StartsWith = WarehouseDetail_SupplierFilterDTO.Phone };
            SupplierFilter.ContactPerson = new StringFilter{ StartsWith = WarehouseDetail_SupplierFilterDTO.ContactPerson };
            SupplierFilter.Address = new StringFilter{ StartsWith = WarehouseDetail_SupplierFilterDTO.Address };

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<WarehouseDetail_SupplierDTO> WarehouseDetail_SupplierDTOs = Suppliers
                .Select(x => new WarehouseDetail_SupplierDTO(x)).ToList();
            return WarehouseDetail_SupplierDTOs;
        }

    }
}
