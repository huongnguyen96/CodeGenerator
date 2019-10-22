
import {ITEM_TYPE_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ItemTypeDetail from './ItemTypeDetail';
import ItemTypeMaster from './ItemTypeMaster';
import './ItemTypeView.scss';

function ItemTypeView() {
  return (
    <Switch>
      <Route path={path.join(ITEM_TYPE_ROUTE)} exact component={ItemTypeMaster}/>
      <Route path={path.join(ITEM_TYPE_ROUTE, ':id')} component={ItemTypeDetail}/>
    </Switch>
  );
}

export default withRouter(ItemTypeView);
