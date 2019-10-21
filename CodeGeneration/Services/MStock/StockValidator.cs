
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MStock
{
    public interface IStockValidator : IServiceScoped
    {
        Task<bool> Create(Stock Stock);
        Task<bool> Update(Stock Stock);
        Task<bool> Delete(Stock Stock);
    }

    public class StockValidator : IStockValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public StockValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Stock Stock)
        {
            StockFilter StockFilter = new StockFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Stock.Id },
                Selects = StockSelect.Id
            };

            int count = await UOW.StockRepository.Count(StockFilter);

            if (count == 0)
                Stock.AddError(nameof(StockValidator), nameof(Stock.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Stock Stock)
        {
            return Stock.IsValidated;
        }

        public async Task<bool> Update(Stock Stock)
        {
            if (await ValidateId(Stock))
            {
            }
            return Stock.IsValidated;
        }

        public async Task<bool> Delete(Stock Stock)
        {
            if (await ValidateId(Stock))
            {
            }
            return Stock.IsValidated;
        }
    }
}
