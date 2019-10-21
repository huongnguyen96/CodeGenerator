
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MOrder
{
    public interface IOrderValidator : IServiceScoped
    {
        Task<bool> Create(Order Order);
        Task<bool> Update(Order Order);
        Task<bool> Delete(Order Order);
    }

    public class OrderValidator : IOrderValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public OrderValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Order Order)
        {
            OrderFilter OrderFilter = new OrderFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Order.Id },
                Selects = OrderSelect.Id
            };

            int count = await UOW.OrderRepository.Count(OrderFilter);

            if (count == 0)
                Order.AddError(nameof(OrderValidator), nameof(Order.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Order Order)
        {
            return Order.IsValidated;
        }

        public async Task<bool> Update(Order Order)
        {
            if (await ValidateId(Order))
            {
            }
            return Order.IsValidated;
        }

        public async Task<bool> Delete(Order Order)
        {
            if (await ValidateId(Order))
            {
            }
            return Order.IsValidated;
        }
    }
}
