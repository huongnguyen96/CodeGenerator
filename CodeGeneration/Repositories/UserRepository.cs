
using Common;
using WG.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Repositories
{
    public interface IUserRepository
    {
        Task<int> Count(UserFilter UserFilter);
        Task<List<User>> List(UserFilter UserFilter);
        Task<User> Get(long Id);
        Task<bool> Create(User User);
        Task<bool> Update(User User);
        Task<bool> Delete(User User);
        
    }
    public class UserRepository : IUserRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public UserRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<UserDAO> DynamicFilter(IQueryable<UserDAO> query, UserFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Username != null)
                query = query.Where(q => q.Username, filter.Username);
            if (filter.Password != null)
                query = query.Where(q => q.Password, filter.Password);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<UserDAO> DynamicOrder(IQueryable<UserDAO> query,  UserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case UserOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case UserOrder.Username:
                            query = query.OrderBy(q => q.Username);
                            break;
                        case UserOrder.Password:
                            query = query.OrderBy(q => q.Password);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case UserOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case UserOrder.Username:
                            query = query.OrderByDescending(q => q.Username);
                            break;
                        case UserOrder.Password:
                            query = query.OrderByDescending(q => q.Password);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<User>> DynamicSelect(IQueryable<UserDAO> query, UserFilter filter)
        {
            List <User> Users = await query.Select(q => new User()
            {
                
                Id = filter.Selects.Contains(UserSelect.Id) ? q.Id : default(long),
                Username = filter.Selects.Contains(UserSelect.Username) ? q.Username : default(string),
                Password = filter.Selects.Contains(UserSelect.Password) ? q.Password : default(string),
            }).ToListAsync();
            return Users;
        }

        public async Task<int> Count(UserFilter filter)
        {
            IQueryable <UserDAO> UserDAOs = DataContext.User;
            UserDAOs = DynamicFilter(UserDAOs, filter);
            return await UserDAOs.CountAsync();
        }

        public async Task<List<User>> List(UserFilter filter)
        {
            if (filter == null) return new List<User>();
            IQueryable<UserDAO> UserDAOs = DataContext.User;
            UserDAOs = DynamicFilter(UserDAOs, filter);
            UserDAOs = DynamicOrder(UserDAOs, filter);
            var Users = await DynamicSelect(UserDAOs, filter);
            return Users;
        }

        
        public async Task<User> Get(long Id)
        {
            User User = await DataContext.User.Where(x => x.Id == Id).Select(UserDAO => new User()
            {
                 
                Id = UserDAO.Id,
                Username = UserDAO.Username,
                Password = UserDAO.Password,
            }).FirstOrDefaultAsync();
            return User;
        }

        public async Task<bool> Create(User User)
        {
            UserDAO UserDAO = new UserDAO();
            
            UserDAO.Id = User.Id;
            UserDAO.Username = User.Username;
            UserDAO.Password = User.Password;
            
            await DataContext.User.AddAsync(UserDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(User User)
        {
            UserDAO UserDAO = DataContext.User.Where(x => x.Id == User.Id).FirstOrDefault();
            
            UserDAO.Id = User.Id;
            UserDAO.Username = User.Username;
            UserDAO.Password = User.Password;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(User User)
        {
            UserDAO UserDAO = await DataContext.User.Where(x => x.Id == User.Id).FirstOrDefaultAsync();
            DataContext.User.Remove(UserDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
