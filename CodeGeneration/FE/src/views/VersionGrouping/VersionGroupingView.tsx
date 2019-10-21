
import {VERSION_GROUPING_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import VersionGroupingDetail from './VersionGroupingDetail';
import VersionGroupingMaster from './VersionGroupingMaster';
import './VersionGroupingView.scss';

function VersionGroupingView() {
  return (
    <Switch>
      <Route path={path.join(VERSION_GROUPING_ROUTE)} exact component={VersionGroupingMaster}/>
      <Route path={path.join(VERSION_GROUPING_ROUTE, ':id')} component={VersionGroupingDetail}/>
    </Switch>
  );
}

export default withRouter(VersionGroupingView);
