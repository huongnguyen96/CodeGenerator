
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.e_voucher_content.e_voucher_content_detail
{
    public class EVoucherContentDetail_EVoucherContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long EVourcherId { get; set; }
        public string UsedCode { get; set; }
        public string MerchantCode { get; set; }
        public DateTime? UsedDate { get; set; }
        public EVoucherContentDetail_EVoucherDTO EVourcher { get; set; }
        public EVoucherContentDetail_EVoucherContentDTO() {}
        public EVoucherContentDetail_EVoucherContentDTO(EVoucherContent EVoucherContent)
        {
            
            this.Id = EVoucherContent.Id;
            this.EVourcherId = EVoucherContent.EVourcherId;
            this.UsedCode = EVoucherContent.UsedCode;
            this.MerchantCode = EVoucherContent.MerchantCode;
            this.UsedDate = EVoucherContent.UsedDate;
            this.EVourcher = new EVoucherContentDetail_EVoucherDTO(EVoucherContent.EVourcher);

        }
    }

    public class EVoucherContentDetail_EVoucherContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? EVourcherId { get; set; }
        public string UsedCode { get; set; }
        public string MerchantCode { get; set; }
        public DateTime? UsedDate { get; set; }
        public EVoucherContentOrder OrderBy { get; set; }
    }
}
