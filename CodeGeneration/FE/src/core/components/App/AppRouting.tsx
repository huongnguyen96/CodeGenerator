import RouterRender from 'core/components/RouterRender';
import {IRoute} from 'core/IRoute';
import React, {FC, lazy, Suspense} from 'react';
import {withRouter} from 'react-router-dom';

const Page = lazy(() => import('views/Pages/ErrorPage'));
const DefaultLayout = lazy(() => import('layouts/DefaultLayout'));
const Login = lazy(() => import('views/Pages/Login'));
const Registration = lazy(() => import('views/Pages/Registration'));

const Page500: FC = () => (
  <Page code={500}/>
);

const Page404: FC = () => (
  <Page code={404}/>
);

const Page403: FC = () => (
  <Page code={403}/>
);

const Page401: FC = () => (
  <Page code={401}/>
);

export const routes: IRoute[] = [
  {
    path: '/login',
    component: Login,
  },
  {
    path: '/register',
    component: Registration,
  },
  {
    path: '/401',
    component: Page401,
  },
  {
    path: '/403',
    component: Page403,
  },
  {
    path: '/404',
    component: Page404,
  },
  {
    path: '/500',
    component: Page500,
  },
  {
    path: '/',
    component: DefaultLayout,
    children: [],
  },
];

function AppRouting() {
  return (
    <Suspense fallback={null}>
      <RouterRender routes={routes}/>
    </Suspense>
  );
}

export default withRouter(AppRouting);
