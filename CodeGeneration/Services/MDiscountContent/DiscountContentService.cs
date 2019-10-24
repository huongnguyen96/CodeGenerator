
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MDiscountContent
{
    public interface IDiscountContentService : IServiceScoped
    {
        Task<int> Count(DiscountContentFilter DiscountContentFilter);
        Task<List<DiscountContent>> List(DiscountContentFilter DiscountContentFilter);
        Task<DiscountContent> Get(long Id);
        Task<DiscountContent> Create(DiscountContent DiscountContent);
        Task<DiscountContent> Update(DiscountContent DiscountContent);
        Task<DiscountContent> Delete(DiscountContent DiscountContent);
    }

    public class DiscountContentService : IDiscountContentService
    {
        public IUOW UOW;
        public IDiscountContentValidator DiscountContentValidator;

        public DiscountContentService(
            IUOW UOW, 
            IDiscountContentValidator DiscountContentValidator
        )
        {
            this.UOW = UOW;
            this.DiscountContentValidator = DiscountContentValidator;
        }
        public async Task<int> Count(DiscountContentFilter DiscountContentFilter)
        {
            int result = await UOW.DiscountContentRepository.Count(DiscountContentFilter);
            return result;
        }

        public async Task<List<DiscountContent>> List(DiscountContentFilter DiscountContentFilter)
        {
            List<DiscountContent> DiscountContents = await UOW.DiscountContentRepository.List(DiscountContentFilter);
            return DiscountContents;
        }

        public async Task<DiscountContent> Get(long Id)
        {
            DiscountContent DiscountContent = await UOW.DiscountContentRepository.Get(Id);
            if (DiscountContent == null)
                return null;
            return DiscountContent;
        }

        public async Task<DiscountContent> Create(DiscountContent DiscountContent)
        {
            if (!await DiscountContentValidator.Create(DiscountContent))
                return DiscountContent;

            try
            {
               
                await UOW.Begin();
                await UOW.DiscountContentRepository.Create(DiscountContent);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(DiscountContent, "", nameof(DiscountContentService));
                return await UOW.DiscountContentRepository.Get(DiscountContent.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountContentService));
                throw new MessageException(ex);
            }
        }

        public async Task<DiscountContent> Update(DiscountContent DiscountContent)
        {
            if (!await DiscountContentValidator.Update(DiscountContent))
                return DiscountContent;
            try
            {
                var oldData = await UOW.DiscountContentRepository.Get(DiscountContent.Id);

                await UOW.Begin();
                await UOW.DiscountContentRepository.Update(DiscountContent);
                await UOW.Commit();

                var newData = await UOW.DiscountContentRepository.Get(DiscountContent.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(DiscountContentService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountContentService));
                throw new MessageException(ex);
            }
        }

        public async Task<DiscountContent> Delete(DiscountContent DiscountContent)
        {
            if (!await DiscountContentValidator.Delete(DiscountContent))
                return DiscountContent;

            try
            {
                await UOW.Begin();
                await UOW.DiscountContentRepository.Delete(DiscountContent);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", DiscountContent, nameof(DiscountContentService));
                return DiscountContent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(DiscountContentService));
                throw new MessageException(ex);
            }
        }
    }
}
