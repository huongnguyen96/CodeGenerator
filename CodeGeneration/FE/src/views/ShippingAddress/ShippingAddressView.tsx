
import {SHIPPING_ADDRESS_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import ShippingAddressDetail from './ShippingAddressDetail';
import ShippingAddressMaster from './ShippingAddressMaster';
import './ShippingAddressView.scss';

function ShippingAddressView() {
  return (
    <Switch>
      <Route path={path.join(SHIPPING_ADDRESS_ROUTE)} exact component={ShippingAddressMaster}/>
      <Route path={path.join(SHIPPING_ADDRESS_ROUTE, ':id')} component={ShippingAddressDetail}/>
    </Switch>
  );
}

export default withRouter(ShippingAddressView);
