using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class PaymentMethodDAO
    {
        public PaymentMethodDAO()
        {
            Product_PaymentMethods = new HashSet<Product_PaymentMethodDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product_PaymentMethodDAO> Product_PaymentMethods { get; set; }
    }
}
