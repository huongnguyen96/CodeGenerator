
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import UserDetail from './UserDetail';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <UserDetail/>
    </MemoryRouter>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
