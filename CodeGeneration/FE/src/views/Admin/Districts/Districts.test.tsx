import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import Districts from './Districts';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <Districts/>
    </MemoryRouter>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
