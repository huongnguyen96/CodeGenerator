import {AxiosRequestConfig} from 'axios';

export const requestConfig: AxiosRequestConfig = {
  withCredentials: true,
  baseURL: process.env.REACT_APP_BASE_URL,
};
