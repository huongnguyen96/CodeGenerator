using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            Districts = new HashSet<DistrictDAO>();
            ShippingAddresses = new HashSet<ShippingAddressDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }

        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<ShippingAddressDAO> ShippingAddresses { get; set; }
    }
}
