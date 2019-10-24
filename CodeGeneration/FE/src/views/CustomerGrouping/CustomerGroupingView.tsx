
import {CUSTOMER_GROUPING_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import CustomerGroupingDetail from './CustomerGroupingDetail';
import CustomerGroupingMaster from './CustomerGroupingMaster';
import './CustomerGroupingView.scss';

function CustomerGroupingView() {
  return (
    <Switch>
      <Route path={path.join(CUSTOMER_GROUPING_ROUTE)} exact component={CustomerGroupingMaster}/>
      <Route path={path.join(CUSTOMER_GROUPING_ROUTE, ':id')} component={CustomerGroupingDetail}/>
    </Switch>
  );
}

export default withRouter(CustomerGroupingView);
