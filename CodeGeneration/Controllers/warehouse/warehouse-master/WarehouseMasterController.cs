

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MUser;


namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMasterRoute : Root
    {
        public const string FE = "/warehouse/warehouse-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListUser="/single-list-user";
    }

    public class WarehouseMasterController : ApiController
    {
        
        
        private IUserService UserService;
        private IWarehouseService WarehouseService;

        public WarehouseMasterController(
            
            IUserService UserService,
            IWarehouseService WarehouseService
        )
        {
            
            this.UserService = UserService;
            this.WarehouseService = WarehouseService;
        }


        [Route(WarehouseMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOToFilterEntity(WarehouseMaster_WarehouseFilterDTO);

            return await WarehouseService.Count(WarehouseFilter);
        }

        [Route(WarehouseMasterRoute.List), HttpPost]
        public async Task<List<WarehouseMaster_WarehouseDTO>> List([FromBody] WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOToFilterEntity(WarehouseMaster_WarehouseFilterDTO);

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);

            return Warehouses.Select(c => new WarehouseMaster_WarehouseDTO(c)).ToList();
        }

        [Route(WarehouseMasterRoute.Get), HttpPost]
        public async Task<WarehouseMaster_WarehouseDTO> Get([FromBody]WarehouseMaster_WarehouseDTO WarehouseMaster_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = await WarehouseService.Get(WarehouseMaster_WarehouseDTO.Id);
            return new WarehouseMaster_WarehouseDTO(Warehouse);
        }


        public WarehouseFilter ConvertFilterDTOToFilterEntity(WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            
            WarehouseFilter.Id = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.Id };
            WarehouseFilter.ManagerId = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.ManagerId };
            WarehouseFilter.Code = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Code };
            WarehouseFilter.Name = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Name };
            return WarehouseFilter;
        }
        
        
        [Route(WarehouseMasterRoute.SingleListUser), HttpPost]
        public async Task<List<WarehouseMaster_UserDTO>> SingleListUser([FromBody] WarehouseMaster_UserFilterDTO WarehouseMaster_UserFilterDTO)
        {
            UserFilter UserFilter = new UserFilter();
            UserFilter.Skip = 0;
            UserFilter.Take = 10;
            UserFilter.OrderBy = UserOrder.Id;
            UserFilter.OrderType = OrderType.ASC;
            UserFilter.Selects = UserSelect.ALL;
            
            UserFilter.Id = new LongFilter{ Equal = WarehouseMaster_UserFilterDTO.Id };
            UserFilter.Username = new StringFilter{ StartsWith = WarehouseMaster_UserFilterDTO.Username };
            UserFilter.Password = new StringFilter{ StartsWith = WarehouseMaster_UserFilterDTO.Password };

            List<User> Users = await UserService.List(UserFilter);
            List<WarehouseMaster_UserDTO> WarehouseMaster_UserDTOs = Users
                .Select(x => new WarehouseMaster_UserDTO(x)).ToList();
            return WarehouseMaster_UserDTOs;
        }

    }
}
