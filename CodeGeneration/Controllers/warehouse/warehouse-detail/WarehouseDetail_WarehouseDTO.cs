
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
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long PartnerId { get; set; }
        public WarehouseDetail_WarehouseDTO() {}
        public WarehouseDetail_WarehouseDTO(Warehouse Warehouse)
        {
            
            this.Id = Warehouse.Id;
            this.Name = Warehouse.Name;
            this.Phone = Warehouse.Phone;
            this.Email = Warehouse.Email;
            this.Address = Warehouse.Address;
            this.PartnerId = Warehouse.PartnerId;
        }
    }

    public class WarehouseDetail_WarehouseFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? PartnerId { get; set; }
        public WarehouseOrder OrderBy { get; set; }
    }
}
