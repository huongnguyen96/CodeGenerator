

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MOrderContent;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MOrder;


namespace WG.Controllers.order_content.order_content_master
{
    public class OrderContentMasterRoute : Root
    {
        public const string FE = "/order-content/order-content-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListOrder= Default + "/single-list-order";
    }

    public class OrderContentMasterController : ApiController
    {
        
        
        private IOrderService OrderService;
        private IOrderContentService OrderContentService;

        public OrderContentMasterController(
            
            IOrderService OrderService,
            IOrderContentService OrderContentService
        )
        {
            
            this.OrderService = OrderService;
            this.OrderContentService = OrderContentService;
        }


        [Route(OrderContentMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] OrderContentMaster_OrderContentFilterDTO OrderContentMaster_OrderContentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContentFilter OrderContentFilter = ConvertFilterDTOToFilterEntity(OrderContentMaster_OrderContentFilterDTO);

            return await OrderContentService.Count(OrderContentFilter);
        }

        [Route(OrderContentMasterRoute.List), HttpPost]
        public async Task<List<OrderContentMaster_OrderContentDTO>> List([FromBody] OrderContentMaster_OrderContentFilterDTO OrderContentMaster_OrderContentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContentFilter OrderContentFilter = ConvertFilterDTOToFilterEntity(OrderContentMaster_OrderContentFilterDTO);

            List<OrderContent> OrderContents = await OrderContentService.List(OrderContentFilter);

            return OrderContents.Select(c => new OrderContentMaster_OrderContentDTO(c)).ToList();
        }

        [Route(OrderContentMasterRoute.Get), HttpPost]
        public async Task<OrderContentMaster_OrderContentDTO> Get([FromBody]OrderContentMaster_OrderContentDTO OrderContentMaster_OrderContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContent OrderContent = await OrderContentService.Get(OrderContentMaster_OrderContentDTO.Id);
            return new OrderContentMaster_OrderContentDTO(OrderContent);
        }


        public OrderContentFilter ConvertFilterDTOToFilterEntity(OrderContentMaster_OrderContentFilterDTO OrderContentMaster_OrderContentFilterDTO)
        {
            OrderContentFilter OrderContentFilter = new OrderContentFilter();
            OrderContentFilter.Selects = OrderContentSelect.ALL;
            
            OrderContentFilter.Id = new LongFilter{ Equal = OrderContentMaster_OrderContentFilterDTO.Id };
            OrderContentFilter.OrderId = new LongFilter{ Equal = OrderContentMaster_OrderContentFilterDTO.OrderId };
            OrderContentFilter.ItemName = new StringFilter{ StartsWith = OrderContentMaster_OrderContentFilterDTO.ItemName };
            OrderContentFilter.FirstVersion = new StringFilter{ StartsWith = OrderContentMaster_OrderContentFilterDTO.FirstVersion };
            OrderContentFilter.SecondVersion = new StringFilter{ StartsWith = OrderContentMaster_OrderContentFilterDTO.SecondVersion };
            OrderContentFilter.ThirdVersion = new StringFilter{ StartsWith = OrderContentMaster_OrderContentFilterDTO.ThirdVersion };
            OrderContentFilter.Price = new LongFilter{ Equal = OrderContentMaster_OrderContentFilterDTO.Price };
            OrderContentFilter.DiscountPrice = new LongFilter{ Equal = OrderContentMaster_OrderContentFilterDTO.DiscountPrice };
            return OrderContentFilter;
        }
        
        
        [Route(OrderContentMasterRoute.SingleListOrder), HttpPost]
        public async Task<List<OrderContentMaster_OrderDTO>> SingleListOrder([FromBody] OrderContentMaster_OrderFilterDTO OrderContentMaster_OrderFilterDTO)
        {
            OrderFilter OrderFilter = new OrderFilter();
            OrderFilter.Skip = 0;
            OrderFilter.Take = 20;
            OrderFilter.OrderBy = OrderOrder.Id;
            OrderFilter.OrderType = OrderType.ASC;
            OrderFilter.Selects = OrderSelect.ALL;
            
            OrderFilter.Id = new LongFilter{ Equal = OrderContentMaster_OrderFilterDTO.Id };
            OrderFilter.CustomerId = new LongFilter{ Equal = OrderContentMaster_OrderFilterDTO.CustomerId };
            OrderFilter.CreatedDate = new DateTimeFilter{ Equal = OrderContentMaster_OrderFilterDTO.CreatedDate };
            OrderFilter.VoucherCode = new StringFilter{ StartsWith = OrderContentMaster_OrderFilterDTO.VoucherCode };
            OrderFilter.Total = new LongFilter{ Equal = OrderContentMaster_OrderFilterDTO.Total };
            OrderFilter.VoucherDiscount = new LongFilter{ Equal = OrderContentMaster_OrderFilterDTO.VoucherDiscount };
            OrderFilter.CampaignDiscount = new LongFilter{ Equal = OrderContentMaster_OrderFilterDTO.CampaignDiscount };

            List<Order> Orders = await OrderService.List(OrderFilter);
            List<OrderContentMaster_OrderDTO> OrderContentMaster_OrderDTOs = Orders
                .Select(x => new OrderContentMaster_OrderDTO(x)).ToList();
            return OrderContentMaster_OrderDTOs;
        }

    }
}
