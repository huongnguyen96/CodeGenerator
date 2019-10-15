import axios, {AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse} from 'axios';
import {Axios} from 'axios-observable';
import {notification} from '../helpers';

export class HttpService extends Axios {

  public static create(config: AxiosRequestConfig) {
    return new this(axios.create(config));
  }

  public static getInstance(): HttpService {
    if (this.instance) {
      return this.instance;
    }
    return HttpService.create({
      baseURL: process.env.REACT_APP_BASE_URL,
      withCredentials: true,
    });
  }

  protected static instance: HttpService;

  private constructor(instance: AxiosInstance) {
    super(instance);
  }
}

const httpService: HttpService = HttpService.getInstance();

httpService.interceptors.request.use(
  (config) => {
    return config;
  },
  (error: Error) => {
    throw error;
  },
);

httpService.interceptors.response.use(
  (response: AxiosResponse) => {
    return response;
  },
  (error: AxiosError) => {
    switch (error.code) {
      case '429':
        notification.error({
          message: 'Too many requests',
        });
        break;
      default:
        throw error;
    }
  },
);

export {httpService};
