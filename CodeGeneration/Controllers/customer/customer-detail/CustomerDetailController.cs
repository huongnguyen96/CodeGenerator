

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCustomer;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.customer.customer_detail
{
    public class CustomerDetailRoute : Root
    {
        public const string FE = "/customer/customer-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class CustomerDetailController : ApiController
    {
        
        
        private ICustomerService CustomerService;

        public CustomerDetailController(
            
            ICustomerService CustomerService
        )
        {
            
            this.CustomerService = CustomerService;
        }


        [Route(CustomerDetailRoute.Get), HttpPost]
        public async Task<CustomerDetail_CustomerDTO> Get([FromBody]CustomerDetail_CustomerDTO CustomerDetail_CustomerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Customer Customer = await CustomerService.Get(CustomerDetail_CustomerDTO.Id);
            return new CustomerDetail_CustomerDTO(Customer);
        }


        [Route(CustomerDetailRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerDetail_CustomerDTO>> Create([FromBody] CustomerDetail_CustomerDTO CustomerDetail_CustomerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Customer Customer = ConvertDTOToEntity(CustomerDetail_CustomerDTO);

            Customer = await CustomerService.Create(Customer);
            CustomerDetail_CustomerDTO = new CustomerDetail_CustomerDTO(Customer);
            if (Customer.IsValidated)
                return CustomerDetail_CustomerDTO;
            else
                return BadRequest(CustomerDetail_CustomerDTO);        
        }

        [Route(CustomerDetailRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerDetail_CustomerDTO>> Update([FromBody] CustomerDetail_CustomerDTO CustomerDetail_CustomerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Customer Customer = ConvertDTOToEntity(CustomerDetail_CustomerDTO);

            Customer = await CustomerService.Update(Customer);
            CustomerDetail_CustomerDTO = new CustomerDetail_CustomerDTO(Customer);
            if (Customer.IsValidated)
                return CustomerDetail_CustomerDTO;
            else
                return BadRequest(CustomerDetail_CustomerDTO);        
        }

        [Route(CustomerDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerDetail_CustomerDTO>> Delete([FromBody] CustomerDetail_CustomerDTO CustomerDetail_CustomerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Customer Customer = ConvertDTOToEntity(CustomerDetail_CustomerDTO);

            Customer = await CustomerService.Delete(Customer);
            CustomerDetail_CustomerDTO = new CustomerDetail_CustomerDTO(Customer);
            if (Customer.IsValidated)
                return CustomerDetail_CustomerDTO;
            else
                return BadRequest(CustomerDetail_CustomerDTO);        
        }

        public Customer ConvertDTOToEntity(CustomerDetail_CustomerDTO CustomerDetail_CustomerDTO)
        {
            Customer Customer = new Customer();
            
            Customer.Id = CustomerDetail_CustomerDTO.Id;
            Customer.Username = CustomerDetail_CustomerDTO.Username;
            Customer.DisplayName = CustomerDetail_CustomerDTO.DisplayName;
            Customer.PhoneNumber = CustomerDetail_CustomerDTO.PhoneNumber;
            Customer.Email = CustomerDetail_CustomerDTO.Email;
            return Customer;
        }
        
        
    }
}
