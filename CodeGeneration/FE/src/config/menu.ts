import { BRAND_ROUTE } from 'config/route-consts';
import { IRoute } from 'core';
import { marker as translate } from 'helpers/translate';

export const menu: IRoute[] = [
    {
        title: translate('brands.title'),
        path: BRAND_ROUTE,
        icon: 'dashboard',
        exact: false,
    },
];
