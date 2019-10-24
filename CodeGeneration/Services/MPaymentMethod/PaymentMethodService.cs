
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MPaymentMethod
{
    public interface IPaymentMethodService : IServiceScoped
    {
        Task<int> Count(PaymentMethodFilter PaymentMethodFilter);
        Task<List<PaymentMethod>> List(PaymentMethodFilter PaymentMethodFilter);
        Task<PaymentMethod> Get(long Id);
        Task<PaymentMethod> Create(PaymentMethod PaymentMethod);
        Task<PaymentMethod> Update(PaymentMethod PaymentMethod);
        Task<PaymentMethod> Delete(PaymentMethod PaymentMethod);
    }

    public class PaymentMethodService : IPaymentMethodService
    {
        public IUOW UOW;
        public IPaymentMethodValidator PaymentMethodValidator;

        public PaymentMethodService(
            IUOW UOW, 
            IPaymentMethodValidator PaymentMethodValidator
        )
        {
            this.UOW = UOW;
            this.PaymentMethodValidator = PaymentMethodValidator;
        }
        public async Task<int> Count(PaymentMethodFilter PaymentMethodFilter)
        {
            int result = await UOW.PaymentMethodRepository.Count(PaymentMethodFilter);
            return result;
        }

        public async Task<List<PaymentMethod>> List(PaymentMethodFilter PaymentMethodFilter)
        {
            List<PaymentMethod> PaymentMethods = await UOW.PaymentMethodRepository.List(PaymentMethodFilter);
            return PaymentMethods;
        }

        public async Task<PaymentMethod> Get(long Id)
        {
            PaymentMethod PaymentMethod = await UOW.PaymentMethodRepository.Get(Id);
            if (PaymentMethod == null)
                return null;
            return PaymentMethod;
        }

        public async Task<PaymentMethod> Create(PaymentMethod PaymentMethod)
        {
            if (!await PaymentMethodValidator.Create(PaymentMethod))
                return PaymentMethod;

            try
            {
               
                await UOW.Begin();
                await UOW.PaymentMethodRepository.Create(PaymentMethod);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(PaymentMethod, "", nameof(PaymentMethodService));
                return await UOW.PaymentMethodRepository.Get(PaymentMethod.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(PaymentMethodService));
                throw new MessageException(ex);
            }
        }

        public async Task<PaymentMethod> Update(PaymentMethod PaymentMethod)
        {
            if (!await PaymentMethodValidator.Update(PaymentMethod))
                return PaymentMethod;
            try
            {
                var oldData = await UOW.PaymentMethodRepository.Get(PaymentMethod.Id);

                await UOW.Begin();
                await UOW.PaymentMethodRepository.Update(PaymentMethod);
                await UOW.Commit();

                var newData = await UOW.PaymentMethodRepository.Get(PaymentMethod.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(PaymentMethodService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(PaymentMethodService));
                throw new MessageException(ex);
            }
        }

        public async Task<PaymentMethod> Delete(PaymentMethod PaymentMethod)
        {
            if (!await PaymentMethodValidator.Delete(PaymentMethod))
                return PaymentMethod;

            try
            {
                await UOW.Begin();
                await UOW.PaymentMethodRepository.Delete(PaymentMethod);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", PaymentMethod, nameof(PaymentMethodService));
                return PaymentMethod;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(PaymentMethodService));
                throw new MessageException(ex);
            }
        }
    }
}
