
import {PROVINCE_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ProvinceDetail from './ProvinceDetail';
import ProvinceMaster from './ProvinceMaster';
import './ProvinceView.scss';

function ProvinceView() {
  return (
    <Switch>
      <Route path={path.join(PROVINCE_ROUTE)} exact component={ProvinceMaster}/>
      <Route path={path.join(PROVINCE_ROUTE, ':id')} component={ProvinceDetail}/>
    </Switch>
  );
}

export default withRouter(ProvinceView);
