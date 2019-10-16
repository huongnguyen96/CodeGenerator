
using Common;
using System.Threading.Tasks;
using CodeGeneration.Repositories.Models;

namespace WG.Repositories
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
        private DataContext DataContext;
        public IAuditLogRepository AuditLogRepository { get; private set; }
        public ISystemLogRepository SystemLogRepository { get; private set; }
        
        public ICategoryRepository CategoryRepository { get; private set; }

        public ICategory_ItemRepository Category_ItemRepository { get; private set; }

        public IItemRepository ItemRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IWarehouseRepository WarehouseRepository { get; private set; }


        public UOW(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            AuditLogRepository = new AuditLogRepository(CurrentContext);
            SystemLogRepository = new SystemLogRepository(CurrentContext);
            
            CategoryRepository = new CategoryRepository(DataContext, CurrentContext);

            Category_ItemRepository = new Category_ItemRepository(DataContext, CurrentContext);

            ItemRepository = new ItemRepository(DataContext, CurrentContext);

            UserRepository = new UserRepository(DataContext, CurrentContext);

            WarehouseRepository = new WarehouseRepository(DataContext, CurrentContext);

        }
        public async Task Begin()
        {
            await DataContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            DataContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            DataContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}
