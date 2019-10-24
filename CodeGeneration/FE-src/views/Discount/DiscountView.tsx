
import {DISCOUNT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import DiscountDetail from './DiscountDetail';
import DiscountMaster from './DiscountMaster';
import './DiscountView.scss';

function DiscountView() {
  return (
    <Switch>
      <Route path={path.join(DISCOUNT_ROUTE)} exact component={DiscountMaster}/>
      <Route path={path.join(DISCOUNT_ROUTE, ':id')} component={DiscountDetail}/>
    </Switch>
  );
}

export default withRouter(DiscountView);
