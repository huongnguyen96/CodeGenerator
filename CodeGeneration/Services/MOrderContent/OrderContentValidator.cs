
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MOrderContent
{
    public interface IOrderContentValidator : IServiceScoped
    {
        Task<bool> Create(OrderContent OrderContent);
        Task<bool> Update(OrderContent OrderContent);
        Task<bool> Delete(OrderContent OrderContent);
    }

    public class OrderContentValidator : IOrderContentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public OrderContentValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(OrderContent OrderContent)
        {
            OrderContentFilter OrderContentFilter = new OrderContentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = OrderContent.Id },
                Selects = OrderContentSelect.Id
            };

            int count = await UOW.OrderContentRepository.Count(OrderContentFilter);

            if (count == 0)
                OrderContent.AddError(nameof(OrderContentValidator), nameof(OrderContent.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(OrderContent OrderContent)
        {
            return OrderContent.IsValidated;
        }

        public async Task<bool> Update(OrderContent OrderContent)
        {
            if (await ValidateId(OrderContent))
            {
            }
            return OrderContent.IsValidated;
        }

        public async Task<bool> Delete(OrderContent OrderContent)
        {
            if (await ValidateId(OrderContent))
            {
            }
            return OrderContent.IsValidated;
        }
    }
}
