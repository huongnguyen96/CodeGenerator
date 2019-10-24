using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class OrderDAO
    {
        public OrderDAO()
        {
            OrderContents = new HashSet<OrderContentDAO>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VoucherCode { get; set; }
        public long Total { get; set; }
        public long VoucherDiscount { get; set; }
        public long CampaignDiscount { get; set; }
        public long StatusId { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual OrderStatusDAO Status { get; set; }
        public virtual ICollection<OrderContentDAO> OrderContents { get; set; }
    }
}
