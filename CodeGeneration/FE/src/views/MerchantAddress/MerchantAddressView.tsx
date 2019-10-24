
import {MERCHANT_ADDRESS_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import MerchantAddressDetail from './MerchantAddressDetail';
import MerchantAddressMaster from './MerchantAddressMaster';
import './MerchantAddressView.scss';

function MerchantAddressView() {
  return (
    <Switch>
      <Route path={path.join(MERCHANT_ADDRESS_ROUTE)} exact component={MerchantAddressMaster}/>
      <Route path={path.join(MERCHANT_ADDRESS_ROUTE, ':id')} component={MerchantAddressDetail}/>
    </Switch>
  );
}

export default withRouter(MerchantAddressView);
