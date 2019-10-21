
import {WARD_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import WardDetail from './WardDetail';
import WardMaster from './WardMaster';
import './WardView.scss';

function WardView() {
  return (
    <Switch>
      <Route path={path.join(WARD_ROUTE)} exact component={WardMaster}/>
      <Route path={path.join(WARD_ROUTE, ':id')} component={WardDetail}/>
    </Switch>
  );
}

export default withRouter(WardView);
