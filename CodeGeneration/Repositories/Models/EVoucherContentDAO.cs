using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EVoucherContentDAO
    {
        public long Id { get; set; }
        public long EVourcherId { get; set; }
        public string UsedCode { get; set; }
        public string MerchantCode { get; set; }
        public DateTime? UsedDate { get; set; }

        public virtual EVoucherDAO EVourcher { get; set; }
    }
}
