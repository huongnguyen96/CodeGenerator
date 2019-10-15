

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

using WeGift.Services.MUser;


namespace WeGift.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetailRoute : Root
    {
        public const string FE = "/warehouse/warehouse-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListUser="/single-list-user";
    }

    public class WarehouseDetailController : ApiController
    {
        
        
        private IUserService UserService;
        private IWarehouseService WarehouseService;

        public WarehouseDetailController(
            
            IUserService UserService,
            IWarehouseService WarehouseService
        )
        {
            
            this.UserService = UserService;
            this.WarehouseService = WarehouseService;
        }


        [Route(WarehouseDetailRoute.Get), HttpPost]
        public async Task<WarehouseDetail_WarehouseDTO> Get([FromBody]WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = await WarehouseService.Get(WarehouseDetail_WarehouseDTO.Id);
            return new WarehouseDetail_WarehouseDTO(Warehouse);
        }


        [Route(WarehouseDetailRoute.Create), HttpPost]
        public async Task<ActionResult<WarehouseDetail_WarehouseDTO>> Create([FromBody] WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = ConvertDTOToEntity(WarehouseDetail_WarehouseDTO);

            Warehouse = await WarehouseService.Create(Warehouse);
            WarehouseDetail_WarehouseDTO = new WarehouseDetail_WarehouseDTO(Warehouse);
            if (Warehouse.IsValidated)
                return WarehouseDetail_WarehouseDTO;
            else
                return BadRequest(WarehouseDetail_WarehouseDTO);        
        }

        [Route(WarehouseDetailRoute.Update), HttpPost]
        public async Task<ActionResult<WarehouseDetail_WarehouseDTO>> Update([FromBody] WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = ConvertDTOToEntity(WarehouseDetail_WarehouseDTO);

            Warehouse = await WarehouseService.Update(Warehouse);
            WarehouseDetail_WarehouseDTO = new WarehouseDetail_WarehouseDTO(Warehouse);
            if (Warehouse.IsValidated)
                return WarehouseDetail_WarehouseDTO;
            else
                return BadRequest(WarehouseDetail_WarehouseDTO);        
        }

        [Route(WarehouseDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<WarehouseDetail_WarehouseDTO>> Delete([FromBody] WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = ConvertDTOToEntity(WarehouseDetail_WarehouseDTO);

            Warehouse = await WarehouseService.Delete(Warehouse);
            WarehouseDetail_WarehouseDTO = new WarehouseDetail_WarehouseDTO(Warehouse);
            if (Warehouse.IsValidated)
                return WarehouseDetail_WarehouseDTO;
            else
                return BadRequest(WarehouseDetail_WarehouseDTO);        
        }

        public Warehouse ConvertDTOToEntity(WarehouseDetail_WarehouseDTO WarehouseDetail_WarehouseDTO)
        {
            Warehouse Warehouse = new Warehouse();
            
            Warehouse.Id = WarehouseDetail_WarehouseDTO.Id;
            Warehouse.ManagerId = WarehouseDetail_WarehouseDTO.ManagerId;
            Warehouse.Code = WarehouseDetail_WarehouseDTO.Code;
            Warehouse.Name = WarehouseDetail_WarehouseDTO.Name;
            return Warehouse;
        }
        
        
        [Route(WarehouseDetailRoute.SingleListUser), HttpPost]
        public async Task<List<WarehouseDetail_UserDTO>> SingleListUser([FromBody] WarehouseDetail_UserFilterDTO WarehouseDetail_UserFilterDTO)
        {
            UserFilter UserFilter = new UserFilter();
            UserFilter.Skip = 0;
            UserFilter.Take = 10;
            UserFilter.OrderBy = UserOrder.Id;
            UserFilter.OrderType = OrderType.ASC;
            UserFilter.Selects = UserSelect.ALL;
            
            UserFilter.Id = new LongFilter{ Equal = WarehouseDetail_UserFilterDTO.Id };
            UserFilter.Username = new StringFilter{ StartsWith = WarehouseDetail_UserFilterDTO.Username };
            UserFilter.Password = new StringFilter{ StartsWith = WarehouseDetail_UserFilterDTO.Password };

            List<User> Users = await UserService.List(UserFilter);
            List<WarehouseDetail_UserDTO> WarehouseDetail_UserDTOs = Users
                .Select(x => new WarehouseDetail_UserDTO(x)).ToList();
            return WarehouseDetail_UserDTOs;
        }

    }
}
