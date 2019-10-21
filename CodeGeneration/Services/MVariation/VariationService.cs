
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MVariation
{
    public interface IVariationService : IServiceScoped
    {
        Task<int> Count(VariationFilter VariationFilter);
        Task<List<Variation>> List(VariationFilter VariationFilter);
        Task<Variation> Get(long Id);
        Task<Variation> Create(Variation Variation);
        Task<Variation> Update(Variation Variation);
        Task<Variation> Delete(Variation Variation);
    }

    public class VariationService : IVariationService
    {
        public IUOW UOW;
        public IVariationValidator VariationValidator;

        public VariationService(
            IUOW UOW, 
            IVariationValidator VariationValidator
        )
        {
            this.UOW = UOW;
            this.VariationValidator = VariationValidator;
        }
        public async Task<int> Count(VariationFilter VariationFilter)
        {
            int result = await UOW.VariationRepository.Count(VariationFilter);
            return result;
        }

        public async Task<List<Variation>> List(VariationFilter VariationFilter)
        {
            List<Variation> Variations = await UOW.VariationRepository.List(VariationFilter);
            return Variations;
        }

        public async Task<Variation> Get(long Id)
        {
            Variation Variation = await UOW.VariationRepository.Get(Id);
            if (Variation == null)
                return null;
            return Variation;
        }

        public async Task<Variation> Create(Variation Variation)
        {
            if (!await VariationValidator.Create(Variation))
                return Variation;

            try
            {
               
                await UOW.Begin();
                await UOW.VariationRepository.Create(Variation);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Variation, "", nameof(VariationService));
                return await UOW.VariationRepository.Get(Variation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(VariationService));
                throw new MessageException(ex);
            }
        }

        public async Task<Variation> Update(Variation Variation)
        {
            if (!await VariationValidator.Update(Variation))
                return Variation;
            try
            {
                var oldData = await UOW.VariationRepository.Get(Variation.Id);

                await UOW.Begin();
                await UOW.VariationRepository.Update(Variation);
                await UOW.Commit();

                var newData = await UOW.VariationRepository.Get(Variation.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(VariationService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(VariationService));
                throw new MessageException(ex);
            }
        }

        public async Task<Variation> Delete(Variation Variation)
        {
            if (!await VariationValidator.Delete(Variation))
                return Variation;

            try
            {
                await UOW.Begin();
                await UOW.VariationRepository.Delete(Variation);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Variation, nameof(VariationService));
                return Variation;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(VariationService));
                throw new MessageException(ex);
            }
        }
    }
}
