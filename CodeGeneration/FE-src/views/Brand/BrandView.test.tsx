
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import BrandView from './BrandView';

describe('BrandMaster', () => {
    it('renders without crashing', () => {
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <BrandView/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    });
});
