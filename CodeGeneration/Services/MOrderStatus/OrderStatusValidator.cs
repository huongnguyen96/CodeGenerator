
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MOrderStatus
{
    public interface IOrderStatusValidator : IServiceScoped
    {
        Task<bool> Create(OrderStatus OrderStatus);
        Task<bool> Update(OrderStatus OrderStatus);
        Task<bool> Delete(OrderStatus OrderStatus);
    }

    public class OrderStatusValidator : IOrderStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public OrderStatusValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(OrderStatus OrderStatus)
        {
            OrderStatusFilter OrderStatusFilter = new OrderStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = OrderStatus.Id },
                Selects = OrderStatusSelect.Id
            };

            int count = await UOW.OrderStatusRepository.Count(OrderStatusFilter);

            if (count == 0)
                OrderStatus.AddError(nameof(OrderStatusValidator), nameof(OrderStatus.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(OrderStatus OrderStatus)
        {
            return OrderStatus.IsValidated;
        }

        public async Task<bool> Update(OrderStatus OrderStatus)
        {
            if (await ValidateId(OrderStatus))
            {
            }
            return OrderStatus.IsValidated;
        }

        public async Task<bool> Delete(OrderStatus OrderStatus)
        {
            if (await ValidateId(OrderStatus))
            {
            }
            return OrderStatus.IsValidated;
        }
    }
}
