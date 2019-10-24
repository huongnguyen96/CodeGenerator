
import {ADMINISTRATOR_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import AdministratorDetail from './AdministratorDetail';
import AdministratorMaster from './AdministratorMaster';
import './AdministratorView.scss';

function AdministratorView() {
  return (
    <Switch>
      <Route path={path.join(ADMINISTRATOR_ROUTE)} exact component={AdministratorMaster}/>
      <Route path={path.join(ADMINISTRATOR_ROUTE, ':id')} component={AdministratorDetail}/>
    </Switch>
  );
}

export default withRouter(AdministratorView);
