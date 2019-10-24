using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Product_MerchantAddressDAO
    {
        public long ProductId { get; set; }
        public long MerchantAddressId { get; set; }

        public virtual MerchantAddressDAO MerchantAddress { get; set; }
        public virtual ProductDAO Product { get; set; }
    }
}
