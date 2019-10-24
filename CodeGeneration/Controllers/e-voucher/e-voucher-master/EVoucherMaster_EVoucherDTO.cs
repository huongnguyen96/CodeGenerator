
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.e_voucher.e_voucher_master
{
    public class EVoucherMaster_EVoucherDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long Quantity { get; set; }
        public EVoucherMaster_EVoucherDTO() {}
        public EVoucherMaster_EVoucherDTO(EVoucher EVoucher)
        {
            
            this.Id = EVoucher.Id;
            this.CustomerId = EVoucher.CustomerId;
            this.ProductId = EVoucher.ProductId;
            this.Name = EVoucher.Name;
            this.Start = EVoucher.Start;
            this.End = EVoucher.End;
            this.Quantity = EVoucher.Quantity;
        }
    }

    public class EVoucherMaster_EVoucherFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public long? Quantity { get; set; }
        public EVoucherOrder OrderBy { get; set; }
    }
}
