
import {PRODUCT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import ProductDetail from './ProductDetail';
import ProductMaster from './ProductMaster';
import './ProductView.scss';

function ProductView() {
  return (
    <Switch>
      <Route path={path.join(PRODUCT_ROUTE)} exact component={ProductMaster}/>
      <Route path={path.join(PRODUCT_ROUTE, ':id')} component={ProductDetail}/>
    </Switch>
  );
}

export default withRouter(ProductView);
