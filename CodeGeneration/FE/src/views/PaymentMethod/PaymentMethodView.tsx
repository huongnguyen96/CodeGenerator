
import {PAYMENT_METHOD_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route, Switch, withRouter} from 'react-router-dom';
import PaymentMethodDetail from './PaymentMethodDetail';
import PaymentMethodMaster from './PaymentMethodMaster';
import './PaymentMethodView.scss';

function PaymentMethodView() {
  return (
    <Switch>
      <Route path={path.join(PAYMENT_METHOD_ROUTE)} exact component={PaymentMethodMaster}/>
      <Route path={path.join(PAYMENT_METHOD_ROUTE, ':id')} component={PaymentMethodDetail}/>
    </Switch>
  );
}

export default withRouter(PaymentMethodView);
