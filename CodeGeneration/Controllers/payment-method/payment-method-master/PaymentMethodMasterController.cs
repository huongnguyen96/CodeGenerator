

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MPaymentMethod;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.payment_method.payment_method_master
{
    public class PaymentMethodMasterRoute : Root
    {
        public const string FE = "/payment-method/payment-method-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class PaymentMethodMasterController : ApiController
    {
        
        
        private IPaymentMethodService PaymentMethodService;

        public PaymentMethodMasterController(
            
            IPaymentMethodService PaymentMethodService
        )
        {
            
            this.PaymentMethodService = PaymentMethodService;
        }


        [Route(PaymentMethodMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] PaymentMethodMaster_PaymentMethodFilterDTO PaymentMethodMaster_PaymentMethodFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethodFilter PaymentMethodFilter = ConvertFilterDTOToFilterEntity(PaymentMethodMaster_PaymentMethodFilterDTO);

            return await PaymentMethodService.Count(PaymentMethodFilter);
        }

        [Route(PaymentMethodMasterRoute.List), HttpPost]
        public async Task<List<PaymentMethodMaster_PaymentMethodDTO>> List([FromBody] PaymentMethodMaster_PaymentMethodFilterDTO PaymentMethodMaster_PaymentMethodFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethodFilter PaymentMethodFilter = ConvertFilterDTOToFilterEntity(PaymentMethodMaster_PaymentMethodFilterDTO);

            List<PaymentMethod> PaymentMethods = await PaymentMethodService.List(PaymentMethodFilter);

            return PaymentMethods.Select(c => new PaymentMethodMaster_PaymentMethodDTO(c)).ToList();
        }

        [Route(PaymentMethodMasterRoute.Get), HttpPost]
        public async Task<PaymentMethodMaster_PaymentMethodDTO> Get([FromBody]PaymentMethodMaster_PaymentMethodDTO PaymentMethodMaster_PaymentMethodDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PaymentMethod PaymentMethod = await PaymentMethodService.Get(PaymentMethodMaster_PaymentMethodDTO.Id);
            return new PaymentMethodMaster_PaymentMethodDTO(PaymentMethod);
        }


        public PaymentMethodFilter ConvertFilterDTOToFilterEntity(PaymentMethodMaster_PaymentMethodFilterDTO PaymentMethodMaster_PaymentMethodFilterDTO)
        {
            PaymentMethodFilter PaymentMethodFilter = new PaymentMethodFilter();
            PaymentMethodFilter.Selects = PaymentMethodSelect.ALL;
            
            PaymentMethodFilter.Id = new LongFilter{ Equal = PaymentMethodMaster_PaymentMethodFilterDTO.Id };
            PaymentMethodFilter.Code = new StringFilter{ StartsWith = PaymentMethodMaster_PaymentMethodFilterDTO.Code };
            PaymentMethodFilter.Name = new StringFilter{ StartsWith = PaymentMethodMaster_PaymentMethodFilterDTO.Name };
            PaymentMethodFilter.Description = new StringFilter{ StartsWith = PaymentMethodMaster_PaymentMethodFilterDTO.Description };
            return PaymentMethodFilter;
        }
        
        
    }
}
