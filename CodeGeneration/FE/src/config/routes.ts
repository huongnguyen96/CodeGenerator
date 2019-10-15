import {IRoute} from 'core/IRoute';
import Provinces from 'views/Admin/Provinces';
import Dashboard from 'views/Dashboard';

export const routes: IRoute[] = [
  {
    path: '/',
    component: Dashboard,
    exact: true,
  },
  {
    path: '/admin/provinces',
    component: Provinces,
  },
];
