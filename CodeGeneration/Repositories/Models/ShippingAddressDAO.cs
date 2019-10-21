using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ShippingAddressDAO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual WardDAO Ward { get; set; }
    }
}
