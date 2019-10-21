
using Common;
using WG.Entities;
using WG.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WG.Services.MUnit
{
    public interface IUnitService : IServiceScoped
    {
        Task<int> Count(UnitFilter UnitFilter);
        Task<List<Unit>> List(UnitFilter UnitFilter);
        Task<Unit> Get(long Id);
        Task<Unit> Create(Unit Unit);
        Task<Unit> Update(Unit Unit);
        Task<Unit> Delete(Unit Unit);
    }

    public class UnitService : IUnitService
    {
        public IUOW UOW;
        public IUnitValidator UnitValidator;

        public UnitService(
            IUOW UOW, 
            IUnitValidator UnitValidator
        )
        {
            this.UOW = UOW;
            this.UnitValidator = UnitValidator;
        }
        public async Task<int> Count(UnitFilter UnitFilter)
        {
            int result = await UOW.UnitRepository.Count(UnitFilter);
            return result;
        }

        public async Task<List<Unit>> List(UnitFilter UnitFilter)
        {
            List<Unit> Units = await UOW.UnitRepository.List(UnitFilter);
            return Units;
        }

        public async Task<Unit> Get(long Id)
        {
            Unit Unit = await UOW.UnitRepository.Get(Id);
            if (Unit == null)
                return null;
            return Unit;
        }

        public async Task<Unit> Create(Unit Unit)
        {
            if (!await UnitValidator.Create(Unit))
                return Unit;

            try
            {
               
                await UOW.Begin();
                await UOW.UnitRepository.Create(Unit);
                await UOW.Commit();

                await UOW.AuditLogRepository.Create(Unit, "", nameof(UnitService));
                return await UOW.UnitRepository.Get(Unit.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(UnitService));
                throw new MessageException(ex);
            }
        }

        public async Task<Unit> Update(Unit Unit)
        {
            if (!await UnitValidator.Update(Unit))
                return Unit;
            try
            {
                var oldData = await UOW.UnitRepository.Get(Unit.Id);

                await UOW.Begin();
                await UOW.UnitRepository.Update(Unit);
                await UOW.Commit();

                var newData = await UOW.UnitRepository.Get(Unit.Id);
                await UOW.AuditLogRepository.Create(newData, oldData, nameof(UnitService));
                return newData;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(UnitService));
                throw new MessageException(ex);
            }
        }

        public async Task<Unit> Delete(Unit Unit)
        {
            if (!await UnitValidator.Delete(Unit))
                return Unit;

            try
            {
                await UOW.Begin();
                await UOW.UnitRepository.Delete(Unit);
                await UOW.Commit();
                await UOW.AuditLogRepository.Create("", Unit, nameof(UnitService));
                return Unit;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                await UOW.SystemLogRepository.Create(ex, nameof(UnitService));
                throw new MessageException(ex);
            }
        }
    }
}
