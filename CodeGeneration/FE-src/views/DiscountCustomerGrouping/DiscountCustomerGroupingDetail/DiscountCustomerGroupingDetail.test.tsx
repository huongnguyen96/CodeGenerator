
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import DiscountCustomerGroupingDetail from './DiscountCustomerGroupingDetail';

describe('DiscountCustomerGroupingDetail', () => {
  it('renders without crashing', () => {
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <DiscountCustomerGroupingDetail/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    });  
});
