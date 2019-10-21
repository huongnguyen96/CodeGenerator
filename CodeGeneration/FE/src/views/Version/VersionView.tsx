
import {VERSION_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import VersionDetail from './VersionDetail';
import VersionMaster from './VersionMaster';
import './VersionView.scss';

function VersionView() {
  return (
    <Switch>
      <Route path={path.join(VERSION_ROUTE)} exact component={VersionMaster}/>
      <Route path={path.join(VERSION_ROUTE, ':id')} component={VersionDetail}/>
    </Switch>
  );
}

export default withRouter(VersionView);
