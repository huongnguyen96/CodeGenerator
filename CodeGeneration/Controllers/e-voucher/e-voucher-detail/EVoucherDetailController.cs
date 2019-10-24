

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MEVoucher;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCustomer;
using WG.Services.MProduct;


namespace WG.Controllers.e_voucher.e_voucher_detail
{
    public class EVoucherDetailRoute : Root
    {
        public const string FE = "/e-voucher/e-voucher-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListCustomer= Default + "/single-list-customer";
        public const string SingleListProduct= Default + "/single-list-product";
    }

    public class EVoucherDetailController : ApiController
    {
        
        
        private ICustomerService CustomerService;
        private IProductService ProductService;
        private IEVoucherService EVoucherService;

        public EVoucherDetailController(
            
            ICustomerService CustomerService,
            IProductService ProductService,
            IEVoucherService EVoucherService
        )
        {
            
            this.CustomerService = CustomerService;
            this.ProductService = ProductService;
            this.EVoucherService = EVoucherService;
        }


        [Route(EVoucherDetailRoute.Get), HttpPost]
        public async Task<EVoucherDetail_EVoucherDTO> Get([FromBody]EVoucherDetail_EVoucherDTO EVoucherDetail_EVoucherDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucher EVoucher = await EVoucherService.Get(EVoucherDetail_EVoucherDTO.Id);
            return new EVoucherDetail_EVoucherDTO(EVoucher);
        }


        [Route(EVoucherDetailRoute.Create), HttpPost]
        public async Task<ActionResult<EVoucherDetail_EVoucherDTO>> Create([FromBody] EVoucherDetail_EVoucherDTO EVoucherDetail_EVoucherDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucher EVoucher = ConvertDTOToEntity(EVoucherDetail_EVoucherDTO);

            EVoucher = await EVoucherService.Create(EVoucher);
            EVoucherDetail_EVoucherDTO = new EVoucherDetail_EVoucherDTO(EVoucher);
            if (EVoucher.IsValidated)
                return EVoucherDetail_EVoucherDTO;
            else
                return BadRequest(EVoucherDetail_EVoucherDTO);        
        }

        [Route(EVoucherDetailRoute.Update), HttpPost]
        public async Task<ActionResult<EVoucherDetail_EVoucherDTO>> Update([FromBody] EVoucherDetail_EVoucherDTO EVoucherDetail_EVoucherDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucher EVoucher = ConvertDTOToEntity(EVoucherDetail_EVoucherDTO);

            EVoucher = await EVoucherService.Update(EVoucher);
            EVoucherDetail_EVoucherDTO = new EVoucherDetail_EVoucherDTO(EVoucher);
            if (EVoucher.IsValidated)
                return EVoucherDetail_EVoucherDTO;
            else
                return BadRequest(EVoucherDetail_EVoucherDTO);        
        }

        [Route(EVoucherDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<EVoucherDetail_EVoucherDTO>> Delete([FromBody] EVoucherDetail_EVoucherDTO EVoucherDetail_EVoucherDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucher EVoucher = ConvertDTOToEntity(EVoucherDetail_EVoucherDTO);

            EVoucher = await EVoucherService.Delete(EVoucher);
            EVoucherDetail_EVoucherDTO = new EVoucherDetail_EVoucherDTO(EVoucher);
            if (EVoucher.IsValidated)
                return EVoucherDetail_EVoucherDTO;
            else
                return BadRequest(EVoucherDetail_EVoucherDTO);        
        }

        public EVoucher ConvertDTOToEntity(EVoucherDetail_EVoucherDTO EVoucherDetail_EVoucherDTO)
        {
            EVoucher EVoucher = new EVoucher();
            
            EVoucher.Id = EVoucherDetail_EVoucherDTO.Id;
            EVoucher.CustomerId = EVoucherDetail_EVoucherDTO.CustomerId;
            EVoucher.ProductId = EVoucherDetail_EVoucherDTO.ProductId;
            EVoucher.Name = EVoucherDetail_EVoucherDTO.Name;
            EVoucher.Start = EVoucherDetail_EVoucherDTO.Start;
            EVoucher.End = EVoucherDetail_EVoucherDTO.End;
            EVoucher.Quantity = EVoucherDetail_EVoucherDTO.Quantity;
            return EVoucher;
        }
        
        
        [Route(EVoucherDetailRoute.SingleListCustomer), HttpPost]
        public async Task<List<EVoucherDetail_CustomerDTO>> SingleListCustomer([FromBody] EVoucherDetail_CustomerFilterDTO EVoucherDetail_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = EVoucherDetail_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = EVoucherDetail_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = EVoucherDetail_CustomerFilterDTO.DisplayName };
            CustomerFilter.PhoneNumber = new StringFilter{ StartsWith = EVoucherDetail_CustomerFilterDTO.PhoneNumber };
            CustomerFilter.Email = new StringFilter{ StartsWith = EVoucherDetail_CustomerFilterDTO.Email };

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<EVoucherDetail_CustomerDTO> EVoucherDetail_CustomerDTOs = Customers
                .Select(x => new EVoucherDetail_CustomerDTO(x)).ToList();
            return EVoucherDetail_CustomerDTOs;
        }

        [Route(EVoucherDetailRoute.SingleListProduct), HttpPost]
        public async Task<List<EVoucherDetail_ProductDTO>> SingleListProduct([FromBody] EVoucherDetail_ProductFilterDTO EVoucherDetail_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = EVoucherDetail_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = EVoucherDetail_ProductFilterDTO.MaximumPurchaseQuantity };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<EVoucherDetail_ProductDTO> EVoucherDetail_ProductDTOs = Products
                .Select(x => new EVoucherDetail_ProductDTO(x)).ToList();
            return EVoucherDetail_ProductDTOs;
        }

    }
}
