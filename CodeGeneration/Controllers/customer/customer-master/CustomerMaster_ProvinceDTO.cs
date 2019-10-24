
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer.customer_master
{
    public class CustomerMaster_ProvinceDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public CustomerMaster_ProvinceDTO() {}
        public CustomerMaster_ProvinceDTO(Province Province)
        {
            
            this.Id = Province.Id;
            this.Name = Province.Name;
            this.OrderNumber = Province.OrderNumber;
        }
    }

    public class CustomerMaster_ProvinceFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public ProvinceOrder OrderBy { get; set; }
    }
}
