
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MDistrict
{
    public interface IDistrictService : IServiceScoped
    {
        Task<int> Count(DistrictFilter DistrictFilter);
        Task<List<District>> List(DistrictFilter DistrictFilter);
        Task<District> Get(long Id);
        Task<District> Create(District District);
        Task<District> Update(District District);
        Task<District> Delete(District District);
    }

    public class DistrictService : IDistrictService
    {
        public IUOW UOW;
        public IDistrictValidator DistrictValidator;

        public DistrictService(
            IUOW UOW, 
            IDistrictValidator DistrictValidator
        )
        {
            this.UOW = UOW;
            this.DistrictValidator = DistrictValidator;
        }
        public async Task<int> Count(DistrictFilter DistrictFilter)
        {
            int result = await UOW.DistrictRepository.Count(DistrictFilter);
            return result;
        }

        public async Task<List<District>> List(DistrictFilter DistrictFilter)
        {
            List<District> Districts = await UOW.DistrictRepository.List(DistrictFilter);
            return Districts;
        }

        public async Task<District> Get(long Id)
        {
            District District = await UOW.DistrictRepository.Get(Id);
            if (District == null)
                return null;
            return District;
        }

        public async Task<District> Create(District District)
        {
            if (!await DistrictValidator.Create(District))
                return District;

            try
            {
               
                await UOW.Begin();
                await UOW.DistrictRepository.Create(District);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(District, "", nameof(DistrictService));
                return await UOW.DistrictRepository.Get(District.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DistrictService));
                throw new MessageException(ex);
            }
        }

        public async Task<District> Update(District District)
        {
            if (!await DistrictValidator.Update(District))
                return District;
            try
            {
                var oldData = await UOW.DistrictRepository.Get(District.Id);

                await UOW.Begin();
                await UOW.DistrictRepository.Update(District);
                await UOW.Commit();

                var newData = await UOW.DistrictRepository.Get(District.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(DistrictService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DistrictService));
                throw new MessageException(ex);
            }
        }

        public async Task<District> Delete(District District)
        {
            if (!await DistrictValidator.Delete(District))
                return District;

            try
            {
                await UOW.Begin();
                await UOW.DistrictRepository.Delete(District);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", District, nameof(DistrictService));
                return District;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DistrictService));
                throw new MessageException(ex);
            }
        }
    }
}
