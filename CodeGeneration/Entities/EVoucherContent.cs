
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class EVoucherContent : DataEntity
    {
        
        public long Id { get; set; }
        public long EVourcherId { get; set; }
        public string UsedCode { get; set; }
        public string MerchantCode { get; set; }
        public DateTime? UsedDate { get; set; }
        public EVoucher EVourcher { get; set; }
    }

    public class EVoucherContentFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter EVourcherId { get; set; }
        public StringFilter UsedCode { get; set; }
        public StringFilter MerchantCode { get; set; }
        public DateTimeFilter UsedDate { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public EVoucherContentOrder OrderBy {get; set;}
        public EVoucherContentSelect Selects {get; set;}
    }

    public enum EVoucherContentOrder
    {
        
        Id = 1,
        EVourcher = 2,
        UsedCode = 3,
        MerchantCode = 4,
        UsedDate = 5,
    }
    
    [Flags]
    public enum EVoucherContentSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        EVourcher = E._2,
        UsedCode = E._3,
        MerchantCode = E._4,
        UsedDate = E._5,
    }
}
