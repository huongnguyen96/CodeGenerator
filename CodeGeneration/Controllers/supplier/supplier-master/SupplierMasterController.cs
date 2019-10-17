

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MSupplier;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.supplier.supplier_master
{
    public class SupplierMasterRoute : Root
    {
        public const string FE = "/supplier/supplier-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class SupplierMasterController : ApiController
    {
        
        
        private ISupplierService SupplierService;

        public SupplierMasterController(
            
            ISupplierService SupplierService
        )
        {
            
            this.SupplierService = SupplierService;
        }


        [Route(SupplierMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] SupplierMaster_SupplierFilterDTO SupplierMaster_SupplierFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            SupplierFilter SupplierFilter = ConvertFilterDTOToFilterEntity(SupplierMaster_SupplierFilterDTO);

            return await SupplierService.Count(SupplierFilter);
        }

        [Route(SupplierMasterRoute.List), HttpPost]
        public async Task<List<SupplierMaster_SupplierDTO>> List([FromBody] SupplierMaster_SupplierFilterDTO SupplierMaster_SupplierFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            SupplierFilter SupplierFilter = ConvertFilterDTOToFilterEntity(SupplierMaster_SupplierFilterDTO);

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);

            return Suppliers.Select(c => new SupplierMaster_SupplierDTO(c)).ToList();
        }

        [Route(SupplierMasterRoute.Get), HttpPost]
        public async Task<SupplierMaster_SupplierDTO> Get([FromBody]SupplierMaster_SupplierDTO SupplierMaster_SupplierDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Supplier Supplier = await SupplierService.Get(SupplierMaster_SupplierDTO.Id);
            return new SupplierMaster_SupplierDTO(Supplier);
        }


        public SupplierFilter ConvertFilterDTOToFilterEntity(SupplierMaster_SupplierFilterDTO SupplierMaster_SupplierFilterDTO)
        {
            SupplierFilter SupplierFilter = new SupplierFilter();
            
            SupplierFilter.Id = new LongFilter{ Equal = SupplierMaster_SupplierFilterDTO.Id };
            SupplierFilter.Name = new StringFilter{ StartsWith = SupplierMaster_SupplierFilterDTO.Name };
            SupplierFilter.Phone = new StringFilter{ StartsWith = SupplierMaster_SupplierFilterDTO.Phone };
            SupplierFilter.ContactPerson = new StringFilter{ StartsWith = SupplierMaster_SupplierFilterDTO.ContactPerson };
            SupplierFilter.Address = new StringFilter{ StartsWith = SupplierMaster_SupplierFilterDTO.Address };
            return SupplierFilter;
        }
        
        
    }
}
