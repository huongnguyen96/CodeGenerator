
import React from 'react';
import ReactDOM from 'react-dom';
import {MemoryRouter} from 'react-router-dom';
import ImageFileView from './ImageFileView';

describe('ImageFileMaster', () => {
    it('renders without crashing', () => {
      const div = document.createElement('div');
      ReactDOM.render(
        <MemoryRouter>
          <ImageFileView/>
        </MemoryRouter>,
        div,
      );
      ReactDOM.unmountComponentAtNode(div);
    });
});
