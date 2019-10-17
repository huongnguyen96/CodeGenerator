import {ADMIN_DISTRICTS_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import DistrictDetail from './DistrictDetail';
import DistrictList from './DistrictList';
import './Districts.scss';

function Districts() {
  return (
    <Switch>
      <Route path={path.join(ADMIN_DISTRICTS_ROUTE)} exact component={DistrictList}/>
      <Route path={path.join(ADMIN_DISTRICTS_ROUTE, ':id')} component={DistrictDetail}/>
    </Switch>
  );
}

export default withRouter(Districts);
