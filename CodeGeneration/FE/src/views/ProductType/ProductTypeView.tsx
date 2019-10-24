
import {PRODUCT_TYPE_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import ProductTypeDetail from './ProductTypeDetail';
import ProductTypeMaster from './ProductTypeMaster';
import './ProductTypeView.scss';

function ProductTypeView() {
  return (
    <Switch>
      <Route path={path.join(PRODUCT_TYPE_ROUTE)} exact component={ProductTypeMaster}/>
      <Route path={path.join(PRODUCT_TYPE_ROUTE, ':id')} component={ProductTypeDetail}/>
    </Switch>
  );
}

export default withRouter(ProductTypeView);
