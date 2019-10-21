
import {DISCOUNT_CUSTOMER_GROUPING_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import DiscountCustomerGroupingDetail from './DiscountCustomerGroupingDetail';
import DiscountCustomerGroupingMaster from './DiscountCustomerGroupingMaster';
import './DiscountCustomerGroupingView.scss';

function DiscountCustomerGroupingView() {
  return (
    <Switch>
      <Route path={path.join(DISCOUNT_CUSTOMER_GROUPING_ROUTE)} exact component={DiscountCustomerGroupingMaster}/>
      <Route path={path.join(DISCOUNT_CUSTOMER_GROUPING_ROUTE, ':id')} component={DiscountCustomerGroupingDetail}/>
    </Switch>
  );
}

export default withRouter(DiscountCustomerGroupingView);
