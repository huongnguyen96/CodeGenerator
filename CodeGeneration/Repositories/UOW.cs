
using Common;
using System.Threading.Tasks;
using CodeGeneration.Repositories.Models;

namespace WeGift.Repositories
{
    public interface IUOW : IServiceScoped
    {
        Task Begin();
        Task Commit();
        Task Rollback();
        IAuditLogRepository AuditLogRepository { get; }
        ISystemLogRepository SystemLogRepository { get; }
        
        ICategoryRepository CategoryRepository { get; }

        ICategory_ItemRepository Category_ItemRepository { get; }

        IItemRepository ItemRepository { get; }

        IUserRepository UserRepository { get; }

        IWarehouseRepository WarehouseRepository { get; }

    }
    public class UOW : IUOW
    {
        private WGContext WGContext;
        public IAuditLogRepository AuditLogRepository { get; private set; }
        public ISystemLogRepository SystemLogRepository { get; private set; }
        
        public ICategoryRepository CategoryRepository { get; private set; }

        public ICategory_ItemRepository Category_ItemRepository { get; private set; }

        public IItemRepository ItemRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IWarehouseRepository WarehouseRepository { get; private set; }


        public UOW(WGContext WGContext, ICurrentContext CurrentContext)
        {
            this.WGContext = WGContext;
            AuditLogRepository = new AuditLogRepository(CurrentContext);
            SystemLogRepository = new SystemLogRepository(CurrentContext);
            
            CategoryRepository = new CategoryRepository(WGContext, CurrentContext);

            Category_ItemRepository = new Category_ItemRepository(WGContext, CurrentContext);

            ItemRepository = new ItemRepository(WGContext, CurrentContext);

            UserRepository = new UserRepository(WGContext, CurrentContext);

            WarehouseRepository = new WarehouseRepository(WGContext, CurrentContext);

        }
        public async Task Begin()
        {
            await WGContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            WGContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            WGContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}
