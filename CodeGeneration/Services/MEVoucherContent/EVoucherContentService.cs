
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MEVoucherContent
{
    public interface IEVoucherContentService : IServiceScoped
    {
        Task<int> Count(EVoucherContentFilter EVoucherContentFilter);
        Task<List<EVoucherContent>> List(EVoucherContentFilter EVoucherContentFilter);
        Task<EVoucherContent> Get(long Id);
        Task<EVoucherContent> Create(EVoucherContent EVoucherContent);
        Task<EVoucherContent> Update(EVoucherContent EVoucherContent);
        Task<EVoucherContent> Delete(EVoucherContent EVoucherContent);
    }

    public class EVoucherContentService : IEVoucherContentService
    {
        public IUOW UOW;
        public IEVoucherContentValidator EVoucherContentValidator;

        public EVoucherContentService(
            IUOW UOW, 
            IEVoucherContentValidator EVoucherContentValidator
        )
        {
            this.UOW = UOW;
            this.EVoucherContentValidator = EVoucherContentValidator;
        }
        public async Task<int> Count(EVoucherContentFilter EVoucherContentFilter)
        {
            int result = await UOW.EVoucherContentRepository.Count(EVoucherContentFilter);
            return result;
        }

        public async Task<List<EVoucherContent>> List(EVoucherContentFilter EVoucherContentFilter)
        {
            List<EVoucherContent> EVoucherContents = await UOW.EVoucherContentRepository.List(EVoucherContentFilter);
            return EVoucherContents;
        }

        public async Task<EVoucherContent> Get(long Id)
        {
            EVoucherContent EVoucherContent = await UOW.EVoucherContentRepository.Get(Id);
            if (EVoucherContent == null)
                return null;
            return EVoucherContent;
        }

        public async Task<EVoucherContent> Create(EVoucherContent EVoucherContent)
        {
            if (!await EVoucherContentValidator.Create(EVoucherContent))
                return EVoucherContent;

            try
            {
               
                await UOW.Begin();
                await UOW.EVoucherContentRepository.Create(EVoucherContent);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(EVoucherContent, "", nameof(EVoucherContentService));
                return await UOW.EVoucherContentRepository.Get(EVoucherContent.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(EVoucherContentService));
                throw new MessageException(ex);
            }
        }

        public async Task<EVoucherContent> Update(EVoucherContent EVoucherContent)
        {
            if (!await EVoucherContentValidator.Update(EVoucherContent))
                return EVoucherContent;
            try
            {
                var oldData = await UOW.EVoucherContentRepository.Get(EVoucherContent.Id);

                await UOW.Begin();
                await UOW.EVoucherContentRepository.Update(EVoucherContent);
                await UOW.Commit();

                var newData = await UOW.EVoucherContentRepository.Get(EVoucherContent.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(EVoucherContentService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(EVoucherContentService));
                throw new MessageException(ex);
            }
        }

        public async Task<EVoucherContent> Delete(EVoucherContent EVoucherContent)
        {
            if (!await EVoucherContentValidator.Delete(EVoucherContent))
                return EVoucherContent;

            try
            {
                await UOW.Begin();
                await UOW.EVoucherContentRepository.Delete(EVoucherContent);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", EVoucherContent, nameof(EVoucherContentService));
                return EVoucherContent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(EVoucherContentService));
                throw new MessageException(ex);
            }
        }
    }
}
