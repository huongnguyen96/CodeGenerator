
using Common;
using WeGift.Entities;
using WeGift.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeGift.Services.MUser
{
    public interface IUserService : IServiceScoped
    {
        Task<int> Count(UserFilter UserFilter);
        Task<List<User>> List(UserFilter UserFilter);
        Task<User> Get(long Id);
        Task<User> Create(User User);
        Task<User> Update(User User);
        Task<User> Delete(User User);
    }

    public class UserService : IUserService
    {
        public IUOW UOW;
        public IUserValidator UserValidator;

        public UserService(
            IUOW UOW, 
            IUserValidator UserValidator
        )
        {
            this.UOW = UOW;
            this.UserValidator = UserValidator;
        }
        public async Task<int> Count(UserFilter UserFilter)
        {
            int result = await UOW.UserRepository.Count(UserFilter);
            return result;
        }

        public async Task<List<User>> List(UserFilter UserFilter)
        {
            List<User> Users = await UOW.UserRepository.List(UserFilter);
            return Users;
        }

        public async Task<User> Get(long Id)
        {
            User User = await UOW.UserRepository.Get(Id);
            if (User == null)
                return null;
            return User;
        }

        public async Task<User> Create(User User)
        {
            if (!await UserValidator.Create(User))
                return User;

            try
            {
               
                await UOW.Begin();
                await UOW.UserRepository.Create(User);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(User, "", nameof(UserService));
                return await UOW.UserRepository.Get(User.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(UserService));
                throw new MessageException(ex);
            }
        }

        public async Task<User> Update(User User)
        {
            if (!await UserValidator.Update(User))
                return User;
            try
            {
                var oldData = await UOW.UserRepository.Get(User.Id);

                await UOW.Begin();
                await UOW.UserRepository.Update(User);
                await UOW.Commit();

                var newData = await UOW.UserRepository.Get(User.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(UserService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(UserService));
                throw new MessageException(ex);
            }
        }

        public async Task<User> Delete(User User)
        {
            if (!await UserValidator.Delete(User))
                return User;

            try
            {
                await UOW.Begin();
                await UOW.UserRepository.Delete(User);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", User, nameof(UserService));
                return User;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(UserService));
                throw new MessageException(ex);
            }
        }
    }
}
