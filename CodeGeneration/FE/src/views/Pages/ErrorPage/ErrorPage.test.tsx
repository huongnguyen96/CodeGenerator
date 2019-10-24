import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import ErrorPage from './ErrorPage';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <ErrorPage code={500}/>
    </MemoryRouter>
    , div);
  ReactDOM.unmountComponentAtNode(div);
});
