import React from 'react';
import ReactDOM from 'react-dom';
import AppLoading from './AppLoading';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <AppLoading/>,
    div,
  );
  ReactDOM.unmountComponentAtNode(div);
});
