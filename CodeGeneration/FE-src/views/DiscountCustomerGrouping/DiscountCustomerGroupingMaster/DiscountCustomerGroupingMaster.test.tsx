
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import DiscountCustomerGroupingMaster from './DiscountCustomerGroupingMaster';

describe('DiscountCustomerGroupingMaster', () => {
    it('renders without crashing', () => {
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <DiscountCustomerGroupingMaster/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    });
});
