

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCustomerGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.customer_grouping.customer_grouping_master
{
    public class CustomerGroupingMasterRoute : Root
    {
        public const string FE = "/customer-grouping/customer-grouping-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class CustomerGroupingMasterController : ApiController
    {
        
        
        private ICustomerGroupingService CustomerGroupingService;

        public CustomerGroupingMasterController(
            
            ICustomerGroupingService CustomerGroupingService
        )
        {
            
            this.CustomerGroupingService = CustomerGroupingService;
        }


        [Route(CustomerGroupingMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] CustomerGroupingMaster_CustomerGroupingFilterDTO CustomerGroupingMaster_CustomerGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = ConvertFilterDTOToFilterEntity(CustomerGroupingMaster_CustomerGroupingFilterDTO);

            return await CustomerGroupingService.Count(CustomerGroupingFilter);
        }

        [Route(CustomerGroupingMasterRoute.List), HttpPost]
        public async Task<List<CustomerGroupingMaster_CustomerGroupingDTO>> List([FromBody] CustomerGroupingMaster_CustomerGroupingFilterDTO CustomerGroupingMaster_CustomerGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = ConvertFilterDTOToFilterEntity(CustomerGroupingMaster_CustomerGroupingFilterDTO);

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);

            return CustomerGroupings.Select(c => new CustomerGroupingMaster_CustomerGroupingDTO(c)).ToList();
        }

        [Route(CustomerGroupingMasterRoute.Get), HttpPost]
        public async Task<CustomerGroupingMaster_CustomerGroupingDTO> Get([FromBody]CustomerGroupingMaster_CustomerGroupingDTO CustomerGroupingMaster_CustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGrouping CustomerGrouping = await CustomerGroupingService.Get(CustomerGroupingMaster_CustomerGroupingDTO.Id);
            return new CustomerGroupingMaster_CustomerGroupingDTO(CustomerGrouping);
        }


        public CustomerGroupingFilter ConvertFilterDTOToFilterEntity(CustomerGroupingMaster_CustomerGroupingFilterDTO CustomerGroupingMaster_CustomerGroupingFilterDTO)
        {
            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.ALL;
            
            CustomerGroupingFilter.Id = new LongFilter{ Equal = CustomerGroupingMaster_CustomerGroupingFilterDTO.Id };
            CustomerGroupingFilter.Name = new StringFilter{ StartsWith = CustomerGroupingMaster_CustomerGroupingFilterDTO.Name };
            return CustomerGroupingFilter;
        }
        
        
    }
}
