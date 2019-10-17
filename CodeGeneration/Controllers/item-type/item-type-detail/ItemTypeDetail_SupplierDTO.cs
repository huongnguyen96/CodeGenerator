
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_type.item_type_detail
{
    public class ItemTypeDetail_SupplierDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public ItemTypeDetail_SupplierDTO() {}
        public ItemTypeDetail_SupplierDTO(Supplier Supplier)
        {
            
            this.Id = Supplier.Id;
            this.Name = Supplier.Name;
            this.Phone = Supplier.Phone;
            this.ContactPerson = Supplier.ContactPerson;
            this.Address = Supplier.Address;
        }
    }

    public class ItemTypeDetail_SupplierFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
    }
}
