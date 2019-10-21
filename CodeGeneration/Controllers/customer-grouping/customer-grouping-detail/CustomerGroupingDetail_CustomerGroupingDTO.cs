
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer_grouping.customer_grouping_detail
{
    public class CustomerGroupingDetail_CustomerGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public CustomerGroupingDetail_CustomerGroupingDTO() {}
        public CustomerGroupingDetail_CustomerGroupingDTO(CustomerGrouping CustomerGrouping)
        {
            
            this.Id = CustomerGrouping.Id;
            this.Name = CustomerGrouping.Name;
        }
    }

    public class CustomerGroupingDetail_CustomerGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
