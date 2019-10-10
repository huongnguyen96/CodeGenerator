
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Entities;
using WeGift.Repositories;

namespace WeGift.Services.MUser
{
    public interface IUserValidator : IServiceScoped
    {
        Task<bool> Create(User User);
        Task<bool> Update(User User);
        Task<bool> Delete(User User);
    }

    public class UserValidator : IUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public UserValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(User User)
        {
            UserFilter UserFilter = new UserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = User.Id },
                Selects = UserSelect.Id
            };

            int count = await UOW.UserRepository.Count(UserFilter);

            if (count == 0)
                User.AddError(nameof(UserValidator), nameof(User.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(User User)
        {
            return User.IsValidated;
        }

        public async Task<bool> Update(User User)
        {
            if (await ValidateId(User))
            {
            }
            return User.IsValidated;
        }

        public async Task<bool> Delete(User User)
        {
            if (await ValidateId(User))
            {
            }
            return User.IsValidated;
        }
    }
}
