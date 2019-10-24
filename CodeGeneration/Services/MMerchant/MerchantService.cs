
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MMerchant
{
    public interface IMerchantService : IServiceScoped
    {
        Task<int> Count(MerchantFilter MerchantFilter);
        Task<List<Merchant>> List(MerchantFilter MerchantFilter);
        Task<Merchant> Get(long Id);
        Task<Merchant> Create(Merchant Merchant);
        Task<Merchant> Update(Merchant Merchant);
        Task<Merchant> Delete(Merchant Merchant);
    }

    public class MerchantService : IMerchantService
    {
        public IUOW UOW;
        public IMerchantValidator MerchantValidator;

        public MerchantService(
            IUOW UOW, 
            IMerchantValidator MerchantValidator
        )
        {
            this.UOW = UOW;
            this.MerchantValidator = MerchantValidator;
        }
        public async Task<int> Count(MerchantFilter MerchantFilter)
        {
            int result = await UOW.MerchantRepository.Count(MerchantFilter);
            return result;
        }

        public async Task<List<Merchant>> List(MerchantFilter MerchantFilter)
        {
            List<Merchant> Merchants = await UOW.MerchantRepository.List(MerchantFilter);
            return Merchants;
        }

        public async Task<Merchant> Get(long Id)
        {
            Merchant Merchant = await UOW.MerchantRepository.Get(Id);
            if (Merchant == null)
                return null;
            return Merchant;
        }

        public async Task<Merchant> Create(Merchant Merchant)
        {
            if (!await MerchantValidator.Create(Merchant))
                return Merchant;

            try
            {
               
                await UOW.Begin();
                await UOW.MerchantRepository.Create(Merchant);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Merchant, "", nameof(MerchantService));
                return await UOW.MerchantRepository.Get(Merchant.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(MerchantService));
                throw new MessageException(ex);
            }
        }

        public async Task<Merchant> Update(Merchant Merchant)
        {
            if (!await MerchantValidator.Update(Merchant))
                return Merchant;
            try
            {
                var oldData = await UOW.MerchantRepository.Get(Merchant.Id);

                await UOW.Begin();
                await UOW.MerchantRepository.Update(Merchant);
                await UOW.Commit();

                var newData = await UOW.MerchantRepository.Get(Merchant.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(MerchantService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(MerchantService));
                throw new MessageException(ex);
            }
        }

        public async Task<Merchant> Delete(Merchant Merchant)
        {
            if (!await MerchantValidator.Delete(Merchant))
                return Merchant;

            try
            {
                await UOW.Begin();
                await UOW.MerchantRepository.Delete(Merchant);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Merchant, nameof(MerchantService));
                return Merchant;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(MerchantService));
                throw new MessageException(ex);
            }
        }
    }
}
