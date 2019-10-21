
import {ORDER_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import OrderDetail from './OrderDetail';
import OrderMaster from './OrderMaster';
import './OrderView.scss';

function OrderView() {
  return (
    <Switch>
      <Route path={path.join(ORDER_ROUTE)} exact component={OrderMaster}/>
      <Route path={path.join(ORDER_ROUTE, ':id')} component={OrderDetail}/>
    </Switch>
  );
}

export default withRouter(OrderView);
