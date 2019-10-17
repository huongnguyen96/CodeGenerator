import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import DistrictList from './DistrictList';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <DistrictList/>
    </MemoryRouter>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
