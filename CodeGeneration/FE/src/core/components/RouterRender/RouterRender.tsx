import {IRoute} from 'core/IRoute';
import React from 'react';
import {Redirect, Route, RouteComponentProps, Switch, withRouter} from 'react-router-dom';
import './RouterRender.scss';

interface IRouterRenderProps extends RouteComponentProps {
  routes: IRoute[];
  children?: any[];
}

function RouterRender(props: IRouterRenderProps) {
  const {
    routes = [],
  } = props;
  return (
    <Switch>
      {routes.map((route: IRoute) => {
        if (route.path === '**' && route.redirectTo) {
          return (
            <Redirect to={route.redirectTo} exact={route.exact}/>
          );
        }
        return (
          <Route key={route.path} {...route}/>
        );
      })}
    </Switch>
  );
}

export default withRouter(RouterRender);
