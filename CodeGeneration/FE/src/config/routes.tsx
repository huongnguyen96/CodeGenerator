
import {IRoute} from 'core/IRoute';
import {lazy} from 'react';

import {ADMINISTRATOR_ROUTE} from './route-consts';
import {BRAND_ROUTE} from './route-consts';
import {CATEGORY_ROUTE} from './route-consts';
import {CUSTOMER_ROUTE} from './route-consts';
import {CUSTOMER_GROUPING_ROUTE} from './route-consts';
import {DISCOUNT_CUSTOMER_GROUPING_ROUTE} from './route-consts';
import {DISCOUNT_ROUTE} from './route-consts';
import {DISCOUNT_ITEM_ROUTE} from './route-consts';
import {DISTRICT_ROUTE} from './route-consts';
import {IMAGE_FILE_ROUTE} from './route-consts';
import {ITEM_ROUTE} from './route-consts';
import {ITEM_STATUS_ROUTE} from './route-consts';
import {ITEM_TYPE_ROUTE} from './route-consts';
import {ORDER_CONTENT_ROUTE} from './route-consts';
import {ORDER_ROUTE} from './route-consts';
import {PARTNER_ROUTE} from './route-consts';
import {PROVINCE_ROUTE} from './route-consts';
import {SHIPPING_ADDRESS_ROUTE} from './route-consts';
import {STOCK_ROUTE} from './route-consts';
import {UNIT_ROUTE} from './route-consts';
import {VARIATION_ROUTE} from './route-consts';
import {VARIATION_GROUPING_ROUTE} from './route-consts';
import {WARD_ROUTE} from './route-consts';
import {WAREHOUSE_ROUTE} from './route-consts';

const AdministratorView = lazy(() => import('views/Administrator/AdministratorView'));
const BrandView = lazy(() => import('views/Brand/BrandView'));
const CategoryView = lazy(() => import('views/Category/CategoryView'));
const CustomerView = lazy(() => import('views/Customer/CustomerView'));
const CustomerGroupingView = lazy(() => import('views/CustomerGrouping/CustomerGroupingView'));
const DiscountCustomerGroupingView = lazy(() => import('views/DiscountCustomerGrouping/DiscountCustomerGroupingView'));
const DiscountView = lazy(() => import('views/Discount/DiscountView'));
const DiscountItemView = lazy(() => import('views/DiscountItem/DiscountItemView'));
const DistrictView = lazy(() => import('views/District/DistrictView'));
const ImageFileView = lazy(() => import('views/ImageFile/ImageFileView'));
const ItemView = lazy(() => import('views/Item/ItemView'));
const ItemStatusView = lazy(() => import('views/ItemStatus/ItemStatusView'));
const ItemTypeView = lazy(() => import('views/ItemType/ItemTypeView'));
const OrderContentView = lazy(() => import('views/OrderContent/OrderContentView'));
const OrderView = lazy(() => import('views/Order/OrderView'));
const PartnerView = lazy(() => import('views/Partner/PartnerView'));
const ProvinceView = lazy(() => import('views/Province/ProvinceView'));
const ShippingAddressView = lazy(() => import('views/ShippingAddress/ShippingAddressView'));
const StockView = lazy(() => import('views/Stock/StockView'));
const UnitView = lazy(() => import('views/Unit/UnitView'));
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
    path: DISCOUNT_CUSTOMER_GROUPING_ROUTE,
    component: DiscountCustomerGroupingView,
  },
  {
    path: DISCOUNT_ROUTE,
    component: DiscountView,
  },
  {
    path: DISCOUNT_ITEM_ROUTE,
    component: DiscountItemView,
  },
  {
    path: DISTRICT_ROUTE,
    component: DistrictView,
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
    path: ITEM_STATUS_ROUTE,
    component: ItemStatusView,
  },
  {
    path: ITEM_TYPE_ROUTE,
    component: ItemTypeView,
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
    path: PARTNER_ROUTE,
    component: PartnerView,
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
    path: UNIT_ROUTE,
    component: UnitView,
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
