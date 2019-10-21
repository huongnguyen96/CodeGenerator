using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class WardDAO
    {
        public WardDAO()
        {
            ShippingAddresses = new HashSet<ShippingAddressDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long DistrictId { get; set; }

        public virtual DistrictDAO District { get; set; }
        public virtual ICollection<ShippingAddressDAO> ShippingAddresses { get; set; }
    }
}
