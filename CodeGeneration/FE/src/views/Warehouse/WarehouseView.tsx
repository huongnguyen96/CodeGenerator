
import {WAREHOUSE_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import WarehouseDetail from './WarehouseDetail';
import WarehouseMaster from './WarehouseMaster';
import './WarehouseView.scss';

function WarehouseView() {
  return (
    <Switch>
      <Route path={path.join(WAREHOUSE_ROUTE)} exact component={WarehouseMaster}/>
      <Route path={path.join(WAREHOUSE_ROUTE, ':id')} component={WarehouseDetail}/>
    </Switch>
  );
}

export default withRouter(WarehouseView);
