using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Product_PaymentMethodDAO
    {
        public long ProductId { get; set; }
        public long PaymentMethodId { get; set; }

        public virtual PaymentMethodDAO PaymentMethod { get; set; }
        public virtual ProductDAO Product { get; set; }
    }
}
