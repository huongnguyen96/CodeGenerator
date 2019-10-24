
import {CATEGORY_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import CategoryDetail from './CategoryDetail';
import CategoryMaster from './CategoryMaster';
import './CategoryView.scss';

function CategoryView() {
  return (
    <Switch>
      <Route path={path.join(CATEGORY_ROUTE)} exact component={CategoryMaster}/>
      <Route path={path.join(CATEGORY_ROUTE, ':id')} component={CategoryDetail}/>
    </Switch>
  );
}

export default withRouter(CategoryView);
