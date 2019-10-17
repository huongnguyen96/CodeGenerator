import axios, {AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse} from 'axios';
import {Axios} from 'axios-observable';
import {requestConfig} from 'config/http';
import {notification} from 'helpers';

export class HttpService extends Axios {

  public static create(config: AxiosRequestConfig) {
    const instance: AxiosInstance = axios.create(config);

    instance.interceptors.request.use(
      (interceptorConfig) => {
        return interceptorConfig;
      },
      (error: Error) => {
        throw error;
      },
    );

    instance.interceptors.response.use(
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
    return new this(instance);
  }

  public static getInstance(): HttpService {
    if (this.instance) {
      return this.instance;
    }
    return HttpService.create(requestConfig);
  }

  protected static instance: HttpService;

  private constructor(instance: AxiosInstance) {
    super(instance);
  }

  public setBasePath(basePath: string) {
    this.defaults.baseURL = `${this.defaults.baseURL}${basePath}`;
  }
}

const httpService: HttpService = HttpService.getInstance();

export {httpService};
