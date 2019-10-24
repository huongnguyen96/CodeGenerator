
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.e_voucher.e_voucher_master
{
    public class EVoucherMaster_EVoucherContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long EVourcherId { get; set; }
        public string UsedCode { get; set; }
        public string MerchantCode { get; set; }
        public DateTime? UsedDate { get; set; }
        public EVoucherMaster_EVoucherContentDTO() {}
        public EVoucherMaster_EVoucherContentDTO(EVoucherContent EVoucherContent)
        {
            
            this.Id = EVoucherContent.Id;
            this.EVourcherId = EVoucherContent.EVourcherId;
            this.UsedCode = EVoucherContent.UsedCode;
            this.MerchantCode = EVoucherContent.MerchantCode;
            this.UsedDate = EVoucherContent.UsedDate;
        }
    }

    public class EVoucherMaster_EVoucherContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? EVourcherId { get; set; }
        public string UsedCode { get; set; }
        public string MerchantCode { get; set; }
        public DateTime? UsedDate { get; set; }
        public EVoucherContentOrder OrderBy { get; set; }
    }
}
