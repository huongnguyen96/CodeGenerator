
import {ORDER_STATUS_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import OrderStatusDetail from './OrderStatusDetail';
import OrderStatusMaster from './OrderStatusMaster';
import './OrderStatusView.scss';

function OrderStatusView() {
  return (
    <Switch>
      <Route path={path.join(ORDER_STATUS_ROUTE)} exact component={OrderStatusMaster}/>
      <Route path={path.join(ORDER_STATUS_ROUTE, ':id')} component={OrderStatusDetail}/>
    </Switch>
  );
}

export default withRouter(OrderStatusView);
