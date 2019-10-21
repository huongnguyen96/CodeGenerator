
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MStock
{
    public interface IStockService : IServiceScoped
    {
        Task<int> Count(StockFilter StockFilter);
        Task<List<Stock>> List(StockFilter StockFilter);
        Task<Stock> Get(long Id);
        Task<Stock> Create(Stock Stock);
        Task<Stock> Update(Stock Stock);
        Task<Stock> Delete(Stock Stock);
    }

    public class StockService : IStockService
    {
        public IUOW UOW;
        public IStockValidator StockValidator;

        public StockService(
            IUOW UOW, 
            IStockValidator StockValidator
        )
        {
            this.UOW = UOW;
            this.StockValidator = StockValidator;
        }
        public async Task<int> Count(StockFilter StockFilter)
        {
            int result = await UOW.StockRepository.Count(StockFilter);
            return result;
        }

        public async Task<List<Stock>> List(StockFilter StockFilter)
        {
            List<Stock> Stocks = await UOW.StockRepository.List(StockFilter);
            return Stocks;
        }

        public async Task<Stock> Get(long Id)
        {
            Stock Stock = await UOW.StockRepository.Get(Id);
            if (Stock == null)
                return null;
            return Stock;
        }

        public async Task<Stock> Create(Stock Stock)
        {
            if (!await StockValidator.Create(Stock))
                return Stock;

            try
            {
               
                await UOW.Begin();
                await UOW.StockRepository.Create(Stock);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Stock, "", nameof(StockService));
                return await UOW.StockRepository.Get(Stock.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(StockService));
                throw new MessageException(ex);
            }
        }

        public async Task<Stock> Update(Stock Stock)
        {
            if (!await StockValidator.Update(Stock))
                return Stock;
            try
            {
                var oldData = await UOW.StockRepository.Get(Stock.Id);

                await UOW.Begin();
                await UOW.StockRepository.Update(Stock);
                await UOW.Commit();

                var newData = await UOW.StockRepository.Get(Stock.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(StockService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(StockService));
                throw new MessageException(ex);
            }
        }

        public async Task<Stock> Delete(Stock Stock)
        {
            if (!await StockValidator.Delete(Stock))
                return Stock;

            try
            {
                await UOW.Begin();
                await UOW.StockRepository.Delete(Stock);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Stock, nameof(StockService));
                return Stock;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(StockService));
                throw new MessageException(ex);
            }
        }
    }
}
