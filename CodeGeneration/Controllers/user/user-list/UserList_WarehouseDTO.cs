
using WeGift.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace WeGift.Controllers.user.user_list
{
    public class UserList_WarehouseDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public User Manager { get; set; }
        public UserList_WarehouseDTO() {}
        public UserList_WarehouseDTO(Warehouse Warehouse)
        {
            
            this.Id = Warehouse.Id;
            this.ManagerId = Warehouse.ManagerId;
            this.Code = Warehouse.Code;
            this.Name = Warehouse.Name;
            this.Manager = new WarehouseList_UserDTO(Warehouse.Manager);

        }
    }

    public class UserList_WarehouseFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ManagerId { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
    }
}
