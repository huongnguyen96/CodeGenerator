import {AxiosRequestConfig} from 'axios';
import {BASE_URL} from './consts';

export const requestConfig: AxiosRequestConfig = {
  baseURL: BASE_URL,
  withCredentials: true,
};
