
import {ITEM_STOCK_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ItemStockDetail from './ItemStockDetail';
import ItemStockMaster from './ItemStockMaster';
import './ItemStock.scss';

function ItemStockView() {
  return (
    <Switch>
      <Route path={path.join(ITEM_STOCK_ROUTE)} exact component={ItemStockMaster}/>
      <Route path={path.join(ITEM_STOCK_ROUTE, ':id')} component={ItemStockDetail}/>
    </Switch>
  );
}

export default withRouter(ItemStockView);
