import {routes} from 'src/config/routes';
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import RouterRender from './RouterRender';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <RouterRender routes={routes}/>
    </MemoryRouter>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
