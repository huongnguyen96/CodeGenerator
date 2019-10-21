using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DistrictDAO
    {
        public DistrictDAO()
        {
            ShippingAddresses = new HashSet<ShippingAddressDAO>();
            Wards = new HashSet<WardDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<ShippingAddressDAO> ShippingAddresses { get; set; }
        public virtual ICollection<WardDAO> Wards { get; set; }
    }
}
