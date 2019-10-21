
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MDiscount
{
    public interface IDiscountService : IServiceScoped
    {
        Task<int> Count(DiscountFilter DiscountFilter);
        Task<List<Discount>> List(DiscountFilter DiscountFilter);
        Task<Discount> Get(long Id);
        Task<Discount> Create(Discount Discount);
        Task<Discount> Update(Discount Discount);
        Task<Discount> Delete(Discount Discount);
    }

    public class DiscountService : IDiscountService
    {
        public IUOW UOW;
        public IDiscountValidator DiscountValidator;

        public DiscountService(
            IUOW UOW, 
            IDiscountValidator DiscountValidator
        )
        {
            this.UOW = UOW;
            this.DiscountValidator = DiscountValidator;
        }
        public async Task<int> Count(DiscountFilter DiscountFilter)
        {
            int result = await UOW.DiscountRepository.Count(DiscountFilter);
            return result;
        }

        public async Task<List<Discount>> List(DiscountFilter DiscountFilter)
        {
            List<Discount> Discounts = await UOW.DiscountRepository.List(DiscountFilter);
            return Discounts;
        }

        public async Task<Discount> Get(long Id)
        {
            Discount Discount = await UOW.DiscountRepository.Get(Id);
            if (Discount == null)
                return null;
            return Discount;
        }

        public async Task<Discount> Create(Discount Discount)
        {
            if (!await DiscountValidator.Create(Discount))
                return Discount;

            try
            {
               
                await UOW.Begin();
                await UOW.DiscountRepository.Create(Discount);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Discount, "", nameof(DiscountService));
                return await UOW.DiscountRepository.Get(Discount.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountService));
                throw new MessageException(ex);
            }
        }

        public async Task<Discount> Update(Discount Discount)
        {
            if (!await DiscountValidator.Update(Discount))
                return Discount;
            try
            {
                var oldData = await UOW.DiscountRepository.Get(Discount.Id);

                await UOW.Begin();
                await UOW.DiscountRepository.Update(Discount);
                await UOW.Commit();

                var newData = await UOW.DiscountRepository.Get(Discount.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(DiscountService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountService));
                throw new MessageException(ex);
            }
        }

        public async Task<Discount> Delete(Discount Discount)
        {
            if (!await DiscountValidator.Delete(Discount))
                return Discount;

            try
            {
                await UOW.Begin();
                await UOW.DiscountRepository.Delete(Discount);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Discount, nameof(DiscountService));
                return Discount;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountService));
                throw new MessageException(ex);
            }
        }
    }
}
