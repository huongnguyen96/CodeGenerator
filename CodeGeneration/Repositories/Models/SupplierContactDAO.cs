using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class SupplierContactDAO
    {
        public Guid Id { get; set; }
        public Guid SupplierDetailId { get; set; }
        public long CX { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid? ProvinceId { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual SupplierDetailDAO SupplierDetail { get; set; }
    }
}
