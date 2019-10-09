
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
    public interface IUserProfileRepository
    {
        Task<int> Count(UserProfileFilter UserProfileFilter);
        Task<List<UserProfile>> List(UserProfileFilter UserProfileFilter);
        Task<UserProfile> Get(Guid Id);
        Task<bool> Create(UserProfile UserProfile);
        Task<bool> Update(UserProfile UserProfile);
        Task<bool> Delete(Guid Id);
        
    }
    public class UserProfileRepository : IUserProfileRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public UserProfileRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<UserProfileDAO> DynamicFilter(IQueryable<UserProfileDAO> query, UserProfileFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Key != null)
                query = query.Where(q => q.Key, filter.Key);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.Value != null)
                query = query.Where(q => q.Value, filter.Value);
            return query;
        }
        private IQueryable<UserProfileDAO> DynamicOrder(IQueryable<UserProfileDAO> query,  UserProfileFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case UserProfileOrder.Key:
                            query = query.OrderBy(q => q.Key);
                            break;
                        case UserProfileOrder.Value:
                            query = query.OrderBy(q => q.Value);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case UserProfileOrder.Key:
                            query = query.OrderByDescending(q => q.Key);
                            break;
                        case UserProfileOrder.Value:
                            query = query.OrderByDescending(q => q.Value);
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

        private async Task<List<UserProfile>> DynamicSelect(IQueryable<UserProfileDAO> query, UserProfileFilter filter)
        {
            List <UserProfile> UserProfiles = await query.Select(q => new UserProfile()
            {
                
                Id = filter.Selects.Contains(UserProfileSelect.Id) ? q.Id : default(Guid),
                Key = filter.Selects.Contains(UserProfileSelect.Key) ? q.Key : default(string),
                UserId = filter.Selects.Contains(UserProfileSelect.User) ? q.UserId : default(Guid),
                Value = filter.Selects.Contains(UserProfileSelect.Value) ? q.Value : default(string),
            }).ToListAsync();
            return UserProfiles;
        }

        public async Task<int> Count(UserProfileFilter filter)
        {
            IQueryable <UserProfileDAO> UserProfileDAOs = ERPContext.UserProfile;
            UserProfileDAOs = DynamicFilter(UserProfileDAOs, filter);
            return await UserProfileDAOs.CountAsync();
        }

        public async Task<List<UserProfile>> List(UserProfileFilter filter)
        {
            if (filter == null) return new List<UserProfile>();
            IQueryable<UserProfileDAO> UserProfileDAOs = ERPContext.UserProfile;
            UserProfileDAOs = DynamicFilter(UserProfileDAOs, filter);
            UserProfileDAOs = DynamicOrder(UserProfileDAOs, filter);
            var UserProfiles = await DynamicSelect(UserProfileDAOs, filter);
            return UserProfiles;
        }

        public async Task<UserProfile> Get(Guid Id)
        {
            UserProfile UserProfile = await ERPContext.UserProfile.Where(l => l.Id == Id).Select(UserProfileDAO => new UserProfile()
            {
                 
                Id = UserProfileDAO.Id,
                Key = UserProfileDAO.Key,
                UserId = UserProfileDAO.UserId,
                Value = UserProfileDAO.Value,
            }).FirstOrDefaultAsync();
            return UserProfile;
        }

        public async Task<bool> Create(UserProfile UserProfile)
        {
            UserProfileDAO UserProfileDAO = new UserProfileDAO();
            
            UserProfileDAO.Id = UserProfile.Id;
            UserProfileDAO.Key = UserProfile.Key;
            UserProfileDAO.UserId = UserProfile.UserId;
            UserProfileDAO.Value = UserProfile.Value;
            UserProfileDAO.Disabled = false;
            
            await ERPContext.UserProfile.AddAsync(UserProfileDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(UserProfile UserProfile)
        {
            UserProfileDAO UserProfileDAO = ERPContext.UserProfile.Where(b => b.Id == UserProfile.Id).FirstOrDefault();
            
            UserProfileDAO.Id = UserProfile.Id;
            UserProfileDAO.Key = UserProfile.Key;
            UserProfileDAO.UserId = UserProfile.UserId;
            UserProfileDAO.Value = UserProfile.Value;
            UserProfileDAO.Disabled = false;
            ERPContext.UserProfile.Update(UserProfileDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            UserProfileDAO UserProfileDAO = await ERPContext.UserProfile.Where(x => x.Id == Id).FirstOrDefaultAsync();
            UserProfileDAO.Disabled = true;
            ERPContext.UserProfile.Update(UserProfileDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
