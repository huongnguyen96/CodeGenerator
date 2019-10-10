
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.user.user_master
{
    public class UserMaster_WarehouseDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public UserMaster_WarehouseDTO() {}
        public UserMaster_WarehouseDTO(Warehouse Warehouse)
        {
            
            this.Id = Warehouse.Id;
            this.ManagerId = Warehouse.ManagerId;
            this.Code = Warehouse.Code;
            this.Name = Warehouse.Name;
        }
    }

    public class UserMaster_WarehouseFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ManagerId { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
    }
}
