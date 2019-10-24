
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MMerchantAddress
{
    public interface IMerchantAddressService : IServiceScoped
    {
        Task<int> Count(MerchantAddressFilter MerchantAddressFilter);
        Task<List<MerchantAddress>> List(MerchantAddressFilter MerchantAddressFilter);
        Task<MerchantAddress> Get(long Id);
        Task<MerchantAddress> Create(MerchantAddress MerchantAddress);
        Task<MerchantAddress> Update(MerchantAddress MerchantAddress);
        Task<MerchantAddress> Delete(MerchantAddress MerchantAddress);
    }

    public class MerchantAddressService : IMerchantAddressService
    {
        public IUOW UOW;
        public IMerchantAddressValidator MerchantAddressValidator;

        public MerchantAddressService(
            IUOW UOW, 
            IMerchantAddressValidator MerchantAddressValidator
        )
        {
            this.UOW = UOW;
            this.MerchantAddressValidator = MerchantAddressValidator;
        }
        public async Task<int> Count(MerchantAddressFilter MerchantAddressFilter)
        {
            int result = await UOW.MerchantAddressRepository.Count(MerchantAddressFilter);
            return result;
        }

        public async Task<List<MerchantAddress>> List(MerchantAddressFilter MerchantAddressFilter)
        {
            List<MerchantAddress> MerchantAddresss = await UOW.MerchantAddressRepository.List(MerchantAddressFilter);
            return MerchantAddresss;
        }

        public async Task<MerchantAddress> Get(long Id)
        {
            MerchantAddress MerchantAddress = await UOW.MerchantAddressRepository.Get(Id);
            if (MerchantAddress == null)
                return null;
            return MerchantAddress;
        }

        public async Task<MerchantAddress> Create(MerchantAddress MerchantAddress)
        {
            if (!await MerchantAddressValidator.Create(MerchantAddress))
                return MerchantAddress;

            try
            {
               
                await UOW.Begin();
                await UOW.MerchantAddressRepository.Create(MerchantAddress);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(MerchantAddress, "", nameof(MerchantAddressService));
                return await UOW.MerchantAddressRepository.Get(MerchantAddress.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(MerchantAddressService));
                throw new MessageException(ex);
            }
        }

        public async Task<MerchantAddress> Update(MerchantAddress MerchantAddress)
        {
            if (!await MerchantAddressValidator.Update(MerchantAddress))
                return MerchantAddress;
            try
            {
                var oldData = await UOW.MerchantAddressRepository.Get(MerchantAddress.Id);

                await UOW.Begin();
                await UOW.MerchantAddressRepository.Update(MerchantAddress);
                await UOW.Commit();

                var newData = await UOW.MerchantAddressRepository.Get(MerchantAddress.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(MerchantAddressService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(MerchantAddressService));
                throw new MessageException(ex);
            }
        }

        public async Task<MerchantAddress> Delete(MerchantAddress MerchantAddress)
        {
            if (!await MerchantAddressValidator.Delete(MerchantAddress))
                return MerchantAddress;

            try
            {
                await UOW.Begin();
                await UOW.MerchantAddressRepository.Delete(MerchantAddress);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", MerchantAddress, nameof(MerchantAddressService));
                return MerchantAddress;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(MerchantAddressService));
                throw new MessageException(ex);
            }
        }
    }
}
