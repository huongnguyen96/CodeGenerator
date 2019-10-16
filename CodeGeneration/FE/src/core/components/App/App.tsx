import {useEffect} from 'react';
import {BrowserRouter} from 'react-router-dom';
import React from 'reactn';
import {spinnerService} from 'services/SpinnerService';
import {translateService} from 'services/TranslateService';
import './App.scss';
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
