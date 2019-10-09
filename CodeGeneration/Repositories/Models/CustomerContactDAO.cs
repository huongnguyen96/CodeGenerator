using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerContactDAO
    {
        public Guid Id { get; set; }
        public Guid CustomerDetailId { get; set; }
        public long CX { get; set; }
        public Guid? ProvinceId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual CustomerDetailDAO CustomerDetail { get; set; }
        public virtual ProvinceDAO Province { get; set; }
    }
}
