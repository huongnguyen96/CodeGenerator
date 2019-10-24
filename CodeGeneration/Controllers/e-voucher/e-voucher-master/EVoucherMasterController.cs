

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


namespace WG.Controllers.e_voucher.e_voucher_master
{
    public class EVoucherMasterRoute : Root
    {
        public const string FE = "/e-voucher/e-voucher-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListCustomer= Default + "/single-list-customer";
        public const string SingleListProduct= Default + "/single-list-product";
    }

    public class EVoucherMasterController : ApiController
    {
        
        
        private ICustomerService CustomerService;
        private IProductService ProductService;
        private IEVoucherService EVoucherService;

        public EVoucherMasterController(
            
            ICustomerService CustomerService,
            IProductService ProductService,
            IEVoucherService EVoucherService
        )
        {
            
            this.CustomerService = CustomerService;
            this.ProductService = ProductService;
            this.EVoucherService = EVoucherService;
        }


        [Route(EVoucherMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] EVoucherMaster_EVoucherFilterDTO EVoucherMaster_EVoucherFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherFilter EVoucherFilter = ConvertFilterDTOToFilterEntity(EVoucherMaster_EVoucherFilterDTO);

            return await EVoucherService.Count(EVoucherFilter);
        }

        [Route(EVoucherMasterRoute.List), HttpPost]
        public async Task<List<EVoucherMaster_EVoucherDTO>> List([FromBody] EVoucherMaster_EVoucherFilterDTO EVoucherMaster_EVoucherFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucherFilter EVoucherFilter = ConvertFilterDTOToFilterEntity(EVoucherMaster_EVoucherFilterDTO);

            List<EVoucher> EVouchers = await EVoucherService.List(EVoucherFilter);

            return EVouchers.Select(c => new EVoucherMaster_EVoucherDTO(c)).ToList();
        }

        [Route(EVoucherMasterRoute.Get), HttpPost]
        public async Task<EVoucherMaster_EVoucherDTO> Get([FromBody]EVoucherMaster_EVoucherDTO EVoucherMaster_EVoucherDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            EVoucher EVoucher = await EVoucherService.Get(EVoucherMaster_EVoucherDTO.Id);
            return new EVoucherMaster_EVoucherDTO(EVoucher);
        }


        public EVoucherFilter ConvertFilterDTOToFilterEntity(EVoucherMaster_EVoucherFilterDTO EVoucherMaster_EVoucherFilterDTO)
        {
            EVoucherFilter EVoucherFilter = new EVoucherFilter();
            EVoucherFilter.Selects = EVoucherSelect.ALL;
            
            EVoucherFilter.Id = new LongFilter{ Equal = EVoucherMaster_EVoucherFilterDTO.Id };
            EVoucherFilter.CustomerId = new LongFilter{ Equal = EVoucherMaster_EVoucherFilterDTO.CustomerId };
            EVoucherFilter.ProductId = new LongFilter{ Equal = EVoucherMaster_EVoucherFilterDTO.ProductId };
            EVoucherFilter.Name = new StringFilter{ StartsWith = EVoucherMaster_EVoucherFilterDTO.Name };
            EVoucherFilter.Start = new DateTimeFilter{ Equal = EVoucherMaster_EVoucherFilterDTO.Start };
            EVoucherFilter.End = new DateTimeFilter{ Equal = EVoucherMaster_EVoucherFilterDTO.End };
            EVoucherFilter.Quantity = new LongFilter{ Equal = EVoucherMaster_EVoucherFilterDTO.Quantity };
            return EVoucherFilter;
        }
        
        
        [Route(EVoucherMasterRoute.SingleListCustomer), HttpPost]
        public async Task<List<EVoucherMaster_CustomerDTO>> SingleListCustomer([FromBody] EVoucherMaster_CustomerFilterDTO EVoucherMaster_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = EVoucherMaster_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = EVoucherMaster_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = EVoucherMaster_CustomerFilterDTO.DisplayName };
            CustomerFilter.PhoneNumber = new StringFilter{ StartsWith = EVoucherMaster_CustomerFilterDTO.PhoneNumber };
            CustomerFilter.Email = new StringFilter{ StartsWith = EVoucherMaster_CustomerFilterDTO.Email };

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<EVoucherMaster_CustomerDTO> EVoucherMaster_CustomerDTOs = Customers
                .Select(x => new EVoucherMaster_CustomerDTO(x)).ToList();
            return EVoucherMaster_CustomerDTOs;
        }

        [Route(EVoucherMasterRoute.SingleListProduct), HttpPost]
        public async Task<List<EVoucherMaster_ProductDTO>> SingleListProduct([FromBody] EVoucherMaster_ProductFilterDTO EVoucherMaster_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = EVoucherMaster_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = EVoucherMaster_ProductFilterDTO.MaximumPurchaseQuantity };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<EVoucherMaster_ProductDTO> EVoucherMaster_ProductDTOs = Products
                .Select(x => new EVoucherMaster_ProductDTO(x)).ToList();
            return EVoucherMaster_ProductDTOs;
        }

    }
}
