import {InitOptions} from 'i18next';

const i18nextConfig: InitOptions = {
  resources: {
    en: {
      general: {
        loading: 'Loading',
      },
    },
  },
  lng: 'en',
  fallbackLng: 'en',
  ns: '',
  defaultNS: '',
  nsSeparator: false,
  keySeparator: '.',
  interpolation: {
    escapeValue: false,
    nestingSuffixEscaped: '.',
  },
};

export default i18nextConfig;
