
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_customer_grouping.discount_customer_grouping_detail
{
    public class DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
        public DiscountCustomerGroupingDetail_DiscountDTO Discount { get; set; }
        public DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO() {}
        public DiscountCustomerGroupingDetail_DiscountCustomerGroupingDTO(DiscountCustomerGrouping DiscountCustomerGrouping)
        {
            
            this.Id = DiscountCustomerGrouping.Id;
            this.DiscountId = DiscountCustomerGrouping.DiscountId;
            this.CustomerGroupingCode = DiscountCustomerGrouping.CustomerGroupingCode;
            this.Discount = new DiscountCustomerGroupingDetail_DiscountDTO(DiscountCustomerGrouping.Discount);

        }
    }

    public class DiscountCustomerGroupingDetail_DiscountCustomerGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
    }
}
