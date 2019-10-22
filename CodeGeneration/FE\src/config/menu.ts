
import { IRoute } from 'core';
import { marker as translate } from 'helpers/translate';

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

export const menu: IRoute[] = [
    
  {
    title: translate('administrator.title'),
    path: ADMINISTRATOR_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('brand.title'),
    path: BRAND_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('category.title'),
    path: CATEGORY_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('customer.title'),
    path: CUSTOMER_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('customerGrouping.title'),
    path: CUSTOMER_GROUPING_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('discountCustomerGrouping.title'),
    path: DISCOUNT_CUSTOMER_GROUPING_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('discount.title'),
    path: DISCOUNT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('discountItem.title'),
    path: DISCOUNT_ITEM_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('district.title'),
    path: DISTRICT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('imageFile.title'),
    path: IMAGE_FILE_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('item.title'),
    path: ITEM_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('itemStatus.title'),
    path: ITEM_STATUS_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('itemType.title'),
    path: ITEM_TYPE_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('orderContent.title'),
    path: ORDER_CONTENT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('order.title'),
    path: ORDER_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('partner.title'),
    path: PARTNER_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('province.title'),
    path: PROVINCE_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('shippingAddress.title'),
    path: SHIPPING_ADDRESS_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('stock.title'),
    path: STOCK_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('unit.title'),
    path: UNIT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('variation.title'),
    path: VARIATION_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('variationGrouping.title'),
    path: VARIATION_GROUPING_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('ward.title'),
    path: WARD_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('warehouse.title'),
    path: WAREHOUSE_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
];
