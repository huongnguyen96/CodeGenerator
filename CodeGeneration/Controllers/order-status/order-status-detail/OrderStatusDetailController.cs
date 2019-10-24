

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MOrderStatus;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.order_status.order_status_detail
{
    public class OrderStatusDetailRoute : Root
    {
        public const string FE = "/order-status/order-status-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class OrderStatusDetailController : ApiController
    {
        
        
        private IOrderStatusService OrderStatusService;

        public OrderStatusDetailController(
            
            IOrderStatusService OrderStatusService
        )
        {
            
            this.OrderStatusService = OrderStatusService;
        }


        [Route(OrderStatusDetailRoute.Get), HttpPost]
        public async Task<OrderStatusDetail_OrderStatusDTO> Get([FromBody]OrderStatusDetail_OrderStatusDTO OrderStatusDetail_OrderStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatus OrderStatus = await OrderStatusService.Get(OrderStatusDetail_OrderStatusDTO.Id);
            return new OrderStatusDetail_OrderStatusDTO(OrderStatus);
        }


        [Route(OrderStatusDetailRoute.Create), HttpPost]
        public async Task<ActionResult<OrderStatusDetail_OrderStatusDTO>> Create([FromBody] OrderStatusDetail_OrderStatusDTO OrderStatusDetail_OrderStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatus OrderStatus = ConvertDTOToEntity(OrderStatusDetail_OrderStatusDTO);

            OrderStatus = await OrderStatusService.Create(OrderStatus);
            OrderStatusDetail_OrderStatusDTO = new OrderStatusDetail_OrderStatusDTO(OrderStatus);
            if (OrderStatus.IsValidated)
                return OrderStatusDetail_OrderStatusDTO;
            else
                return BadRequest(OrderStatusDetail_OrderStatusDTO);        
        }

        [Route(OrderStatusDetailRoute.Update), HttpPost]
        public async Task<ActionResult<OrderStatusDetail_OrderStatusDTO>> Update([FromBody] OrderStatusDetail_OrderStatusDTO OrderStatusDetail_OrderStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatus OrderStatus = ConvertDTOToEntity(OrderStatusDetail_OrderStatusDTO);

            OrderStatus = await OrderStatusService.Update(OrderStatus);
            OrderStatusDetail_OrderStatusDTO = new OrderStatusDetail_OrderStatusDTO(OrderStatus);
            if (OrderStatus.IsValidated)
                return OrderStatusDetail_OrderStatusDTO;
            else
                return BadRequest(OrderStatusDetail_OrderStatusDTO);        
        }

        [Route(OrderStatusDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<OrderStatusDetail_OrderStatusDTO>> Delete([FromBody] OrderStatusDetail_OrderStatusDTO OrderStatusDetail_OrderStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatus OrderStatus = ConvertDTOToEntity(OrderStatusDetail_OrderStatusDTO);

            OrderStatus = await OrderStatusService.Delete(OrderStatus);
            OrderStatusDetail_OrderStatusDTO = new OrderStatusDetail_OrderStatusDTO(OrderStatus);
            if (OrderStatus.IsValidated)
                return OrderStatusDetail_OrderStatusDTO;
            else
                return BadRequest(OrderStatusDetail_OrderStatusDTO);        
        }

        public OrderStatus ConvertDTOToEntity(OrderStatusDetail_OrderStatusDTO OrderStatusDetail_OrderStatusDTO)
        {
            OrderStatus OrderStatus = new OrderStatus();
            
            OrderStatus.Id = OrderStatusDetail_OrderStatusDTO.Id;
            OrderStatus.Code = OrderStatusDetail_OrderStatusDTO.Code;
            OrderStatus.Name = OrderStatusDetail_OrderStatusDTO.Name;
            OrderStatus.Description = OrderStatusDetail_OrderStatusDTO.Description;
            return OrderStatus;
        }
        
        
    }
}
