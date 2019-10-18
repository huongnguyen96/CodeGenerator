
import {Category_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import CategoryDetail from './CategoryDetail';
import CategoryMaster from './CategoryMaster';
import './Category.scss';

function CategoryView() {
  return (
    <Switch>
      <Route path={path.join(Category_ROUTE)} exact component={CategoryMaster}/>
      <Route path={path.join(Category_ROUTE, ':id')} component={CategoryDetail}/>
    </Switch>
  );
}

export default withRouter(CategoryView);
