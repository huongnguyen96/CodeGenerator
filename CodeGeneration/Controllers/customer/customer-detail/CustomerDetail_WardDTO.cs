
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer.customer_detail
{
    public class CustomerDetail_WardDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long DistrictId { get; set; }
        public CustomerDetail_WardDTO() {}
        public CustomerDetail_WardDTO(Ward Ward)
        {
            
            this.Id = Ward.Id;
            this.Name = Ward.Name;
            this.OrderNumber = Ward.OrderNumber;
            this.DistrictId = Ward.DistrictId;
        }
    }

    public class CustomerDetail_WardFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public long? DistrictId { get; set; }
        public WardOrder OrderBy { get; set; }
    }
}
