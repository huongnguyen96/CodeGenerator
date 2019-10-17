import {globalData} from 'config/global';
import i18nextConfig from 'config/i18next';
import App from 'core/components/App';
import AppLoading from 'core/components/AppLoading';
import i18n from 'i18next';
import React from 'react';
import ReactDOM from 'react-dom';
import {initReactI18next} from 'react-i18next';
import {setGlobal} from 'reactn';
import './index.scss';
import * as serviceWorker from './service-worker';

Promise.all([
  setGlobal(globalData),
  i18n
    .use(initReactI18next)
    .init(i18nextConfig),
])
  .then(() => {
    ReactDOM.render(
      <AppLoading>
        <App/>
      </AppLoading>,
      document.getElementById('root'),
    );

    // If you want your app to work offline and load faster, you can change
    // unregister() to register() below. Note this comes with some pitfalls.
    // Learn more about service workers: https://bit.ly/CRA-PWA
    serviceWorker.unregister();
  });
