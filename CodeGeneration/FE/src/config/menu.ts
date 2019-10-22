import { IRoute } from 'core';
import { marker as translate } from 'helpers/translate';

import { BRAND_ROUTE } from 'config/route-consts';
export const menu: IRoute[] = [
    {
        title: translate('brand.title'),
        path: BRAND_ROUTE,
        icon: 'dashboard',
        exact: false,
    },
];
