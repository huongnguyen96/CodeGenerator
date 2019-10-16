
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetail_WarehouseDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public WarehouseDetail_UserDTO Manager { get; set; }
        public WarehouseDetail_WarehouseDTO() {}
        public WarehouseDetail_WarehouseDTO(Warehouse Warehouse)
        {
            
            this.Id = Warehouse.Id;
            this.ManagerId = Warehouse.ManagerId;
            this.Code = Warehouse.Code;
            this.Name = Warehouse.Name;
            this.Manager = new WarehouseDetail_UserDTO(Warehouse.Manager);

        }
    }

    public class WarehouseDetail_WarehouseFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ManagerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
