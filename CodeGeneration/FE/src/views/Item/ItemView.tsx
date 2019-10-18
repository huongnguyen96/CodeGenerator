
import {ITEM_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ItemDetail from './ItemDetail';
import ItemMaster from './ItemMaster';
import './Item.scss';

function ItemView() {
  return (
    <Switch>
      <Route path={path.join(ITEM_ROUTE)} exact component={ItemMaster}/>
      <Route path={path.join(ITEM_ROUTE, ':id')} component={ItemDetail}/>
    </Switch>
  );
}

export default withRouter(ItemView);
