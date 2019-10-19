
import {USER_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import UserDetail from './UserDetail';
import UserMaster from './UserMaster';
import './UserView.scss';

function UserView() {
  return (
    <Switch>
      <Route path={path.join(USER_ROUTE)} exact component={UserMaster}/>
      <Route path={path.join(USER_ROUTE, ':id')} component={UserDetail}/>
    </Switch>
  );
}

export default withRouter(UserView);
