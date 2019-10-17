import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import CardTitle from './CardTitle';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <CardTitle title="Should render card title"/>
    </MemoryRouter>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
