

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MSupplier;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.supplier.supplier_detail
{
    public class SupplierDetailRoute : Root
    {
        public const string FE = "/supplier/supplier-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class SupplierDetailController : ApiController
    {
        
        
        private ISupplierService SupplierService;

        public SupplierDetailController(
            
            ISupplierService SupplierService
        )
        {
            
            this.SupplierService = SupplierService;
        }


        [Route(SupplierDetailRoute.Get), HttpPost]
        public async Task<SupplierDetail_SupplierDTO> Get([FromBody]SupplierDetail_SupplierDTO SupplierDetail_SupplierDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Supplier Supplier = await SupplierService.Get(SupplierDetail_SupplierDTO.Id);
            return new SupplierDetail_SupplierDTO(Supplier);
        }


        [Route(SupplierDetailRoute.Create), HttpPost]
        public async Task<ActionResult<SupplierDetail_SupplierDTO>> Create([FromBody] SupplierDetail_SupplierDTO SupplierDetail_SupplierDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Supplier Supplier = ConvertDTOToEntity(SupplierDetail_SupplierDTO);

            Supplier = await SupplierService.Create(Supplier);
            SupplierDetail_SupplierDTO = new SupplierDetail_SupplierDTO(Supplier);
            if (Supplier.IsValidated)
                return SupplierDetail_SupplierDTO;
            else
                return BadRequest(SupplierDetail_SupplierDTO);        
        }

        [Route(SupplierDetailRoute.Update), HttpPost]
        public async Task<ActionResult<SupplierDetail_SupplierDTO>> Update([FromBody] SupplierDetail_SupplierDTO SupplierDetail_SupplierDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Supplier Supplier = ConvertDTOToEntity(SupplierDetail_SupplierDTO);

            Supplier = await SupplierService.Update(Supplier);
            SupplierDetail_SupplierDTO = new SupplierDetail_SupplierDTO(Supplier);
            if (Supplier.IsValidated)
                return SupplierDetail_SupplierDTO;
            else
                return BadRequest(SupplierDetail_SupplierDTO);        
        }

        [Route(SupplierDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<SupplierDetail_SupplierDTO>> Delete([FromBody] SupplierDetail_SupplierDTO SupplierDetail_SupplierDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Supplier Supplier = ConvertDTOToEntity(SupplierDetail_SupplierDTO);

            Supplier = await SupplierService.Delete(Supplier);
            SupplierDetail_SupplierDTO = new SupplierDetail_SupplierDTO(Supplier);
            if (Supplier.IsValidated)
                return SupplierDetail_SupplierDTO;
            else
                return BadRequest(SupplierDetail_SupplierDTO);        
        }

        public Supplier ConvertDTOToEntity(SupplierDetail_SupplierDTO SupplierDetail_SupplierDTO)
        {
            Supplier Supplier = new Supplier();
            
            Supplier.Id = SupplierDetail_SupplierDTO.Id;
            Supplier.Name = SupplierDetail_SupplierDTO.Name;
            Supplier.Phone = SupplierDetail_SupplierDTO.Phone;
            Supplier.ContactPerson = SupplierDetail_SupplierDTO.ContactPerson;
            Supplier.Address = SupplierDetail_SupplierDTO.Address;
            return Supplier;
        }
        
        
    }
}
