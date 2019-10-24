
import {UNIT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import UnitDetail from './UnitDetail';
import UnitMaster from './UnitMaster';
import './UnitView.scss';

function UnitView() {
  return (
    <Switch>
      <Route path={path.join(UNIT_ROUTE)} exact component={UnitMaster}/>
      <Route path={path.join(UNIT_ROUTE, ':id')} component={UnitDetail}/>
    </Switch>
  );
}

export default withRouter(UnitView);
