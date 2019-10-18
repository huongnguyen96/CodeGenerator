
import {ITEM_STATUS_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ItemStatusDetail from './ItemStatusDetail';
import ItemStatusMaster from './ItemStatusMaster';
import './ItemStatus.scss';

function ItemStatusView() {
  return (
    <Switch>
      <Route path={path.join(ITEM_STATUS_ROUTE)} exact component={ItemStatusMaster}/>
      <Route path={path.join(ITEM_STATUS_ROUTE, ':id')} component={ItemStatusDetail}/>
    </Switch>
  );
}

export default withRouter(ItemStatusView);
