

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCustomer;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.customer.customer_master
{
    public class CustomerMasterRoute : Root
    {
        public const string FE = "/customer/customer-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class CustomerMasterController : ApiController
    {
        
        
        private ICustomerService CustomerService;

        public CustomerMasterController(
            
            ICustomerService CustomerService
        )
        {
            
            this.CustomerService = CustomerService;
        }


        [Route(CustomerMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] CustomerMaster_CustomerFilterDTO CustomerMaster_CustomerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerFilter CustomerFilter = ConvertFilterDTOToFilterEntity(CustomerMaster_CustomerFilterDTO);

            return await CustomerService.Count(CustomerFilter);
        }

        [Route(CustomerMasterRoute.List), HttpPost]
        public async Task<List<CustomerMaster_CustomerDTO>> List([FromBody] CustomerMaster_CustomerFilterDTO CustomerMaster_CustomerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CustomerFilter CustomerFilter = ConvertFilterDTOToFilterEntity(CustomerMaster_CustomerFilterDTO);

            List<Customer> Customers = await CustomerService.List(CustomerFilter);

            return Customers.Select(c => new CustomerMaster_CustomerDTO(c)).ToList();
        }

        [Route(CustomerMasterRoute.Get), HttpPost]
        public async Task<CustomerMaster_CustomerDTO> Get([FromBody]CustomerMaster_CustomerDTO CustomerMaster_CustomerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Customer Customer = await CustomerService.Get(CustomerMaster_CustomerDTO.Id);
            return new CustomerMaster_CustomerDTO(Customer);
        }


        public CustomerFilter ConvertFilterDTOToFilterEntity(CustomerMaster_CustomerFilterDTO CustomerMaster_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = CustomerMaster_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = CustomerMaster_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = CustomerMaster_CustomerFilterDTO.DisplayName };
            CustomerFilter.PhoneNumber = new StringFilter{ StartsWith = CustomerMaster_CustomerFilterDTO.PhoneNumber };
            CustomerFilter.Email = new StringFilter{ StartsWith = CustomerMaster_CustomerFilterDTO.Email };
            return CustomerFilter;
        }
        
        
    }
}
