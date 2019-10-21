
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Entities;
using WG.Repositories;

namespace WG.Services.MImageFile
{
    public interface IImageFileValidator : IServiceScoped
    {
        Task<bool> Create(ImageFile ImageFile);
        Task<bool> Update(ImageFile ImageFile);
        Task<bool> Delete(ImageFile ImageFile);
    }

    public class ImageFileValidator : IImageFileValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            StringEmpty,
            StringLimited,
        }

        private IUOW UOW;

        public ImageFileValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(ImageFile ImageFile)
        {
            ImageFileFilter ImageFileFilter = new ImageFileFilter
            {
                Skip = 0,
                Take = 10,
                Id = new LongFilter { Equal = ImageFile.Id },
                Selects = ImageFileSelect.Id
            };

            int count = await UOW.ImageFileRepository.Count(ImageFileFilter);

            if (count == 0)
                ImageFile.AddError(nameof(ImageFileValidator), nameof(ImageFile.Id), ErrorCode.IdNotExisted);

            return count == 1;
        }

        public async Task<bool> Create(ImageFile ImageFile)
        {
            return ImageFile.IsValidated;
        }

        public async Task<bool> Update(ImageFile ImageFile)
        {
            if (await ValidateId(ImageFile))
            {
            }
            return ImageFile.IsValidated;
        }

        public async Task<bool> Delete(ImageFile ImageFile)
        {
            if (await ValidateId(ImageFile))
            {
            }
            return ImageFile.IsValidated;
        }
    }
}
