
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MShippingAddress
{
    public interface IShippingAddressService : IServiceScoped
    {
        Task<int> Count(ShippingAddressFilter ShippingAddressFilter);
        Task<List<ShippingAddress>> List(ShippingAddressFilter ShippingAddressFilter);
        Task<ShippingAddress> Get(long Id);
        Task<ShippingAddress> Create(ShippingAddress ShippingAddress);
        Task<ShippingAddress> Update(ShippingAddress ShippingAddress);
        Task<ShippingAddress> Delete(ShippingAddress ShippingAddress);
    }

    public class ShippingAddressService : IShippingAddressService
    {
        public IUOW UOW;
        public IShippingAddressValidator ShippingAddressValidator;

        public ShippingAddressService(
            IUOW UOW, 
            IShippingAddressValidator ShippingAddressValidator
        )
        {
            this.UOW = UOW;
            this.ShippingAddressValidator = ShippingAddressValidator;
        }
        public async Task<int> Count(ShippingAddressFilter ShippingAddressFilter)
        {
            int result = await UOW.ShippingAddressRepository.Count(ShippingAddressFilter);
            return result;
        }

        public async Task<List<ShippingAddress>> List(ShippingAddressFilter ShippingAddressFilter)
        {
            List<ShippingAddress> ShippingAddresss = await UOW.ShippingAddressRepository.List(ShippingAddressFilter);
            return ShippingAddresss;
        }

        public async Task<ShippingAddress> Get(long Id)
        {
            ShippingAddress ShippingAddress = await UOW.ShippingAddressRepository.Get(Id);
            if (ShippingAddress == null)
                return null;
            return ShippingAddress;
        }

        public async Task<ShippingAddress> Create(ShippingAddress ShippingAddress)
        {
            if (!await ShippingAddressValidator.Create(ShippingAddress))
                return ShippingAddress;

            try
            {
               
                await UOW.Begin();
                await UOW.ShippingAddressRepository.Create(ShippingAddress);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ShippingAddress, "", nameof(ShippingAddressService));
                return await UOW.ShippingAddressRepository.Get(ShippingAddress.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ShippingAddressService));
                throw new MessageException(ex);
            }
        }

        public async Task<ShippingAddress> Update(ShippingAddress ShippingAddress)
        {
            if (!await ShippingAddressValidator.Update(ShippingAddress))
                return ShippingAddress;
            try
            {
                var oldData = await UOW.ShippingAddressRepository.Get(ShippingAddress.Id);

                await UOW.Begin();
                await UOW.ShippingAddressRepository.Update(ShippingAddress);
                await UOW.Commit();

                var newData = await UOW.ShippingAddressRepository.Get(ShippingAddress.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ShippingAddressService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ShippingAddressService));
                throw new MessageException(ex);
            }
        }

        public async Task<ShippingAddress> Delete(ShippingAddress ShippingAddress)
        {
            if (!await ShippingAddressValidator.Delete(ShippingAddress))
                return ShippingAddress;

            try
            {
                await UOW.Begin();
                await UOW.ShippingAddressRepository.Delete(ShippingAddress);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ShippingAddress, nameof(ShippingAddressService));
                return ShippingAddress;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ShippingAddressService));
                throw new MessageException(ex);
            }
        }
    }
}
