
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_customer_grouping.discount_customer_grouping_master
{
    public class DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
        public DiscountCustomerGroupingMaster_DiscountDTO Discount { get; set; }
        public DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO() {}
        public DiscountCustomerGroupingMaster_DiscountCustomerGroupingDTO(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            
            this.Id = DiscountCustomerGrouping.Id;
            this.DiscountId = DiscountCustomerGrouping.DiscountId;
            this.CustomerGroupingCode = DiscountCustomerGrouping.CustomerGroupingCode;
            this.Discount = new DiscountCustomerGroupingMaster_DiscountDTO(DiscountCustomerGrouping.Discount);

        }
    }

    public class DiscountCustomerGroupingMaster_DiscountCustomerGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
    }
}
