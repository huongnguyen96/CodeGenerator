
import {MERCHANT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import MerchantDetail from './MerchantDetail';
import MerchantMaster from './MerchantMaster';
import './MerchantView.scss';

function MerchantView() {
  return (
    <Switch>
      <Route path={path.join(MERCHANT_ROUTE)} exact component={MerchantMaster}/>
      <Route path={path.join(MERCHANT_ROUTE, ':id')} component={MerchantDetail}/>
    </Switch>
  );
}

export default withRouter(MerchantView);
