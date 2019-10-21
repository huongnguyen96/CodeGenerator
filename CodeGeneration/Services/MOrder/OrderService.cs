
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MOrder
{
    public interface IOrderService : IServiceScoped
    {
        Task<int> Count(OrderFilter OrderFilter);
        Task<List<Order>> List(OrderFilter OrderFilter);
        Task<Order> Get(long Id);
        Task<Order> Create(Order Order);
        Task<Order> Update(Order Order);
        Task<Order> Delete(Order Order);
    }

    public class OrderService : IOrderService
    {
        public IUOW UOW;
        public IOrderValidator OrderValidator;

        public OrderService(
            IUOW UOW, 
            IOrderValidator OrderValidator
        )
        {
            this.UOW = UOW;
            this.OrderValidator = OrderValidator;
        }
        public async Task<int> Count(OrderFilter OrderFilter)
        {
            int result = await UOW.OrderRepository.Count(OrderFilter);
            return result;
        }

        public async Task<List<Order>> List(OrderFilter OrderFilter)
        {
            List<Order> Orders = await UOW.OrderRepository.List(OrderFilter);
            return Orders;
        }

        public async Task<Order> Get(long Id)
        {
            Order Order = await UOW.OrderRepository.Get(Id);
            if (Order == null)
                return null;
            return Order;
        }

        public async Task<Order> Create(Order Order)
        {
            if (!await OrderValidator.Create(Order))
                return Order;

            try
            {
               
                await UOW.Begin();
                await UOW.OrderRepository.Create(Order);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Order, "", nameof(OrderService));
                return await UOW.OrderRepository.Get(Order.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderService));
                throw new MessageException(ex);
            }
        }

        public async Task<Order> Update(Order Order)
        {
            if (!await OrderValidator.Update(Order))
                return Order;
            try
            {
                var oldData = await UOW.OrderRepository.Get(Order.Id);

                await UOW.Begin();
                await UOW.OrderRepository.Update(Order);
                await UOW.Commit();

                var newData = await UOW.OrderRepository.Get(Order.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(OrderService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderService));
                throw new MessageException(ex);
            }
        }

        public async Task<Order> Delete(Order Order)
        {
            if (!await OrderValidator.Delete(Order))
                return Order;

            try
            {
                await UOW.Begin();
                await UOW.OrderRepository.Delete(Order);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Order, nameof(OrderService));
                return Order;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(OrderService));
                throw new MessageException(ex);
            }
        }
    }
}
