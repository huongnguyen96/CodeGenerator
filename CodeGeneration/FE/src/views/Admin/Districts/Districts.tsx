import React from 'react';
import {Route, Switch, withRouter} from 'react-router';
import DistrictDetail from './DistrictDetail/DistrictDetail';
import DistrictList from './DistrictList/DistrictList';
import './Districts.scss';

function Districts() {
  return (
    <Switch>
      <Route path="/admin/districts" exact component={DistrictList}/>
      <Route path="/admin/districts/:id" component={DistrictDetail}/>
    </Switch>
  );
}

export default withRouter(Districts);
