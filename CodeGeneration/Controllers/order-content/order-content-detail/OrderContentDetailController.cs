

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MOrderContent;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MOrder;


namespace WG.Controllers.order_content.order_content_detail
{
    public class OrderContentDetailRoute : Root
    {
        public const string FE = "/order-content/order-content-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListOrder= Default + "/single-list-order";
    }

    public class OrderContentDetailController : ApiController
    {
        
        
        private IOrderService OrderService;
        private IOrderContentService OrderContentService;

        public OrderContentDetailController(
            
            IOrderService OrderService,
            IOrderContentService OrderContentService
        )
        {
            
            this.OrderService = OrderService;
            this.OrderContentService = OrderContentService;
        }


        [Route(OrderContentDetailRoute.Get), HttpPost]
        public async Task<OrderContentDetail_OrderContentDTO> Get([FromBody]OrderContentDetail_OrderContentDTO OrderContentDetail_OrderContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContent OrderContent = await OrderContentService.Get(OrderContentDetail_OrderContentDTO.Id);
            return new OrderContentDetail_OrderContentDTO(OrderContent);
        }


        [Route(OrderContentDetailRoute.Create), HttpPost]
        public async Task<ActionResult<OrderContentDetail_OrderContentDTO>> Create([FromBody] OrderContentDetail_OrderContentDTO OrderContentDetail_OrderContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContent OrderContent = ConvertDTOToEntity(OrderContentDetail_OrderContentDTO);

            OrderContent = await OrderContentService.Create(OrderContent);
            OrderContentDetail_OrderContentDTO = new OrderContentDetail_OrderContentDTO(OrderContent);
            if (OrderContent.IsValidated)
                return OrderContentDetail_OrderContentDTO;
            else
                return BadRequest(OrderContentDetail_OrderContentDTO);        
        }

        [Route(OrderContentDetailRoute.Update), HttpPost]
        public async Task<ActionResult<OrderContentDetail_OrderContentDTO>> Update([FromBody] OrderContentDetail_OrderContentDTO OrderContentDetail_OrderContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContent OrderContent = ConvertDTOToEntity(OrderContentDetail_OrderContentDTO);

            OrderContent = await OrderContentService.Update(OrderContent);
            OrderContentDetail_OrderContentDTO = new OrderContentDetail_OrderContentDTO(OrderContent);
            if (OrderContent.IsValidated)
                return OrderContentDetail_OrderContentDTO;
            else
                return BadRequest(OrderContentDetail_OrderContentDTO);        
        }

        [Route(OrderContentDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<OrderContentDetail_OrderContentDTO>> Delete([FromBody] OrderContentDetail_OrderContentDTO OrderContentDetail_OrderContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            OrderContent OrderContent = ConvertDTOToEntity(OrderContentDetail_OrderContentDTO);

            OrderContent = await OrderContentService.Delete(OrderContent);
            OrderContentDetail_OrderContentDTO = new OrderContentDetail_OrderContentDTO(OrderContent);
            if (OrderContent.IsValidated)
                return OrderContentDetail_OrderContentDTO;
            else
                return BadRequest(OrderContentDetail_OrderContentDTO);        
        }

        public OrderContent ConvertDTOToEntity(OrderContentDetail_OrderContentDTO OrderContentDetail_OrderContentDTO)
        {
            OrderContent OrderContent = new OrderContent();
            
            OrderContent.Id = OrderContentDetail_OrderContentDTO.Id;
            OrderContent.OrderId = OrderContentDetail_OrderContentDTO.OrderId;
            OrderContent.ItemName = OrderContentDetail_OrderContentDTO.ItemName;
            OrderContent.FirstVersion = OrderContentDetail_OrderContentDTO.FirstVersion;
            OrderContent.SecondVersion = OrderContentDetail_OrderContentDTO.SecondVersion;
            OrderContent.ThirdVersion = OrderContentDetail_OrderContentDTO.ThirdVersion;
            OrderContent.Price = OrderContentDetail_OrderContentDTO.Price;
            OrderContent.DiscountPrice = OrderContentDetail_OrderContentDTO.DiscountPrice;
            return OrderContent;
        }
        
        
        [Route(OrderContentDetailRoute.SingleListOrder), HttpPost]
        public async Task<List<OrderContentDetail_OrderDTO>> SingleListOrder([FromBody] OrderContentDetail_OrderFilterDTO OrderContentDetail_OrderFilterDTO)
        {
            OrderFilter OrderFilter = new OrderFilter();
            OrderFilter.Skip = 0;
            OrderFilter.Take = 20;
            OrderFilter.OrderBy = OrderOrder.Id;
            OrderFilter.OrderType = OrderType.ASC;
            OrderFilter.Selects = OrderSelect.ALL;
            
            OrderFilter.Id = new LongFilter{ Equal = OrderContentDetail_OrderFilterDTO.Id };
            OrderFilter.CustomerId = new LongFilter{ Equal = OrderContentDetail_OrderFilterDTO.CustomerId };
            OrderFilter.CreatedDate = new DateTimeFilter{ Equal = OrderContentDetail_OrderFilterDTO.CreatedDate };
            OrderFilter.VoucherCode = new StringFilter{ StartsWith = OrderContentDetail_OrderFilterDTO.VoucherCode };
            OrderFilter.Total = new LongFilter{ Equal = OrderContentDetail_OrderFilterDTO.Total };
            OrderFilter.VoucherDiscount = new LongFilter{ Equal = OrderContentDetail_OrderFilterDTO.VoucherDiscount };
            OrderFilter.CampaignDiscount = new LongFilter{ Equal = OrderContentDetail_OrderFilterDTO.CampaignDiscount };

            List<Order> Orders = await OrderService.List(OrderFilter);
            List<OrderContentDetail_OrderDTO> OrderContentDetail_OrderDTOs = Orders
                .Select(x => new OrderContentDetail_OrderDTO(x)).ToList();
            return OrderContentDetail_OrderDTOs;
        }

    }
}
