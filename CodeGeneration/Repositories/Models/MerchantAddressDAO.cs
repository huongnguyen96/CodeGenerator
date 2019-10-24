using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class MerchantAddressDAO
    {
        public MerchantAddressDAO()
        {
            Product_MerchantAddresses = new HashSet<Product_MerchantAddressDAO>();
        }

        public long Id { get; set; }
        public long MerchantId { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }

        public virtual MerchantDAO Merchant { get; set; }
        public virtual ICollection<Product_MerchantAddressDAO> Product_MerchantAddresses { get; set; }
    }
}
