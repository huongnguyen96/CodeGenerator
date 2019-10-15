import React from 'react';
import ReactDOM from 'react-dom';
import Provinces from './Provinces';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<Provinces/>, div);
  ReactDOM.unmountComponentAtNode(div);
});
