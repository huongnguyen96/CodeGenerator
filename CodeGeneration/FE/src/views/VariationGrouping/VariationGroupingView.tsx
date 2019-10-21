
import {VARIATION_GROUPING_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import VariationGroupingDetail from './VariationGroupingDetail';
import VariationGroupingMaster from './VariationGroupingMaster';
import './VariationGroupingView.scss';

function VariationGroupingView() {
  return (
    <Switch>
      <Route path={path.join(VARIATION_GROUPING_ROUTE)} exact component={VariationGroupingMaster}/>
      <Route path={path.join(VARIATION_GROUPING_ROUTE, ':id')} component={VariationGroupingDetail}/>
    </Switch>
  );
}

export default withRouter(VariationGroupingView);
