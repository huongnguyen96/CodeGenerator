
import {STOCK_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import StockDetail from './StockDetail';
import StockMaster from './StockMaster';
import './StockView.scss';

function StockView() {
  return (
    <Switch>
      <Route path={path.join(STOCK_ROUTE)} exact component={StockMaster}/>
      <Route path={path.join(STOCK_ROUTE, ':id')} component={StockDetail}/>
    </Switch>
  );
}

export default withRouter(StockView);
