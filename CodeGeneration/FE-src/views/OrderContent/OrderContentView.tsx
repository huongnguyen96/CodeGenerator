
import {ORDER_CONTENT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import OrderContentDetail from './OrderContentDetail';
import OrderContentMaster from './OrderContentMaster';
import './OrderContentView.scss';

function OrderContentView() {
  return (
    <Switch>
      <Route path={path.join(ORDER_CONTENT_ROUTE)} exact component={OrderContentMaster}/>
      <Route path={path.join(ORDER_CONTENT_ROUTE, ':id')} component={OrderContentDetail}/>
    </Switch>
  );
}

export default withRouter(OrderContentView);
