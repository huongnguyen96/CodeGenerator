
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import MerchantDetail from './MerchantDetail';

describe('MerchantDetail', () => {
  it('renders without crashing', () => {
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <MerchantDetail/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    });
});
