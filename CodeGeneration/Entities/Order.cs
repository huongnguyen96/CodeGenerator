
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Order : DataEntity
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long Total { get; set; }
        public long VoucherDiscount { get; set; }
        public long CampaignDiscount { get; set; }
        public Customer Customer { get; set; }
        public List<OrderContent> OrderContents { get; set; }
    }

    public class OrderFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter CustomerId { get; set; }
        public DateTimeFilter CreatedDate { get; set; }
        public StringFilter VoucherCode { get; set; }
        public LongFilter Total { get; set; }
        public LongFilter VoucherDiscount { get; set; }
        public LongFilter CampaignDiscount { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public OrderOrder OrderBy {get; set;}
        public OrderSelect Selects {get; set;}
    }

    public enum OrderOrder
    {
        
        Id = 1,
        Customer = 2,
        CreatedDate = 3,
        VoucherCode = 4,
        Total = 5,
        VoucherDiscount = 6,
        CampaignDiscount = 7,
    }

    public enum OrderSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Customer = E._2,
        CreatedDate = E._3,
        VoucherCode = E._4,
        Total = E._5,
        VoucherDiscount = E._6,
        CampaignDiscount = E._7,
    }
}
