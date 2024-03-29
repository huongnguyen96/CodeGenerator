

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


namespace WG.Controllers.order.order_detail
{
    public class OrderDetailRoute : Root
    {
        public const string FE = "/order/order-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListCustomer= Default + "/single-list-customer";
        public const string SingleListOrderStatus= Default + "/single-list-order-status";
    }

    public class OrderDetailController : ApiController
    {
        
        
        private ICustomerService CustomerService;
        private IOrderStatusService OrderStatusService;
        private IOrderService OrderService;

        public OrderDetailController(
            
            ICustomerService CustomerService,
            IOrderStatusService OrderStatusService,
            IOrderService OrderService
        )
        {
            
            this.CustomerService = CustomerService;
            this.OrderStatusService = OrderStatusService;
            this.OrderService = OrderService;
        }


        [Route(OrderDetailRoute.Get), HttpPost]
        public async Task<OrderDetail_OrderDTO> Get([FromBody]OrderDetail_OrderDTO OrderDetail_OrderDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Order Order = await OrderService.Get(OrderDetail_OrderDTO.Id);
            return new OrderDetail_OrderDTO(Order);
        }


        [Route(OrderDetailRoute.Create), HttpPost]
        public async Task<ActionResult<OrderDetail_OrderDTO>> Create([FromBody] OrderDetail_OrderDTO OrderDetail_OrderDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Order Order = ConvertDTOToEntity(OrderDetail_OrderDTO);

            Order = await OrderService.Create(Order);
            OrderDetail_OrderDTO = new OrderDetail_OrderDTO(Order);
            if (Order.IsValidated)
                return OrderDetail_OrderDTO;
            else
                return BadRequest(OrderDetail_OrderDTO);        
        }

        [Route(OrderDetailRoute.Update), HttpPost]
        public async Task<ActionResult<OrderDetail_OrderDTO>> Update([FromBody] OrderDetail_OrderDTO OrderDetail_OrderDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Order Order = ConvertDTOToEntity(OrderDetail_OrderDTO);

            Order = await OrderService.Update(Order);
            OrderDetail_OrderDTO = new OrderDetail_OrderDTO(Order);
            if (Order.IsValidated)
                return OrderDetail_OrderDTO;
            else
                return BadRequest(OrderDetail_OrderDTO);        
        }

        [Route(OrderDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<OrderDetail_OrderDTO>> Delete([FromBody] OrderDetail_OrderDTO OrderDetail_OrderDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Order Order = ConvertDTOToEntity(OrderDetail_OrderDTO);

            Order = await OrderService.Delete(Order);
            OrderDetail_OrderDTO = new OrderDetail_OrderDTO(Order);
            if (Order.IsValidated)
                return OrderDetail_OrderDTO;
            else
                return BadRequest(OrderDetail_OrderDTO);        
        }

        public Order ConvertDTOToEntity(OrderDetail_OrderDTO OrderDetail_OrderDTO)
        {
            Order Order = new Order();
            
            Order.Id = OrderDetail_OrderDTO.Id;
            Order.CustomerId = OrderDetail_OrderDTO.CustomerId;
            Order.CreatedDate = OrderDetail_OrderDTO.CreatedDate;
            Order.VoucherCode = OrderDetail_OrderDTO.VoucherCode;
            Order.Total = OrderDetail_OrderDTO.Total;
            Order.VoucherDiscount = OrderDetail_OrderDTO.VoucherDiscount;
            Order.CampaignDiscount = OrderDetail_OrderDTO.CampaignDiscount;
            Order.StatusId = OrderDetail_OrderDTO.StatusId;
            return Order;
        }
        
        
        [Route(OrderDetailRoute.SingleListCustomer), HttpPost]
        public async Task<List<OrderDetail_CustomerDTO>> SingleListCustomer([FromBody] OrderDetail_CustomerFilterDTO OrderDetail_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = OrderDetail_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = OrderDetail_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = OrderDetail_CustomerFilterDTO.DisplayName };
            CustomerFilter.PhoneNumber = new StringFilter{ StartsWith = OrderDetail_CustomerFilterDTO.PhoneNumber };
            CustomerFilter.Email = new StringFilter{ StartsWith = OrderDetail_CustomerFilterDTO.Email };

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<OrderDetail_CustomerDTO> OrderDetail_CustomerDTOs = Customers
                .Select(x => new OrderDetail_CustomerDTO(x)).ToList();
            return OrderDetail_CustomerDTOs;
        }

        [Route(OrderDetailRoute.SingleListOrderStatus), HttpPost]
        public async Task<List<OrderDetail_OrderStatusDTO>> SingleListOrderStatus([FromBody] OrderDetail_OrderStatusFilterDTO OrderDetail_OrderStatusFilterDTO)
        {
            OrderStatusFilter OrderStatusFilter = new OrderStatusFilter();
            OrderStatusFilter.Skip = 0;
            OrderStatusFilter.Take = 20;
            OrderStatusFilter.OrderBy = OrderStatusOrder.Id;
            OrderStatusFilter.OrderType = OrderType.ASC;
            OrderStatusFilter.Selects = OrderStatusSelect.ALL;
            
            OrderStatusFilter.Id = new LongFilter{ Equal = OrderDetail_OrderStatusFilterDTO.Id };
            OrderStatusFilter.Code = new StringFilter{ StartsWith = OrderDetail_OrderStatusFilterDTO.Code };
            OrderStatusFilter.Name = new StringFilter{ StartsWith = OrderDetail_OrderStatusFilterDTO.Name };
            OrderStatusFilter.Description = new StringFilter{ StartsWith = OrderDetail_OrderStatusFilterDTO.Description };

            List<OrderStatus> OrderStatuss = await OrderStatusService.List(OrderStatusFilter);
            List<OrderDetail_OrderStatusDTO> OrderDetail_OrderStatusDTOs = OrderStatuss
                .Select(x => new OrderDetail_OrderStatusDTO(x)).ToList();
            return OrderDetail_OrderStatusDTOs;
        }

    }
}
