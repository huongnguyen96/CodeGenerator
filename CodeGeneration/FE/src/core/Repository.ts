import {requestConfig} from 'config/http';
import {HttpService} from 'services';

export abstract class Repository {
  protected httpService: HttpService;

  protected constructor() {
    this.httpService = HttpService.create(requestConfig);
  }
}

export default Repository;
