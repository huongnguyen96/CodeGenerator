using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class MerchantDAO
    {
        public MerchantDAO()
        {
            MerchantAddresses = new HashSet<MerchantAddressDAO>();
            Products = new HashSet<ProductDAO>();
            Warehouses = new HashSet<WarehouseDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }

        public virtual ICollection<MerchantAddressDAO> MerchantAddresses { get; set; }
        public virtual ICollection<ProductDAO> Products { get; set; }
        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
    }
}
