
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
        
        IAdministratorRepository AdministratorRepository { get; }

        IBrandRepository BrandRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        ICustomerGroupingRepository CustomerGroupingRepository { get; }

        ICustomer_CustomerGroupingRepository Customer_CustomerGroupingRepository { get; }

        IDiscountCustomerGroupingRepository DiscountCustomerGroupingRepository { get; }

        IDiscountRepository DiscountRepository { get; }

        IDiscountItemRepository DiscountItemRepository { get; }

        IDistrictRepository DistrictRepository { get; }

        IImageFileRepository ImageFileRepository { get; }

        IItemRepository ItemRepository { get; }

        IItemStatusRepository ItemStatusRepository { get; }

        IItemTypeRepository ItemTypeRepository { get; }

        IOrderContentRepository OrderContentRepository { get; }

        IOrderRepository OrderRepository { get; }

        IPartnerRepository PartnerRepository { get; }

        IProvinceRepository ProvinceRepository { get; }

        IShippingAddressRepository ShippingAddressRepository { get; }

        IStockRepository StockRepository { get; }

        IUnitRepository UnitRepository { get; }

        IVariationRepository VariationRepository { get; }

        IVariationGroupingRepository VariationGroupingRepository { get; }

        IWardRepository WardRepository { get; }

        IWarehouseRepository WarehouseRepository { get; }

    }
    public class UOW : IUOW
    {
        private DataContext DataContext;
        public IAuditLogRepository AuditLogRepository { get; private set; }
        public ISystemLogRepository SystemLogRepository { get; private set; }
        
        public IAdministratorRepository AdministratorRepository { get; private set; }

        public IBrandRepository BrandRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public ICustomerGroupingRepository CustomerGroupingRepository { get; private set; }

        public ICustomer_CustomerGroupingRepository Customer_CustomerGroupingRepository { get; private set; }

        public IDiscountCustomerGroupingRepository DiscountCustomerGroupingRepository { get; private set; }

        public IDiscountRepository DiscountRepository { get; private set; }

        public IDiscountItemRepository DiscountItemRepository { get; private set; }

        public IDistrictRepository DistrictRepository { get; private set; }

        public IImageFileRepository ImageFileRepository { get; private set; }

        public IItemRepository ItemRepository { get; private set; }

        public IItemStatusRepository ItemStatusRepository { get; private set; }

        public IItemTypeRepository ItemTypeRepository { get; private set; }

        public IOrderContentRepository OrderContentRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IPartnerRepository PartnerRepository { get; private set; }

        public IProvinceRepository ProvinceRepository { get; private set; }

        public IShippingAddressRepository ShippingAddressRepository { get; private set; }

        public IStockRepository StockRepository { get; private set; }

        public IUnitRepository UnitRepository { get; private set; }

        public IVariationRepository VariationRepository { get; private set; }

        public IVariationGroupingRepository VariationGroupingRepository { get; private set; }

        public IWardRepository WardRepository { get; private set; }

        public IWarehouseRepository WarehouseRepository { get; private set; }


        public UOW(DataContext DataContext, ICurrentContext CurrentContext)
        {
            this.DataContext = DataContext;
            AuditLogRepository = new AuditLogRepository(CurrentContext);
            SystemLogRepository = new SystemLogRepository(CurrentContext);
            
            AdministratorRepository = new AdministratorRepository(DataContext, CurrentContext);

            BrandRepository = new BrandRepository(DataContext, CurrentContext);

            CategoryRepository = new CategoryRepository(DataContext, CurrentContext);

            CustomerRepository = new CustomerRepository(DataContext, CurrentContext);

            CustomerGroupingRepository = new CustomerGroupingRepository(DataContext, CurrentContext);

            Customer_CustomerGroupingRepository = new Customer_CustomerGroupingRepository(DataContext, CurrentContext);

            DiscountCustomerGroupingRepository = new DiscountCustomerGroupingRepository(DataContext, CurrentContext);

            DiscountRepository = new DiscountRepository(DataContext, CurrentContext);

            DiscountItemRepository = new DiscountItemRepository(DataContext, CurrentContext);

            DistrictRepository = new DistrictRepository(DataContext, CurrentContext);

            ImageFileRepository = new ImageFileRepository(DataContext, CurrentContext);

            ItemRepository = new ItemRepository(DataContext, CurrentContext);

            ItemStatusRepository = new ItemStatusRepository(DataContext, CurrentContext);

            ItemTypeRepository = new ItemTypeRepository(DataContext, CurrentContext);

            OrderContentRepository = new OrderContentRepository(DataContext, CurrentContext);

            OrderRepository = new OrderRepository(DataContext, CurrentContext);

            PartnerRepository = new PartnerRepository(DataContext, CurrentContext);

            ProvinceRepository = new ProvinceRepository(DataContext, CurrentContext);

            ShippingAddressRepository = new ShippingAddressRepository(DataContext, CurrentContext);

            StockRepository = new StockRepository(DataContext, CurrentContext);

            UnitRepository = new UnitRepository(DataContext, CurrentContext);

            VariationRepository = new VariationRepository(DataContext, CurrentContext);

            VariationGroupingRepository = new VariationGroupingRepository(DataContext, CurrentContext);

            WardRepository = new WardRepository(DataContext, CurrentContext);

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
