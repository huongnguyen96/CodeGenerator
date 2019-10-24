
import {DISTRICT_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import DistrictDetail from './DistrictDetail';
import DistrictMaster from './DistrictMaster';
import './DistrictView.scss';

function DistrictView() {
  return (
    <Switch>
      <Route path={path.join(DISTRICT_ROUTE)} exact component={DistrictMaster}/>
      <Route path={path.join(DISTRICT_ROUTE, ':id')} component={DistrictDetail}/>
    </Switch>
  );
}

export default withRouter(DistrictView);
