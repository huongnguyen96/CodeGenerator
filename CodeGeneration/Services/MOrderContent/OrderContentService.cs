
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MOrderContent
{
    public interface IOrderContentService : IServiceScoped
    {
        Task<int> Count(OrderContentFilter OrderContentFilter);
        Task<List<OrderContent>> List(OrderContentFilter OrderContentFilter);
        Task<OrderContent> Get(long Id);
        Task<OrderContent> Create(OrderContent OrderContent);
        Task<OrderContent> Update(OrderContent OrderContent);
        Task<OrderContent> Delete(OrderContent OrderContent);
    }

    public class OrderContentService : IOrderContentService
    {
        public IUOW UOW;
        public IOrderContentValidator OrderContentValidator;

        public OrderContentService(
            IUOW UOW, 
            IOrderContentValidator OrderContentValidator
        )
        {
            this.UOW = UOW;
            this.OrderContentValidator = OrderContentValidator;
        }
        public async Task<int> Count(OrderContentFilter OrderContentFilter)
        {
            int result = await UOW.OrderContentRepository.Count(OrderContentFilter);
            return result;
        }

        public async Task<List<OrderContent>> List(OrderContentFilter OrderContentFilter)
        {
            List<OrderContent> OrderContents = await UOW.OrderContentRepository.List(OrderContentFilter);
            return OrderContents;
        }

        public async Task<OrderContent> Get(long Id)
        {
            OrderContent OrderContent = await UOW.OrderContentRepository.Get(Id);
            if (OrderContent == null)
                return null;
            return OrderContent;
        }

        public async Task<OrderContent> Create(OrderContent OrderContent)
        {
            if (!await OrderContentValidator.Create(OrderContent))
                return OrderContent;

            try
            {
               
                await UOW.Begin();
                await UOW.OrderContentRepository.Create(OrderContent);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(OrderContent, "", nameof(OrderContentService));
                return await UOW.OrderContentRepository.Get(OrderContent.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderContentService));
                throw new MessageException(ex);
            }
        }

        public async Task<OrderContent> Update(OrderContent OrderContent)
        {
            if (!await OrderContentValidator.Update(OrderContent))
                return OrderContent;
            try
            {
                var oldData = await UOW.OrderContentRepository.Get(OrderContent.Id);

                await UOW.Begin();
                await UOW.OrderContentRepository.Update(OrderContent);
                await UOW.Commit();

                var newData = await UOW.OrderContentRepository.Get(OrderContent.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(OrderContentService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderContentService));
                throw new MessageException(ex);
            }
        }

        public async Task<OrderContent> Delete(OrderContent OrderContent)
        {
            if (!await OrderContentValidator.Delete(OrderContent))
                return OrderContent;

            try
            {
                await UOW.Begin();
                await UOW.OrderContentRepository.Delete(OrderContent);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", OrderContent, nameof(OrderContentService));
                return OrderContent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderContentService));
                throw new MessageException(ex);
            }
        }
    }
}
