
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
    public interface INotificationRepository
    {
        Task<int> Count(NotificationFilter NotificationFilter);
        Task<List<Notification>> List(NotificationFilter NotificationFilter);
        Task<Notification> Get(Guid Id);
        Task<bool> Create(Notification Notification);
        Task<bool> Update(Notification Notification);
        Task<bool> Delete(Guid Id);
        
    }
    public class NotificationRepository : INotificationRepository
    {
        private ERPContext ERPContext;
        private ICurrentContext CurrentContext;
        public NotificationRepository(ERPContext ERPContext, ICurrentContext CurrentContext)
        {
            this.ERPContext = ERPContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<NotificationDAO> DynamicFilter(IQueryable<NotificationDAO> query, NotificationFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Time != null)
                query = query.Where(q => q.Time, filter.Time);
            if (filter.Unread.HasValue)
                query = query.Where(q => q.Unread == filter.Unread.Value);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.Content != null)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.URL != null)
                query = query.Where(q => q.URL, filter.URL);
            return query;
        }
        private IQueryable<NotificationDAO> DynamicOrder(IQueryable<NotificationDAO> query,  NotificationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case NotificationOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                        case NotificationOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case NotificationOrder.URL:
                            query = query.OrderBy(q => q.URL);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case NotificationOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
                            break;
                        case NotificationOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case NotificationOrder.URL:
                            query = query.OrderByDescending(q => q.URL);
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

        private async Task<List<Notification>> DynamicSelect(IQueryable<NotificationDAO> query, NotificationFilter filter)
        {
            List <Notification> Notifications = await query.Select(q => new Notification()
            {
                
                Id = filter.Selects.Contains(NotificationSelect.Id) ? q.Id : default(Guid),
                Time = filter.Selects.Contains(NotificationSelect.Time) ? q.Time : default(DateTime),
                UserId = filter.Selects.Contains(NotificationSelect.User) ? q.UserId : default(Guid),
                Content = filter.Selects.Contains(NotificationSelect.Content) ? q.Content : default(string),
                URL = filter.Selects.Contains(NotificationSelect.URL) ? q.URL : default(string),
            }).ToListAsync();
            return Notifications;
        }

        public async Task<int> Count(NotificationFilter filter)
        {
            IQueryable <NotificationDAO> NotificationDAOs = ERPContext.Notification;
            NotificationDAOs = DynamicFilter(NotificationDAOs, filter);
            return await NotificationDAOs.CountAsync();
        }

        public async Task<List<Notification>> List(NotificationFilter filter)
        {
            if (filter == null) return new List<Notification>();
            IQueryable<NotificationDAO> NotificationDAOs = ERPContext.Notification;
            NotificationDAOs = DynamicFilter(NotificationDAOs, filter);
            NotificationDAOs = DynamicOrder(NotificationDAOs, filter);
            var Notifications = await DynamicSelect(NotificationDAOs, filter);
            return Notifications;
        }

        public async Task<Notification> Get(Guid Id)
        {
            Notification Notification = await ERPContext.Notification.Where(l => l.Id == Id).Select(NotificationDAO => new Notification()
            {
                 
                Id = NotificationDAO.Id,
                Time = NotificationDAO.Time,
                UserId = NotificationDAO.UserId,
                Content = NotificationDAO.Content,
                URL = NotificationDAO.URL,
            }).FirstOrDefaultAsync();
            return Notification;
        }

        public async Task<bool> Create(Notification Notification)
        {
            NotificationDAO NotificationDAO = new NotificationDAO();
            
            NotificationDAO.Id = Notification.Id;
            NotificationDAO.Time = Notification.Time;
            NotificationDAO.UserId = Notification.UserId;
            NotificationDAO.Content = Notification.Content;
            NotificationDAO.URL = Notification.URL;
            NotificationDAO.Disabled = false;
            
            await ERPContext.Notification.AddAsync(NotificationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Notification Notification)
        {
            NotificationDAO NotificationDAO = ERPContext.Notification.Where(b => b.Id == Notification.Id).FirstOrDefault();
            
            NotificationDAO.Id = Notification.Id;
            NotificationDAO.Time = Notification.Time;
            NotificationDAO.UserId = Notification.UserId;
            NotificationDAO.Content = Notification.Content;
            NotificationDAO.URL = Notification.URL;
            NotificationDAO.Disabled = false;
            ERPContext.Notification.Update(NotificationDAO).Property(x => x.CX).IsModified = false;
            await ERPContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Guid Id)
        {
            NotificationDAO NotificationDAO = await ERPContext.Notification.Where(x => x.Id == Id).FirstOrDefaultAsync();
            NotificationDAO.Disabled = true;
            ERPContext.Notification.Update(NotificationDAO);
            await ERPContext.SaveChangesAsync();
            return true;
        }
    }
}
