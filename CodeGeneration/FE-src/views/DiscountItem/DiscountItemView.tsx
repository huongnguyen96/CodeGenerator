
import {DISCOUNT_ITEM_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import DiscountItemDetail from './DiscountItemDetail';
import DiscountItemMaster from './DiscountItemMaster';
import './DiscountItemView.scss';

function DiscountItemView() {
  return (
    <Switch>
      <Route path={path.join(DISCOUNT_ITEM_ROUTE)} exact component={DiscountItemMaster}/>
      <Route path={path.join(DISCOUNT_ITEM_ROUTE, ':id')} component={DiscountItemDetail}/>
    </Switch>
  );
}

export default withRouter(DiscountItemView);
