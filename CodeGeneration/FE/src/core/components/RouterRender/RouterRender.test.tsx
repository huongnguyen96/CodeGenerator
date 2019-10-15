import {routes} from 'config/routes';
import React from 'react';
import ReactDOM from 'react-dom';
import RouterRender from './RouterRender';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <RouterRender routes={routes}/>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
