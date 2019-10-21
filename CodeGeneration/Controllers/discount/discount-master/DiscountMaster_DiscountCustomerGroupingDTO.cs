
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount.discount_master
{
    public class DiscountMaster_DiscountCustomerGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
        public DiscountMaster_DiscountCustomerGroupingDTO() {}
        public DiscountMaster_DiscountCustomerGroupingDTO(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            
            this.Id = DiscountCustomerGrouping.Id;
            this.DiscountId = DiscountCustomerGrouping.DiscountId;
            this.CustomerGroupingCode = DiscountCustomerGrouping.CustomerGroupingCode;
        }
    }

    public class DiscountMaster_DiscountCustomerGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
    }
}
