
using Common;
using WeGift.Entities;
using WeGift.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeGift.Services.MCategory
{
    public interface ICategoryService : IServiceScoped
    {
        Task<int> Count(CategoryFilter CategoryFilter);
        Task<List<Category>> List(CategoryFilter CategoryFilter);
        Task<Category> Get(long Id);
        Task<Category> Create(Category Category);
        Task<Category> Update(Category Category);
        Task<Category> Delete(Category Category);
    }

    public class CategoryService : ICategoryService
    {
        public IUOW UOW;
        public ICategoryValidator CategoryValidator;

        public CategoryService(
            IUOW UOW, 
            ICategoryValidator CategoryValidator
        )
        {
            this.UOW = UOW;
            this.CategoryValidator = CategoryValidator;
        }
        public async Task<int> Count(CategoryFilter CategoryFilter)
        {
            int result = await UOW.CategoryRepository.Count(CategoryFilter);
            return result;
        }

        public async Task<List<Category>> List(CategoryFilter CategoryFilter)
        {
            List<Category> Categorys = await UOW.CategoryRepository.List(CategoryFilter);
            return Categorys;
        }

        public async Task<Category> Get(long Id)
        {
            Category Category = await UOW.CategoryRepository.Get(Id);
            if (Category == null)
                return null;
            return Category;
        }

        public async Task<Category> Create(Category Category)
        {
            if (!await CategoryValidator.Create(Category))
                return Category;

            try
            {
               
                await UOW.Begin();
                await UOW.CategoryRepository.Create(Category);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Category, "", nameof(CategoryService));
                return await UOW.CategoryRepository.Get(Category.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CategoryService));
                throw new MessageException(ex);
            }
        }

        public async Task<Category> Update(Category Category)
        {
            if (!await CategoryValidator.Update(Category))
                return Category;
            try
            {
                var oldData = await UOW.CategoryRepository.Get(Category.Id);

                await UOW.Begin();
                await UOW.CategoryRepository.Update(Category);
                await UOW.Commit();

                var newData = await UOW.CategoryRepository.Get(Category.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(CategoryService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CategoryService));
                throw new MessageException(ex);
            }
        }

        public async Task<Category> Delete(Category Category)
        {
            if (!await CategoryValidator.Delete(Category))
                return Category;

            try
            {
                await UOW.Begin();
                await UOW.CategoryRepository.Delete(Category);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Category, nameof(CategoryService));
                return Category;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(CategoryService));
                throw new MessageException(ex);
            }
        }
    }
}
