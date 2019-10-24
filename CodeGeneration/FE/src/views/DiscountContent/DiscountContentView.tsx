
import {DISCOUNT_CONTENT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import DiscountContentDetail from './DiscountContentDetail';
import DiscountContentMaster from './DiscountContentMaster';
import './DiscountContentView.scss';

function DiscountContentView() {
  return (
    <Switch>
      <Route path={path.join(DISCOUNT_CONTENT_ROUTE)} exact component={DiscountContentMaster}/>
      <Route path={path.join(DISCOUNT_CONTENT_ROUTE, ':id')} component={DiscountContentDetail}/>
    </Switch>
  );
}

export default withRouter(DiscountContentView);
