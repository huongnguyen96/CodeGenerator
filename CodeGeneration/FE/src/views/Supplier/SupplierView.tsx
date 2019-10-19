
import {SUPPLIER_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import SupplierDetail from './SupplierDetail';
import SupplierMaster from './SupplierMaster';
import './SupplierView.scss';

function SupplierView() {
  return (
    <Switch>
      <Route path={path.join(SUPPLIER_ROUTE)} exact component={SupplierMaster}/>
      <Route path={path.join(SUPPLIER_ROUTE, ':id')} component={SupplierDetail}/>
    </Switch>
  );
}

export default withRouter(SupplierView);
