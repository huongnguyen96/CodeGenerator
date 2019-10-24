
import {E_VOUCHER_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import EVoucherDetail from './EVoucherDetail';
import EVoucherMaster from './EVoucherMaster';
import './EVoucherView.scss';

function EVoucherView() {
  return (
    <Switch>
      <Route path={path.join(E_VOUCHER_ROUTE)} exact component={EVoucherMaster}/>
      <Route path={path.join(E_VOUCHER_ROUTE, ':id')} component={EVoucherDetail}/>
    </Switch>
  );
}

export default withRouter(EVoucherView);
