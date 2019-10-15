import Axios from 'axios-observable';
import {httpService} from 'services';

export abstract class Repository {
  protected httpService: Axios = httpService;

  protected baseURL: string = process.env.REACT_APP_BASE_URL;

  protected constructor(baseURL?: string) {
    if (baseURL) {
      this.baseURL = baseURL;
    }
  }

  public getURL = (pathname: string) => `${this.baseURL}/${pathname}`;
}

export default Repository;
