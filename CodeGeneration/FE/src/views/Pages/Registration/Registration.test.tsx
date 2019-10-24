import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import Registration from './Registration';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <Registration/>
    </MemoryRouter>
    , div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
