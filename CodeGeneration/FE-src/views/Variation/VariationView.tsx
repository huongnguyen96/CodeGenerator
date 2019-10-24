
import {VARIATION_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import VariationDetail from './VariationDetail';
import VariationMaster from './VariationMaster';
import './VariationView.scss';

function VariationView() {
  return (
    <Switch>
      <Route path={path.join(VARIATION_ROUTE)} exact component={VariationMaster}/>
      <Route path={path.join(VARIATION_ROUTE, ':id')} component={VariationDetail}/>
    </Switch>
  );
}

export default withRouter(VariationView);
