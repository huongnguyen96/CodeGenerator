
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.user.user_master
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
        
        public long? Id { get; set; }
        public long? ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
