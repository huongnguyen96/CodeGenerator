
import {PRODUCT_STATUS_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import ProductStatusDetail from './ProductStatusDetail';
import ProductStatusMaster from './ProductStatusMaster';
import './ProductStatusView.scss';

function ProductStatusView() {
  return (
    <Switch>
      <Route path={path.join(PRODUCT_STATUS_ROUTE)} exact component={ProductStatusMaster}/>
      <Route path={path.join(PRODUCT_STATUS_ROUTE, ':id')} component={ProductStatusDetail}/>
    </Switch>
  );
}

export default withRouter(ProductStatusView);
