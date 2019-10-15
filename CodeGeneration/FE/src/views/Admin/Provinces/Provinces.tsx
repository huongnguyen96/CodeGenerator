import React from 'react';
import {Route, Switch} from 'react-router-dom';
import ProvinceDetail from './ProvinceDetail/ProvinceDetail';
import ProvinceList from './ProvinceList/ProvinceList';

function Provinces() {
  return (
    <Switch>
      <Route path="/admin/provinces" exact component={ProvinceList}/>
      <Route path="/admin/provinces/:id" component={ProvinceDetail}/>
    </Switch>
  );
}

export default Provinces;
