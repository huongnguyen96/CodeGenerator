
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.payment_method.payment_method_detail
{
    public class PaymentMethodDetail_PaymentMethodDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PaymentMethodDetail_PaymentMethodDTO() {}
        public PaymentMethodDetail_PaymentMethodDTO(PaymentMethod PaymentMethod)
        {
            
            this.Id = PaymentMethod.Id;
            this.Code = PaymentMethod.Code;
            this.Name = PaymentMethod.Name;
            this.Description = PaymentMethod.Description;
        }
    }

    public class PaymentMethodDetail_PaymentMethodFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PaymentMethodOrder OrderBy { get; set; }
    }
}
