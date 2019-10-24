
import {BRAND_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import BrandDetail from './BrandDetail';
import BrandMaster from './BrandMaster';
import './BrandView.scss';

function BrandView() {
  return (
    <Switch>
      <Route path={path.join(BRAND_ROUTE)} exact component={BrandMaster}/>
      <Route path={path.join(BRAND_ROUTE, ':id')} component={BrandDetail}/>
    </Switch>
  );
}

export default withRouter(BrandView);
