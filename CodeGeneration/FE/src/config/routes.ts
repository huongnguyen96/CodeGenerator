
import {IRoute} from 'core/IRoute';
import {lazy} from 'react';

import {ADMINISTRATOR_ROUTE} from './route-consts';
import {BRAND_ROUTE} from './route-consts';
import {CATEGORY_ROUTE} from './route-consts';
import {CUSTOMER_ROUTE} from './route-consts';
import {CUSTOMER_GROUPING_ROUTE} from './route-consts';
import {DISCOUNT_CONTENT_ROUTE} from './route-consts';
import {DISCOUNT_ROUTE} from './route-consts';
import {DISTRICT_ROUTE} from './route-consts';
import {E_VOUCHER_CONTENT_ROUTE} from './route-consts';
import {E_VOUCHER_ROUTE} from './route-consts';
import {IMAGE_FILE_ROUTE} from './route-consts';
import {ITEM_ROUTE} from './route-consts';
import {MERCHANT_ADDRESS_ROUTE} from './route-consts';
import {MERCHANT_ROUTE} from './route-consts';
import {ORDER_CONTENT_ROUTE} from './route-consts';
import {ORDER_ROUTE} from './route-consts';
import {ORDER_STATUS_ROUTE} from './route-consts';
import {PAYMENT_METHOD_ROUTE} from './route-consts';
import {PRODUCT_ROUTE} from './route-consts';
import {PRODUCT_STATUS_ROUTE} from './route-consts';
import {PRODUCT_TYPE_ROUTE} from './route-consts';
import {PROVINCE_ROUTE} from './route-consts';
import {SHIPPING_ADDRESS_ROUTE} from './route-consts';
import {STOCK_ROUTE} from './route-consts';
import {VARIATION_ROUTE} from './route-consts';
import {VARIATION_GROUPING_ROUTE} from './route-consts';
import {WARD_ROUTE} from './route-consts';
import {WAREHOUSE_ROUTE} from './route-consts';

const AdministratorView = lazy(() => import('views/Administrator/AdministratorView'));
const BrandView = lazy(() => import('views/Brand/BrandView'));
const CategoryView = lazy(() => import('views/Category/CategoryView'));
const CustomerView = lazy(() => import('views/Customer/CustomerView'));
const CustomerGroupingView = lazy(() => import('views/CustomerGrouping/CustomerGroupingView'));
const DiscountContentView = lazy(() => import('views/DiscountContent/DiscountContentView'));
const DiscountView = lazy(() => import('views/Discount/DiscountView'));
const DistrictView = lazy(() => import('views/District/DistrictView'));
const EVoucherContentView = lazy(() => import('views/EVoucherContent/EVoucherContentView'));
const EVoucherView = lazy(() => import('views/EVoucher/EVoucherView'));
const ImageFileView = lazy(() => import('views/ImageFile/ImageFileView'));
const ItemView = lazy(() => import('views/Item/ItemView'));
const MerchantAddressView = lazy(() => import('views/MerchantAddress/MerchantAddressView'));
const MerchantView = lazy(() => import('views/Merchant/MerchantView'));
const OrderContentView = lazy(() => import('views/OrderContent/OrderContentView'));
const OrderView = lazy(() => import('views/Order/OrderView'));
const OrderStatusView = lazy(() => import('views/OrderStatus/OrderStatusView'));
const PaymentMethodView = lazy(() => import('views/PaymentMethod/PaymentMethodView'));
const ProductView = lazy(() => import('views/Product/ProductView'));
const ProductStatusView = lazy(() => import('views/ProductStatus/ProductStatusView'));
const ProductTypeView = lazy(() => import('views/ProductType/ProductTypeView'));
const ProvinceView = lazy(() => import('views/Province/ProvinceView'));
const ShippingAddressView = lazy(() => import('views/ShippingAddress/ShippingAddressView'));
const StockView = lazy(() => import('views/Stock/StockView'));
const VariationView = lazy(() => import('views/Variation/VariationView'));
const VariationGroupingView = lazy(() => import('views/VariationGrouping/VariationGroupingView'));
const WardView = lazy(() => import('views/Ward/WardView'));
const WarehouseView = lazy(() => import('views/Warehouse/WarehouseView'));

export const routes: IRoute[] = [

  {
    path: ADMINISTRATOR_ROUTE,
    component: AdministratorView,
  },
  {
    path: BRAND_ROUTE,
    component: BrandView,
  },
  {
    path: CATEGORY_ROUTE,
    component: CategoryView,
  },
  {
    path: CUSTOMER_ROUTE,
    component: CustomerView,
  },
  {
    path: CUSTOMER_GROUPING_ROUTE,
    component: CustomerGroupingView,
  },
  {
    path: DISCOUNT_CONTENT_ROUTE,
    component: DiscountContentView,
  },
  {
    path: DISCOUNT_ROUTE,
    component: DiscountView,
  },
  {
    path: DISTRICT_ROUTE,
    component: DistrictView,
  },
  {
    path: E_VOUCHER_CONTENT_ROUTE,
    component: EVoucherContentView,
  },
  {
    path: E_VOUCHER_ROUTE,
    component: EVoucherView,
  },
  {
    path: IMAGE_FILE_ROUTE,
    component: ImageFileView,
  },
  {
    path: ITEM_ROUTE,
    component: ItemView,
  },
  {
    path: MERCHANT_ADDRESS_ROUTE,
    component: MerchantAddressView,
  },
  {
    path: MERCHANT_ROUTE,
    component: MerchantView,
  },
  {
    path: ORDER_CONTENT_ROUTE,
    component: OrderContentView,
  },
  {
    path: ORDER_ROUTE,
    component: OrderView,
  },
  {
    path: ORDER_STATUS_ROUTE,
    component: OrderStatusView,
  },
  {
    path: PAYMENT_METHOD_ROUTE,
    component: PaymentMethodView,
  },
  {
    path: PRODUCT_ROUTE,
    component: ProductView,
  },
  {
    path: PRODUCT_STATUS_ROUTE,
    component: ProductStatusView,
  },
  {
    path: PRODUCT_TYPE_ROUTE,
    component: ProductTypeView,
  },
  {
    path: PROVINCE_ROUTE,
    component: ProvinceView,
  },
  {
    path: SHIPPING_ADDRESS_ROUTE,
    component: ShippingAddressView,
  },
  {
    path: STOCK_ROUTE,
    component: StockView,
  },
  {
    path: VARIATION_ROUTE,
    component: VariationView,
  },
  {
    path: VARIATION_GROUPING_ROUTE,
    component: VariationGroupingView,
  },
  {
    path: WARD_ROUTE,
    component: WardView,
  },
  {
    path: WAREHOUSE_ROUTE,
    component: WarehouseView,
  },
];
 
