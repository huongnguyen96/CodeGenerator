
import {ITEM_UNIT_OF_MEASURE_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ItemUnitOfMeasureDetail from './ItemUnitOfMeasureDetail';
import ItemUnitOfMeasureMaster from './ItemUnitOfMeasureMaster';
import './ItemUnitOfMeasure.scss';

function ItemUnitOfMeasureView() {
  return (
    <Switch>
      <Route path={path.join(ITEM_UNIT_OF_MEASURE_ROUTE)} exact component={ItemUnitOfMeasureMaster}/>
      <Route path={path.join(ITEM_UNIT_OF_MEASURE_ROUTE, ':id')} component={ItemUnitOfMeasureDetail}/>
    </Switch>
  );
}

export default withRouter(ItemUnitOfMeasureView);
