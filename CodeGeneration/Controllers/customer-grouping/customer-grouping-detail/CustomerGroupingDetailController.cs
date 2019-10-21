

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCustomerGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.customer_grouping.customer_grouping_detail
{
    public class CustomerGroupingDetailRoute : Root
    {
        public const string FE = "/customer-grouping/customer-grouping-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class CustomerGroupingDetailController : ApiController
    {
        
        
        private ICustomerGroupingService CustomerGroupingService;

        public CustomerGroupingDetailController(
            
            ICustomerGroupingService CustomerGroupingService
        )
        {
            
            this.CustomerGroupingService = CustomerGroupingService;
        }


        [Route(CustomerGroupingDetailRoute.Get), HttpPost]
        public async Task<CustomerGroupingDetail_CustomerGroupingDTO> Get([FromBody]CustomerGroupingDetail_CustomerGroupingDTO CustomerGroupingDetail_CustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGrouping CustomerGrouping = await CustomerGroupingService.Get(CustomerGroupingDetail_CustomerGroupingDTO.Id);
            return new CustomerGroupingDetail_CustomerGroupingDTO(CustomerGrouping);
        }


        [Route(CustomerGroupingDetailRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerGroupingDetail_CustomerGroupingDTO>> Create([FromBody] CustomerGroupingDetail_CustomerGroupingDTO CustomerGroupingDetail_CustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGrouping CustomerGrouping = ConvertDTOToEntity(CustomerGroupingDetail_CustomerGroupingDTO);

            CustomerGrouping = await CustomerGroupingService.Create(CustomerGrouping);
            CustomerGroupingDetail_CustomerGroupingDTO = new CustomerGroupingDetail_CustomerGroupingDTO(CustomerGrouping);
            if (CustomerGrouping.IsValidated)
                return CustomerGroupingDetail_CustomerGroupingDTO;
            else
                return BadRequest(CustomerGroupingDetail_CustomerGroupingDTO);        
        }

        [Route(CustomerGroupingDetailRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerGroupingDetail_CustomerGroupingDTO>> Update([FromBody] CustomerGroupingDetail_CustomerGroupingDTO CustomerGroupingDetail_CustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGrouping CustomerGrouping = ConvertDTOToEntity(CustomerGroupingDetail_CustomerGroupingDTO);

            CustomerGrouping = await CustomerGroupingService.Update(CustomerGrouping);
            CustomerGroupingDetail_CustomerGroupingDTO = new CustomerGroupingDetail_CustomerGroupingDTO(CustomerGrouping);
            if (CustomerGrouping.IsValidated)
                return CustomerGroupingDetail_CustomerGroupingDTO;
            else
                return BadRequest(CustomerGroupingDetail_CustomerGroupingDTO);        
        }

        [Route(CustomerGroupingDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerGroupingDetail_CustomerGroupingDTO>> Delete([FromBody] CustomerGroupingDetail_CustomerGroupingDTO CustomerGroupingDetail_CustomerGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerGrouping CustomerGrouping = ConvertDTOToEntity(CustomerGroupingDetail_CustomerGroupingDTO);

            CustomerGrouping = await CustomerGroupingService.Delete(CustomerGrouping);
            CustomerGroupingDetail_CustomerGroupingDTO = new CustomerGroupingDetail_CustomerGroupingDTO(CustomerGrouping);
            if (CustomerGrouping.IsValidated)
                return CustomerGroupingDetail_CustomerGroupingDTO;
            else
                return BadRequest(CustomerGroupingDetail_CustomerGroupingDTO);        
        }

        public CustomerGrouping ConvertDTOToEntity(CustomerGroupingDetail_CustomerGroupingDTO CustomerGroupingDetail_CustomerGroupingDTO)
        {
            CustomerGrouping CustomerGrouping = new CustomerGrouping();
            
            CustomerGrouping.Id = CustomerGroupingDetail_CustomerGroupingDTO.Id;
            CustomerGrouping.Name = CustomerGroupingDetail_CustomerGroupingDTO.Name;
            return CustomerGrouping;
        }
        
        
    }
}
