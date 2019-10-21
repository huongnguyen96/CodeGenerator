
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MImageFile
{
    public interface IImageFileService : IServiceScoped
    {
        Task<int> Count(ImageFileFilter ImageFileFilter);
        Task<List<ImageFile>> List(ImageFileFilter ImageFileFilter);
        Task<ImageFile> Get(long Id);
        Task<ImageFile> Create(ImageFile ImageFile);
        Task<ImageFile> Update(ImageFile ImageFile);
        Task<ImageFile> Delete(ImageFile ImageFile);
    }

    public class ImageFileService : IImageFileService
    {
        public IUOW UOW;
        public IImageFileValidator ImageFileValidator;

        public ImageFileService(
            IUOW UOW, 
            IImageFileValidator ImageFileValidator
        )
        {
            this.UOW = UOW;
            this.ImageFileValidator = ImageFileValidator;
        }
        public async Task<int> Count(ImageFileFilter ImageFileFilter)
        {
            int result = await UOW.ImageFileRepository.Count(ImageFileFilter);
            return result;
        }

        public async Task<List<ImageFile>> List(ImageFileFilter ImageFileFilter)
        {
            List<ImageFile> ImageFiles = await UOW.ImageFileRepository.List(ImageFileFilter);
            return ImageFiles;
        }

        public async Task<ImageFile> Get(long Id)
        {
            ImageFile ImageFile = await UOW.ImageFileRepository.Get(Id);
            if (ImageFile == null)
                return null;
            return ImageFile;
        }

        public async Task<ImageFile> Create(ImageFile ImageFile)
        {
            if (!await ImageFileValidator.Create(ImageFile))
                return ImageFile;

            try
            {
               
                await UOW.Begin();
                await UOW.ImageFileRepository.Create(ImageFile);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(ImageFile, "", nameof(ImageFileService));
                return await UOW.ImageFileRepository.Get(ImageFile.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ImageFileService));
                throw new MessageException(ex);
            }
        }

        public async Task<ImageFile> Update(ImageFile ImageFile)
        {
            if (!await ImageFileValidator.Update(ImageFile))
                return ImageFile;
            try
            {
                var oldData = await UOW.ImageFileRepository.Get(ImageFile.Id);

                await UOW.Begin();
                await UOW.ImageFileRepository.Update(ImageFile);
                await UOW.Commit();

                var newData = await UOW.ImageFileRepository.Get(ImageFile.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(ImageFileService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ImageFileService));
                throw new MessageException(ex);
            }
        }

        public async Task<ImageFile> Delete(ImageFile ImageFile)
        {
            if (!await ImageFileValidator.Delete(ImageFile))
                return ImageFile;

            try
            {
                await UOW.Begin();
                await UOW.ImageFileRepository.Delete(ImageFile);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", ImageFile, nameof(ImageFileService));
                return ImageFile;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(ImageFileService));
                throw new MessageException(ex);
            }
        }
    }
}
