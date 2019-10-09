
using Common;
using ERP.Entities;
using CodeGeneration.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    public interface IUserRepository
    {
        Task<int> Count(UserFilter UserFilter);
        Task<List<User>> List(UserFilter UserFilter);
        Task<User> Get(Guid Id);
        Task<bool> Create(User User);
        Task<bool> Update(User User);
        Task<bool> Delete(Guid Id);
        
    }
    public class UserRepository : IUserRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public UserRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
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
            if (filter.EmployeeId.HasValue)
                query = query.Where(q => q.EmployeeId.HasValue && q.EmployeeId.Value == filter.EmployeeId.Value);
            if (filter.EmployeeId != null)
                query = query.Where(q => q.EmployeeId, filter.EmployeeId);
            if (filter.IsSysUser.HasValue)
                query = query.Where(q => q.IsSysUser == filter.IsSysUser.Value);
            if (filter.IsActive.HasValue)
                query = query.Where(q => q.IsActive == filter.IsActive.Value);
            if (filter.Salt != null)
                query = query.Where(q => q.Salt, filter.Salt);
            return query;
        }
        private IQueryable<UserDAO> DynamicOrder(IQueryable<UserDAO> query,  UserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case UserOrder.Username:
                            query = query.OrderBy(q => q.Username);
                            break;
                        case UserOrder.Password:
                            query = query.OrderBy(q => q.Password);
                            break;
                        case UserOrder.EmployeeId:
                            query = query.OrderBy(q => q.EmployeeId);
                            break;
                        case UserOrder.Salt:
                            query = query.OrderBy(q => q.Salt);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case UserOrder.Username:
                            query = query.OrderByDescending(q => q.Username);
                            break;
                        case UserOrder.Password:
                            query = query.OrderByDescending(q => q.Password);
                            break;
                        case UserOrder.EmployeeId:
                            query = query.OrderByDescending(q => q.EmployeeId);
                            break;
                        case UserOrder.Salt:
                            query = query.OrderByDescending(q => q.Salt);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<User>> DynamicSelect(IQueryable<UserDAO> query, UserFilter filter)
        {
            List <User> Users = await query.Select(q => new User()
            {
                
                Id = filter.Selects.Contains(UserSelect.Id) ? q.Id : default(Guid),
                Username = filter.Selects.Contains(UserSelect.Username) ? q.Username : default(string),
                Password = filter.Selects.Contains(UserSelect.Password) ? q.Password : default(string),
                EmployeeId = filter.Selects.Contains(UserSelect.Employee) ? q.EmployeeId : default(Guid?),
                Salt = filter.Selects.Contains(UserSelect.Salt) ? q.Salt : default(string),
            }).ToListAsync();
            return Users;
        }

        public async Task<int> Count(UserFilter filter)
        {
            IQueryable <UserDAO> UserDAOs = ERPContext.User;
            UserDAOs = DynamicFilter(UserDAOs, filter);
            return await UserDAOs.CountAsync();
        }

        public async Task<List<User>> List(UserFilter filter)
        {
            if (filter == null) return new List<User>();
            IQueryable<UserDAO> UserDAOs = ERPContext.User;
            UserDAOs = DynamicFilter(UserDAOs, filter);
            UserDAOs = DynamicOrder(UserDAOs, filter);
            var Users = await DynamicSelect(UserDAOs, filter);
            return Users;
        }

        public async Task<User> Get(Guid Id)
        {
            User User = await ERPContext.User.Where(l => l.Id == Id).Select(UserDAO => new User()
            {
                 
                Id = UserDAO.Id,
                Username = UserDAO.Username,
                Password = UserDAO.Password,
                EmployeeId = UserDAO.EmployeeId,
                Salt = UserDAO.Salt,
            }).FirstOrDefaultAsync();
            return User;
        }

        public async Task<bool> Create(User User)
        {
            UserDAO UserDAO = new UserDAO();
            
            UserDAO.Id = User.Id;
            UserDAO.Username = User.Username;
            UserDAO.Password = User.Password;
            UserDAO.EmployeeId = User.EmployeeId;
            UserDAO.Salt = User.Salt;
            UserDAO.Disabled = false;
            
            await ERPContext.User.AddAsync(UserDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(User User)
        {
            UserDAO UserDAO = ERPContext.User.Where(b => b.Id == User.Id).FirstOrDefault();
            
            UserDAO.Id = User.Id;
            UserDAO.Username = User.Username;
            UserDAO.Password = User.Password;
            UserDAO.EmployeeId = User.EmployeeId;
            UserDAO.Salt = User.Salt;
            UserDAO.Disabled = false;
            ERPContext.User.Update(UserDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            UserDAO UserDAO = await ERPContext.User.Where(x => x.Id == Id).FirstOrDefaultAsync();
            UserDAO.Disabled = true;
            ERPContext.User.Update(UserDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
