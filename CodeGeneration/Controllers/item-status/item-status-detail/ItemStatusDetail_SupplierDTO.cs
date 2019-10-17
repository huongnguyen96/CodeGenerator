
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_status.item_status_detail
{
    public class ItemStatusDetail_SupplierDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public ItemStatusDetail_SupplierDTO() {}
        public ItemStatusDetail_SupplierDTO(Supplier Supplier)
        {
            
            this.Id = Supplier.Id;
            this.Name = Supplier.Name;
            this.Phone = Supplier.Phone;
            this.ContactPerson = Supplier.ContactPerson;
            this.Address = Supplier.Address;
        }
    }

    public class ItemStatusDetail_SupplierFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
    }
}
