
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MCategory
{
    public interface ICategoryValidator : IServiceScoped
    {
        Task<bool> Create(Category Category);
        Task<bool> Update(Category Category);
        Task<bool> Delete(Category Category);
    }

    public class CategoryValidator : ICategoryValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public CategoryValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Category Category)
        {
            CategoryFilter CategoryFilter = new CategoryFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = Category.Id },
                Selects = CategorySelect.Id
            };

            int count = await UOW.CategoryRepository.Count(CategoryFilter);

            if (count == 0)
                Category.AddError(nameof(CategoryValidator), nameof(Category.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(Category Category)
        {
            return Category.IsValidated;
        }

        public async Task<bool> Update(Category Category)
        {
            if (await ValidateId(Category))
            {
            }
            return Category.IsValidated;
        }

        public async Task<bool> Delete(Category Category)
        {
            if (await ValidateId(Category))
            {
            }
            return Category.IsValidated;
        }
    }
}
