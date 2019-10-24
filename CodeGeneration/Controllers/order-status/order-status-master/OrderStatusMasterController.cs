

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MOrderStatus;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.order_status.order_status_master
{
    public class OrderStatusMasterRoute : Root
    {
        public const string FE = "/order-status/order-status-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class OrderStatusMasterController : ApiController
    {
        
        
        private IOrderStatusService OrderStatusService;

        public OrderStatusMasterController(
            
            IOrderStatusService OrderStatusService
        )
        {
            
            this.OrderStatusService = OrderStatusService;
        }


        [Route(OrderStatusMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] OrderStatusMaster_OrderStatusFilterDTO OrderStatusMaster_OrderStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatusFilter OrderStatusFilter = ConvertFilterDTOToFilterEntity(OrderStatusMaster_OrderStatusFilterDTO);

            return await OrderStatusService.Count(OrderStatusFilter);
        }

        [Route(OrderStatusMasterRoute.List), HttpPost]
        public async Task<List<OrderStatusMaster_OrderStatusDTO>> List([FromBody] OrderStatusMaster_OrderStatusFilterDTO OrderStatusMaster_OrderStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatusFilter OrderStatusFilter = ConvertFilterDTOToFilterEntity(OrderStatusMaster_OrderStatusFilterDTO);

            List<OrderStatus> OrderStatuss = await OrderStatusService.List(OrderStatusFilter);

            return OrderStatuss.Select(c => new OrderStatusMaster_OrderStatusDTO(c)).ToList();
        }

        [Route(OrderStatusMasterRoute.Get), HttpPost]
        public async Task<OrderStatusMaster_OrderStatusDTO> Get([FromBody]OrderStatusMaster_OrderStatusDTO OrderStatusMaster_OrderStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderStatus OrderStatus = await OrderStatusService.Get(OrderStatusMaster_OrderStatusDTO.Id);
            return new OrderStatusMaster_OrderStatusDTO(OrderStatus);
        }


        public OrderStatusFilter ConvertFilterDTOToFilterEntity(OrderStatusMaster_OrderStatusFilterDTO OrderStatusMaster_OrderStatusFilterDTO)
        {
            OrderStatusFilter OrderStatusFilter = new OrderStatusFilter();
            OrderStatusFilter.Selects = OrderStatusSelect.ALL;
            
            OrderStatusFilter.Id = new LongFilter{ Equal = OrderStatusMaster_OrderStatusFilterDTO.Id };
            OrderStatusFilter.Code = new StringFilter{ StartsWith = OrderStatusMaster_OrderStatusFilterDTO.Code };
            OrderStatusFilter.Name = new StringFilter{ StartsWith = OrderStatusMaster_OrderStatusFilterDTO.Name };
            OrderStatusFilter.Description = new StringFilter{ StartsWith = OrderStatusMaster_OrderStatusFilterDTO.Description };
            return OrderStatusFilter;
        }
        
        
    }
}
