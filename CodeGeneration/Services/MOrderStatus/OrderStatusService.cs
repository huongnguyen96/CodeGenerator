
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MOrderStatus
{
    public interface IOrderStatusService : IServiceScoped
    {
        Task<int> Count(OrderStatusFilter OrderStatusFilter);
        Task<List<OrderStatus>> List(OrderStatusFilter OrderStatusFilter);
        Task<OrderStatus> Get(long Id);
        Task<OrderStatus> Create(OrderStatus OrderStatus);
        Task<OrderStatus> Update(OrderStatus OrderStatus);
        Task<OrderStatus> Delete(OrderStatus OrderStatus);
    }

    public class OrderStatusService : IOrderStatusService
    {
        public IUOW UOW;
        public IOrderStatusValidator OrderStatusValidator;

        public OrderStatusService(
            IUOW UOW, 
            IOrderStatusValidator OrderStatusValidator
        )
        {
            this.UOW = UOW;
            this.OrderStatusValidator = OrderStatusValidator;
        }
        public async Task<int> Count(OrderStatusFilter OrderStatusFilter)
        {
            int result = await UOW.OrderStatusRepository.Count(OrderStatusFilter);
            return result;
        }

        public async Task<List<OrderStatus>> List(OrderStatusFilter OrderStatusFilter)
        {
            List<OrderStatus> OrderStatuss = await UOW.OrderStatusRepository.List(OrderStatusFilter);
            return OrderStatuss;
        }

        public async Task<OrderStatus> Get(long Id)
        {
            OrderStatus OrderStatus = await UOW.OrderStatusRepository.Get(Id);
            if (OrderStatus == null)
                return null;
            return OrderStatus;
        }

        public async Task<OrderStatus> Create(OrderStatus OrderStatus)
        {
            if (!await OrderStatusValidator.Create(OrderStatus))
                return OrderStatus;

            try
            {
               
                await UOW.Begin();
                await UOW.OrderStatusRepository.Create(OrderStatus);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(OrderStatus, "", nameof(OrderStatusService));
                return await UOW.OrderStatusRepository.Get(OrderStatus.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderStatusService));
                throw new MessageException(ex);
            }
        }

        public async Task<OrderStatus> Update(OrderStatus OrderStatus)
        {
            if (!await OrderStatusValidator.Update(OrderStatus))
                return OrderStatus;
            try
            {
                var oldData = await UOW.OrderStatusRepository.Get(OrderStatus.Id);

                await UOW.Begin();
                await UOW.OrderStatusRepository.Update(OrderStatus);
                await UOW.Commit();

                var newData = await UOW.OrderStatusRepository.Get(OrderStatus.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(OrderStatusService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderStatusService));
                throw new MessageException(ex);
            }
        }

        public async Task<OrderStatus> Delete(OrderStatus OrderStatus)
        {
            if (!await OrderStatusValidator.Delete(OrderStatus))
                return OrderStatus;

            try
            {
                await UOW.Begin();
                await UOW.OrderStatusRepository.Delete(OrderStatus);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", OrderStatus, nameof(OrderStatusService));
                return OrderStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderStatusService));
                throw new MessageException(ex);
            }
        }
    }
}
