
import {PARTNER_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import PartnerDetail from './PartnerDetail';
import PartnerMaster from './PartnerMaster';
import './PartnerView.scss';

function PartnerView() {
  return (
    <Switch>
      <Route path={path.join(PARTNER_ROUTE)} exact component={PartnerMaster}/>
      <Route path={path.join(PARTNER_ROUTE, ':id')} component={PartnerDetail}/>
    </Switch>
  );
}

export default withRouter(PartnerView);
