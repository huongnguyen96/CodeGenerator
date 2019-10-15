import 'core/components/App/App.scss';
import {useEffect} from 'react';
import {BrowserRouter} from 'react-router-dom';
import React from 'reactn';
import {spinnerService} from 'services/SpinnerService';
import {translateService} from 'services/TranslateService';
import AppRouting from './AppRouting';

function App() {

  useEffect(
    () => {
      translateService.useLanguage('vi');
      spinnerService.hide();
    },
  );

  return (
    <BrowserRouter>
      <AppRouting/>
    </BrowserRouter>
  );
}

export default App;
