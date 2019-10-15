
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.warehouse.warehouse_master
{
    public class WarehouseMaster_WarehouseDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public WarehouseMaster_UserDTO Manager { get; set; }
        public WarehouseMaster_WarehouseDTO() {}
        public WarehouseMaster_WarehouseDTO(Warehouse Warehouse)
        {
            
            this.Id = Warehouse.Id;
            this.ManagerId = Warehouse.ManagerId;
            this.Code = Warehouse.Code;
            this.Name = Warehouse.Name;
            this.Manager = new WarehouseMaster_UserDTO(Warehouse.Manager);

        }
    }

    public class WarehouseMaster_WarehouseFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
