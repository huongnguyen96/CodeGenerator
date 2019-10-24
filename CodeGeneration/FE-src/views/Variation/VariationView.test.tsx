
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import VariationView from './VariationView';

describe('VariationMaster', () => {
    it('renders without crashing', () => {
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <VariationView/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    });
});
