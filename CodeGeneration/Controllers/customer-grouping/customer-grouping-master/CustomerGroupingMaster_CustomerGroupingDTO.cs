
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer_grouping.customer_grouping_master
{
    public class CustomerGroupingMaster_CustomerGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public CustomerGroupingMaster_CustomerGroupingDTO() {}
        public CustomerGroupingMaster_CustomerGroupingDTO(CustomerGrouping CustomerGrouping)
        {
            
            this.Id = CustomerGrouping.Id;
            this.Name = CustomerGrouping.Name;
        }
    }

    public class CustomerGroupingMaster_CustomerGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
