
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.supplier.supplier_master
{
    public class SupplierMaster_SupplierDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public SupplierMaster_SupplierDTO() {}
        public SupplierMaster_SupplierDTO(Supplier Supplier)
        {
            
            this.Id = Supplier.Id;
            this.Name = Supplier.Name;
            this.Phone = Supplier.Phone;
            this.ContactPerson = Supplier.ContactPerson;
            this.Address = Supplier.Address;
        }
    }

    public class SupplierMaster_SupplierFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
    }
}
