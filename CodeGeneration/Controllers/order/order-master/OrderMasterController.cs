

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MOrder;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCustomer;
using WG.Services.MOrderStatus;


namespace WG.Controllers.order.order_master
{
    public class OrderMasterRoute : Root
    {
        public const string FE = "/order/order-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListCustomer= Default + "/single-list-customer";
        public const string SingleListOrderStatus= Default + "/single-list-order-status";
    }

    public class OrderMasterController : ApiController
    {
        
        
        private ICustomerService CustomerService;
        private IOrderStatusService OrderStatusService;
        private IOrderService OrderService;

        public OrderMasterController(
            
            ICustomerService CustomerService,
            IOrderStatusService OrderStatusService,
            IOrderService OrderService
        )
        {
            
            this.CustomerService = CustomerService;
            this.OrderStatusService = OrderStatusService;
            this.OrderService = OrderService;
        }


        [Route(OrderMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] OrderMaster_OrderFilterDTO OrderMaster_OrderFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderFilter OrderFilter = ConvertFilterDTOToFilterEntity(OrderMaster_OrderFilterDTO);

            return await OrderService.Count(OrderFilter);
        }

        [Route(OrderMasterRoute.List), HttpPost]
        public async Task<List<OrderMaster_OrderDTO>> List([FromBody] OrderMaster_OrderFilterDTO OrderMaster_OrderFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderFilter OrderFilter = ConvertFilterDTOToFilterEntity(OrderMaster_OrderFilterDTO);

            List<Order> Orders = await OrderService.List(OrderFilter);

            return Orders.Select(c => new OrderMaster_OrderDTO(c)).ToList();
        }

        [Route(OrderMasterRoute.Get), HttpPost]
        public async Task<OrderMaster_OrderDTO> Get([FromBody]OrderMaster_OrderDTO OrderMaster_OrderDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Order Order = await OrderService.Get(OrderMaster_OrderDTO.Id);
            return new OrderMaster_OrderDTO(Order);
        }


        public OrderFilter ConvertFilterDTOToFilterEntity(OrderMaster_OrderFilterDTO OrderMaster_OrderFilterDTO)
        {
            OrderFilter OrderFilter = new OrderFilter();
            OrderFilter.Selects = OrderSelect.ALL;
            
            OrderFilter.Id = new LongFilter{ Equal = OrderMaster_OrderFilterDTO.Id };
            OrderFilter.CustomerId = new LongFilter{ Equal = OrderMaster_OrderFilterDTO.CustomerId };
            OrderFilter.CreatedDate = new DateTimeFilter{ Equal = OrderMaster_OrderFilterDTO.CreatedDate };
            OrderFilter.VoucherCode = new StringFilter{ StartsWith = OrderMaster_OrderFilterDTO.VoucherCode };
            OrderFilter.Total = new LongFilter{ Equal = OrderMaster_OrderFilterDTO.Total };
            OrderFilter.VoucherDiscount = new LongFilter{ Equal = OrderMaster_OrderFilterDTO.VoucherDiscount };
            OrderFilter.CampaignDiscount = new LongFilter{ Equal = OrderMaster_OrderFilterDTO.CampaignDiscount };
            OrderFilter.StatusId = new LongFilter{ Equal = OrderMaster_OrderFilterDTO.StatusId };
            return OrderFilter;
        }
        
        
        [Route(OrderMasterRoute.SingleListCustomer), HttpPost]
        public async Task<List<OrderMaster_CustomerDTO>> SingleListCustomer([FromBody] OrderMaster_CustomerFilterDTO OrderMaster_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = OrderMaster_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = OrderMaster_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = OrderMaster_CustomerFilterDTO.DisplayName };
            CustomerFilter.PhoneNumber = new StringFilter{ StartsWith = OrderMaster_CustomerFilterDTO.PhoneNumber };
            CustomerFilter.Email = new StringFilter{ StartsWith = OrderMaster_CustomerFilterDTO.Email };

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<OrderMaster_CustomerDTO> OrderMaster_CustomerDTOs = Customers
                .Select(x => new OrderMaster_CustomerDTO(x)).ToList();
            return OrderMaster_CustomerDTOs;
        }

        [Route(OrderMasterRoute.SingleListOrderStatus), HttpPost]
        public async Task<List<OrderMaster_OrderStatusDTO>> SingleListOrderStatus([FromBody] OrderMaster_OrderStatusFilterDTO OrderMaster_OrderStatusFilterDTO)
        {
            OrderStatusFilter OrderStatusFilter = new OrderStatusFilter();
            OrderStatusFilter.Skip = 0;
            OrderStatusFilter.Take = 20;
            OrderStatusFilter.OrderBy = OrderStatusOrder.Id;
            OrderStatusFilter.OrderType = OrderType.ASC;
            OrderStatusFilter.Selects = OrderStatusSelect.ALL;
            
            OrderStatusFilter.Id = new LongFilter{ Equal = OrderMaster_OrderStatusFilterDTO.Id };
            OrderStatusFilter.Code = new StringFilter{ StartsWith = OrderMaster_OrderStatusFilterDTO.Code };
            OrderStatusFilter.Name = new StringFilter{ StartsWith = OrderMaster_OrderStatusFilterDTO.Name };
            OrderStatusFilter.Description = new StringFilter{ StartsWith = OrderMaster_OrderStatusFilterDTO.Description };

            List<OrderStatus> OrderStatuss = await OrderStatusService.List(OrderStatusFilter);
            List<OrderMaster_OrderStatusDTO> OrderMaster_OrderStatusDTOs = OrderStatuss
                .Select(x => new OrderMaster_OrderStatusDTO(x)).ToList();
            return OrderMaster_OrderStatusDTOs;
        }

    }
}
