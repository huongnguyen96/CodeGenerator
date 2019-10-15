import React from 'react';
import ReactDOM from 'react-dom';
import ErrorPage from './ErrorPage';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<ErrorPage code={500}/>, div);
  ReactDOM.unmountComponentAtNode(div);
});
