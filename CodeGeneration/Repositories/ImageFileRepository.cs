
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
    public interface IImageFileRepository
    {
        Task<int> Count(ImageFileFilter ImageFileFilter);
        Task<List<ImageFile>> List(ImageFileFilter ImageFileFilter);
        Task<ImageFile> Get(long Id);
        Task<bool> Create(ImageFile ImageFile);
        Task<bool> Update(ImageFile ImageFile);
        Task<bool> Delete(ImageFile ImageFile);
        
    }
    public class ImageFileRepository : IImageFileRepository
    {
        private DataContext DataContext;
        private ICurrentContext CurrentContext;
        public ImageFileRepository(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        private IQueryable<ImageFileDAO> DynamicFilter(IQueryable<ImageFileDAO> query, ImageFileFilter filter)
        {
            if (filter == null) 
                return query.Where(q => false);
            
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Path != null)
                query = query.Where(q => q.Path, filter.Path);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Ids != null)
                query = query.Where(q => filter.Ids.Contains(q.Id));
            if (filter.ExceptIds != null)
                query = query.Where(q => !filter.ExceptIds.Contains(q.Id));
            return query;
        }
        private IQueryable<ImageFileDAO> DynamicOrder(IQueryable<ImageFileDAO> query,  ImageFileFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        
                        case ImageFileOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ImageFileOrder.Path:
                            query = query.OrderBy(q => q.Path);
                            break;
                        case ImageFileOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        
                        case ImageFileOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ImageFileOrder.Path:
                            query = query.OrderByDescending(q => q.Path);
                            break;
                        case ImageFileOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ImageFile>> DynamicSelect(IQueryable<ImageFileDAO> query, ImageFileFilter filter)
        {
            List <ImageFile> ImageFiles = await query.Select(q => new ImageFile()
            {
                
                Id = filter.Selects.Contains(ImageFileSelect.Id) ? q.Id : default(long),
                Path = filter.Selects.Contains(ImageFileSelect.Path) ? q.Path : default(string),
                Name = filter.Selects.Contains(ImageFileSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ImageFiles;
        }

        public async Task<int> Count(ImageFileFilter filter)
        {
            IQueryable <ImageFileDAO> ImageFileDAOs = DataContext.ImageFile;
            ImageFileDAOs = DynamicFilter(ImageFileDAOs, filter);
            return await ImageFileDAOs.CountAsync();
        }

        public async Task<List<ImageFile>> List(ImageFileFilter filter)
        {
            if (filter == null) return new List<ImageFile>();
            IQueryable<ImageFileDAO> ImageFileDAOs = DataContext.ImageFile;
            ImageFileDAOs = DynamicFilter(ImageFileDAOs, filter);
            ImageFileDAOs = DynamicOrder(ImageFileDAOs, filter);
            var ImageFiles = await DynamicSelect(ImageFileDAOs, filter);
            return ImageFiles;
        }

        
        public async Task<ImageFile> Get(long Id)
        {
            ImageFile ImageFile = await DataContext.ImageFile.Where(x => x.Id == Id).Select(ImageFileDAO => new ImageFile()
            {
                 
                Id = ImageFileDAO.Id,
                Path = ImageFileDAO.Path,
                Name = ImageFileDAO.Name,
            }).FirstOrDefaultAsync();
            return ImageFile;
        }

        public async Task<bool> Create(ImageFile ImageFile)
        {
            ImageFileDAO ImageFileDAO = new ImageFileDAO();
            
            ImageFileDAO.Id = ImageFile.Id;
            ImageFileDAO.Path = ImageFile.Path;
            ImageFileDAO.Name = ImageFile.Name;
            
            await DataContext.ImageFile.AddAsync(ImageFileDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        
        public async Task<bool> Update(ImageFile ImageFile)
        {
            ImageFileDAO ImageFileDAO = DataContext.ImageFile.Where(x => x.Id == ImageFile.Id).FirstOrDefault();
            
            ImageFileDAO.Id = ImageFile.Id;
            ImageFileDAO.Path = ImageFile.Path;
            ImageFileDAO.Name = ImageFile.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Delete(ImageFile ImageFile)
        {
            ImageFileDAO ImageFileDAO = await DataContext.ImageFile.Where(x => x.Id == ImageFile.Id).FirstOrDefaultAsync();
            DataContext.ImageFile.Remove(ImageFileDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
