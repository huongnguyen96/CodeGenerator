

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MPaymentMethod;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.payment_method.payment_method_detail
{
    public class PaymentMethodDetailRoute : Root
    {
        public const string FE = "/payment-method/payment-method-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class PaymentMethodDetailController : ApiController
    {
        
        
        private IPaymentMethodService PaymentMethodService;

        public PaymentMethodDetailController(
            
            IPaymentMethodService PaymentMethodService
        )
        {
            
            this.PaymentMethodService = PaymentMethodService;
        }


        [Route(PaymentMethodDetailRoute.Get), HttpPost]
        public async Task<PaymentMethodDetail_PaymentMethodDTO> Get([FromBody]PaymentMethodDetail_PaymentMethodDTO PaymentMethodDetail_PaymentMethodDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethod PaymentMethod = await PaymentMethodService.Get(PaymentMethodDetail_PaymentMethodDTO.Id);
            return new PaymentMethodDetail_PaymentMethodDTO(PaymentMethod);
        }


        [Route(PaymentMethodDetailRoute.Create), HttpPost]
        public async Task<ActionResult<PaymentMethodDetail_PaymentMethodDTO>> Create([FromBody] PaymentMethodDetail_PaymentMethodDTO PaymentMethodDetail_PaymentMethodDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethod PaymentMethod = ConvertDTOToEntity(PaymentMethodDetail_PaymentMethodDTO);

            PaymentMethod = await PaymentMethodService.Create(PaymentMethod);
            PaymentMethodDetail_PaymentMethodDTO = new PaymentMethodDetail_PaymentMethodDTO(PaymentMethod);
            if (PaymentMethod.IsValidated)
                return PaymentMethodDetail_PaymentMethodDTO;
            else
                return BadRequest(PaymentMethodDetail_PaymentMethodDTO);        
        }

        [Route(PaymentMethodDetailRoute.Update), HttpPost]
        public async Task<ActionResult<PaymentMethodDetail_PaymentMethodDTO>> Update([FromBody] PaymentMethodDetail_PaymentMethodDTO PaymentMethodDetail_PaymentMethodDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethod PaymentMethod = ConvertDTOToEntity(PaymentMethodDetail_PaymentMethodDTO);

            PaymentMethod = await PaymentMethodService.Update(PaymentMethod);
            PaymentMethodDetail_PaymentMethodDTO = new PaymentMethodDetail_PaymentMethodDTO(PaymentMethod);
            if (PaymentMethod.IsValidated)
                return PaymentMethodDetail_PaymentMethodDTO;
            else
                return BadRequest(PaymentMethodDetail_PaymentMethodDTO);        
        }

        [Route(PaymentMethodDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<PaymentMethodDetail_PaymentMethodDTO>> Delete([FromBody] PaymentMethodDetail_PaymentMethodDTO PaymentMethodDetail_PaymentMethodDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethod PaymentMethod = ConvertDTOToEntity(PaymentMethodDetail_PaymentMethodDTO);

            PaymentMethod = await PaymentMethodService.Delete(PaymentMethod);
            PaymentMethodDetail_PaymentMethodDTO = new PaymentMethodDetail_PaymentMethodDTO(PaymentMethod);
            if (PaymentMethod.IsValidated)
                return PaymentMethodDetail_PaymentMethodDTO;
            else
                return BadRequest(PaymentMethodDetail_PaymentMethodDTO);        
        }

        public PaymentMethod ConvertDTOToEntity(PaymentMethodDetail_PaymentMethodDTO PaymentMethodDetail_PaymentMethodDTO)
        {
            PaymentMethod PaymentMethod = new PaymentMethod();
            
            PaymentMethod.Id = PaymentMethodDetail_PaymentMethodDTO.Id;
            PaymentMethod.Code = PaymentMethodDetail_PaymentMethodDTO.Code;
            PaymentMethod.Name = PaymentMethodDetail_PaymentMethodDTO.Name;
            PaymentMethod.Description = PaymentMethodDetail_PaymentMethodDTO.Description;
            return PaymentMethod;
        }
        
        
    }
}
