
import {E_VOUCHER_CONTENT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import EVoucherContentDetail from './EVoucherContentDetail';
import EVoucherContentMaster from './EVoucherContentMaster';
import './EVoucherContentView.scss';

function EVoucherContentView() {
  return (
    <Switch>
      <Route path={path.join(E_VOUCHER_CONTENT_ROUTE)} exact component={EVoucherContentMaster}/>
      <Route path={path.join(E_VOUCHER_CONTENT_ROUTE, ':id')} component={EVoucherContentDetail}/>
    </Switch>
  );
}

export default withRouter(EVoucherContentView);
