using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EVoucherDAO
    {
        public EVoucherDAO()
        {
            EVoucherContents = new HashSet<EVoucherContentDAO>();
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long Quantity { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual ProductDAO Product { get; set; }
        public virtual ICollection<EVoucherContentDAO> EVoucherContents { get; set; }
    }
}
