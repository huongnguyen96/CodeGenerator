
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MProvince
{
    public interface IProvinceService : IServiceScoped
    {
        Task<int> Count(ProvinceFilter ProvinceFilter);
        Task<List<Province>> List(ProvinceFilter ProvinceFilter);
        Task<Province> Get(long Id);
        Task<Province> Create(Province Province);
        Task<Province> Update(Province Province);
        Task<Province> Delete(Province Province);
    }

    public class ProvinceService : IProvinceService
    {
        public IUOW UOW;
        public IProvinceValidator ProvinceValidator;

        public ProvinceService(
            IUOW UOW, 
            IProvinceValidator ProvinceValidator
        )
        {
            this.UOW = UOW;
            this.ProvinceValidator = ProvinceValidator;
        }
        public async Task<int> Count(ProvinceFilter ProvinceFilter)
        {
            int result = await UOW.ProvinceRepository.Count(ProvinceFilter);
            return result;
        }

        public async Task<List<Province>> List(ProvinceFilter ProvinceFilter)
        {
            List<Province> Provinces = await UOW.ProvinceRepository.List(ProvinceFilter);
            return Provinces;
        }

        public async Task<Province> Get(long Id)
        {
            Province Province = await UOW.ProvinceRepository.Get(Id);
            if (Province == null)
                return null;
            return Province;
        }

        public async Task<Province> Create(Province Province)
        {
            if (!await ProvinceValidator.Create(Province))
                return Province;

            try
            {
               
                await UOW.Begin();
                await UOW.ProvinceRepository.Create(Province);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Province, "", nameof(ProvinceService));
                return await UOW.ProvinceRepository.Get(Province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProvinceService));
                throw new MessageException(ex);
            }
        }

        public async Task<Province> Update(Province Province)
        {
            if (!await ProvinceValidator.Update(Province))
                return Province;
            try
            {
                var oldData = await UOW.ProvinceRepository.Get(Province.Id);

                await UOW.Begin();
                await UOW.ProvinceRepository.Update(Province);
                await UOW.Commit();

                var newData = await UOW.ProvinceRepository.Get(Province.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ProvinceService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProvinceService));
                throw new MessageException(ex);
            }
        }

        public async Task<Province> Delete(Province Province)
        {
            if (!await ProvinceValidator.Delete(Province))
                return Province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Delete(Province);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Province, nameof(ProvinceService));
                return Province;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ProvinceService));
                throw new MessageException(ex);
            }
        }
    }
}
