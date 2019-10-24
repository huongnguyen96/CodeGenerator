
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

        IDiscountContentRepository DiscountContentRepository { get; }

        IDiscountRepository DiscountRepository { get; }

        IDiscount_CustomerGroupingRepository Discount_CustomerGroupingRepository { get; }

        IDistrictRepository DistrictRepository { get; }

        IEVoucherContentRepository EVoucherContentRepository { get; }

        IEVoucherRepository EVoucherRepository { get; }

        IImageFileRepository ImageFileRepository { get; }

        IItemRepository ItemRepository { get; }

        IMerchantAddressRepository MerchantAddressRepository { get; }

        IMerchantRepository MerchantRepository { get; }

        IOrderContentRepository OrderContentRepository { get; }

        IOrderRepository OrderRepository { get; }

        IOrderStatusRepository OrderStatusRepository { get; }

        IPaymentMethodRepository PaymentMethodRepository { get; }

        IProductRepository ProductRepository { get; }

        IProductStatusRepository ProductStatusRepository { get; }

        IProductTypeRepository ProductTypeRepository { get; }

        IProduct_MerchantAddressRepository Product_MerchantAddressRepository { get; }

        IProduct_PaymentMethodRepository Product_PaymentMethodRepository { get; }

        IProvinceRepository ProvinceRepository { get; }

        IShippingAddressRepository ShippingAddressRepository { get; }

        IStockRepository StockRepository { get; }

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

        public IDiscountContentRepository DiscountContentRepository { get; private set; }

        public IDiscountRepository DiscountRepository { get; private set; }

        public IDiscount_CustomerGroupingRepository Discount_CustomerGroupingRepository { get; private set; }

        public IDistrictRepository DistrictRepository { get; private set; }

        public IEVoucherContentRepository EVoucherContentRepository { get; private set; }

        public IEVoucherRepository EVoucherRepository { get; private set; }

        public IImageFileRepository ImageFileRepository { get; private set; }

        public IItemRepository ItemRepository { get; private set; }

        public IMerchantAddressRepository MerchantAddressRepository { get; private set; }

        public IMerchantRepository MerchantRepository { get; private set; }

        public IOrderContentRepository OrderContentRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IOrderStatusRepository OrderStatusRepository { get; private set; }

        public IPaymentMethodRepository PaymentMethodRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IProductStatusRepository ProductStatusRepository { get; private set; }

        public IProductTypeRepository ProductTypeRepository { get; private set; }

        public IProduct_MerchantAddressRepository Product_MerchantAddressRepository { get; private set; }

        public IProduct_PaymentMethodRepository Product_PaymentMethodRepository { get; private set; }

        public IProvinceRepository ProvinceRepository { get; private set; }

        public IShippingAddressRepository ShippingAddressRepository { get; private set; }

        public IStockRepository StockRepository { get; private set; }

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

            DiscountContentRepository = new DiscountContentRepository(DataContext, CurrentContext);

            DiscountRepository = new DiscountRepository(DataContext, CurrentContext);

            Discount_CustomerGroupingRepository = new Discount_CustomerGroupingRepository(DataContext, CurrentContext);

            DistrictRepository = new DistrictRepository(DataContext, CurrentContext);

            EVoucherContentRepository = new EVoucherContentRepository(DataContext, CurrentContext);

            EVoucherRepository = new EVoucherRepository(DataContext, CurrentContext);

            ImageFileRepository = new ImageFileRepository(DataContext, CurrentContext);

            ItemRepository = new ItemRepository(DataContext, CurrentContext);

            MerchantAddressRepository = new MerchantAddressRepository(DataContext, CurrentContext);

            MerchantRepository = new MerchantRepository(DataContext, CurrentContext);

            OrderContentRepository = new OrderContentRepository(DataContext, CurrentContext);

            OrderRepository = new OrderRepository(DataContext, CurrentContext);

            OrderStatusRepository = new OrderStatusRepository(DataContext, CurrentContext);

            PaymentMethodRepository = new PaymentMethodRepository(DataContext, CurrentContext);

            ProductRepository = new ProductRepository(DataContext, CurrentContext);

            ProductStatusRepository = new ProductStatusRepository(DataContext, CurrentContext);

            ProductTypeRepository = new ProductTypeRepository(DataContext, CurrentContext);

            Product_MerchantAddressRepository = new Product_MerchantAddressRepository(DataContext, CurrentContext);

            Product_PaymentMethodRepository = new Product_PaymentMethodRepository(DataContext, CurrentContext);

            ProvinceRepository = new ProvinceRepository(DataContext, CurrentContext);

            ShippingAddressRepository = new ShippingAddressRepository(DataContext, CurrentContext);

            StockRepository = new StockRepository(DataContext, CurrentContext);

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
