
import {CUSTOMER_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import CustomerDetail from './CustomerDetail';
import CustomerMaster from './CustomerMaster';
import './CustomerView.scss';

function CustomerView() {
  return (
    <Switch>
      <Route path={path.join(CUSTOMER_ROUTE)} exact component={CustomerMaster}/>
      <Route path={path.join(CUSTOMER_ROUTE, ':id')} component={CustomerDetail}/>
    </Switch>
  );
}

export default withRouter(CustomerView);
