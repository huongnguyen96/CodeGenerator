
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MBrand
{
    public interface IBrandService : IServiceScoped
    {
        Task<int> Count(BrandFilter BrandFilter);
        Task<List<Brand>> List(BrandFilter BrandFilter);
        Task<Brand> Get(long Id);
        Task<Brand> Create(Brand Brand);
        Task<Brand> Update(Brand Brand);
        Task<Brand> Delete(Brand Brand);
    }

    public class BrandService : IBrandService
    {
        public IUOW UOW;
        public IBrandValidator BrandValidator;

        public BrandService(
            IUOW UOW, 
            IBrandValidator BrandValidator
        )
        {
            this.UOW = UOW;
            this.BrandValidator = BrandValidator;
        }
        public async Task<int> Count(BrandFilter BrandFilter)
        {
            int result = await UOW.BrandRepository.Count(BrandFilter);
            return result;
        }

        public async Task<List<Brand>> List(BrandFilter BrandFilter)
        {
            List<Brand> Brands = await UOW.BrandRepository.List(BrandFilter);
            return Brands;
        }

        public async Task<Brand> Get(long Id)
        {
            Brand Brand = await UOW.BrandRepository.Get(Id);
            if (Brand == null)
                return null;
            return Brand;
        }

        public async Task<Brand> Create(Brand Brand)
        {
            if (!await BrandValidator.Create(Brand))
                return Brand;

            try
            {
               
                await UOW.Begin();
                await UOW.BrandRepository.Create(Brand);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Brand, "", nameof(BrandService));
                return await UOW.BrandRepository.Get(Brand.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(BrandService));
                throw new MessageException(ex);
            }
        }

        public async Task<Brand> Update(Brand Brand)
        {
            if (!await BrandValidator.Update(Brand))
                return Brand;
            try
            {
                var oldData = await UOW.BrandRepository.Get(Brand.Id);

                await UOW.Begin();
                await UOW.BrandRepository.Update(Brand);
                await UOW.Commit();

                var newData = await UOW.BrandRepository.Get(Brand.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(BrandService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(BrandService));
                throw new MessageException(ex);
            }
        }

        public async Task<Brand> Delete(Brand Brand)
        {
            if (!await BrandValidator.Delete(Brand))
                return Brand;

            try
            {
                await UOW.Begin();
                await UOW.BrandRepository.Delete(Brand);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Brand, nameof(BrandService));
                return Brand;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(BrandService));
                throw new MessageException(ex);
            }
        }
    }
}
