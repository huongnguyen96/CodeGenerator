
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_detail
{
    public class ItemUnitOfMeasureDetail_WarehouseDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long SupplierId { get; set; }
        public ItemUnitOfMeasureDetail_WarehouseDTO() {}
        public ItemUnitOfMeasureDetail_WarehouseDTO(Warehouse Warehouse)
        {
            
            this.Id = Warehouse.Id;
            this.Name = Warehouse.Name;
            this.Phone = Warehouse.Phone;
            this.Email = Warehouse.Email;
            this.Address = Warehouse.Address;
            this.SupplierId = Warehouse.SupplierId;
        }
    }

    public class ItemUnitOfMeasureDetail_WarehouseFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? SupplierId { get; set; }
    }
}