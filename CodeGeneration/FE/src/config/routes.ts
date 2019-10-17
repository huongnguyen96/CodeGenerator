import {IRoute} from 'core/IRoute';
import Districts from 'views/Admin/Districts';
import Dashboard from 'views/Dashboard';
import {
  ADMIN_DISTRICTS_ROUTE,
  HOME_ROUTE,
} from './route-consts';

export const routes: IRoute[] = [
  {
    path: HOME_ROUTE,
    component: Dashboard,
    exact: true,
  },
  {
    path: ADMIN_DISTRICTS_ROUTE,
    component: Districts,
  },
];
