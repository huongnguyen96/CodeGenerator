using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class VoucherTypeDAO
    {
        public VoucherTypeDAO()
        {
            Vouchers = new HashSet<VoucherDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual ICollection<VoucherDAO> Vouchers { get; set; }
    }
}
