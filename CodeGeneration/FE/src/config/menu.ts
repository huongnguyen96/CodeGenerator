
import { IRoute } from 'core';
import { marker as translate } from 'helpers/translate';

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
    title: translate('discountContent.title'),
    path: DISCOUNT_CONTENT_ROUTE,
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
    title: translate('district.title'),
    path: DISTRICT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('eVoucherContent.title'),
    path: E_VOUCHER_CONTENT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('eVoucher.title'),
    path: E_VOUCHER_ROUTE,
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
    title: translate('merchantAddress.title'),
    path: MERCHANT_ADDRESS_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('merchant.title'),
    path: MERCHANT_ROUTE,
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
    title: translate('orderStatus.title'),
    path: ORDER_STATUS_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('paymentMethod.title'),
    path: PAYMENT_METHOD_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('product.title'),
    path: PRODUCT_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('productStatus.title'),
    path: PRODUCT_STATUS_ROUTE,
    icon: 'dashboard',
    exact: false,
  },
  {
    title: translate('productType.title'),
    path: PRODUCT_TYPE_ROUTE,
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
