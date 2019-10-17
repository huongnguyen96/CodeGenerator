
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.supplier.supplier_master
{
    public class SupplierMaster_ItemTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public SupplierMaster_ItemTypeDTO() {}
        public SupplierMaster_ItemTypeDTO(ItemType ItemType)
        {
            
            this.Id = ItemType.Id;
            this.Code = ItemType.Code;
            this.Name = ItemType.Name;
        }
    }

    public class SupplierMaster_ItemTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}