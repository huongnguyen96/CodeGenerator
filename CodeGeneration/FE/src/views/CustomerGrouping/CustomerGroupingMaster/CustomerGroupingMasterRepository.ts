
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {CustomerGrouping} from 'models/CustomerGrouping';
import {CustomerGroupingSearch} from 'models/CustomerGroupingSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class CustomerGroupingMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/customer-grouping/customer-grouping-master');
  }

  public count = (customerGroupingSearch: CustomerGroupingSearch): Observable<number> => {
    return this.httpService.post('/count', customerGroupingSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (customerGroupingSearch: CustomerGroupingSearch): Observable<CustomerGrouping[]> => {
    return this.httpService.post('/list', customerGroupingSearch)
      .pipe(
        map((response: AxiosResponse<CustomerGrouping[]>) => response.data),
      );
  };

  public get = (id: number): Observable<CustomerGrouping> => {
    return this.httpService.post<CustomerGrouping>('/get', { id })
      .pipe(
        map((response: AxiosResponse<CustomerGrouping>) => response.data),
      );
  };

  public delete = (customerGrouping: CustomerGrouping): Observable<CustomerGrouping> => {
    return this.httpService.post<CustomerGrouping>(`/delete`, customerGrouping)
      .pipe(
        map((response: AxiosResponse<CustomerGrouping>) => response.data),
      );
  };

}

export default new CustomerGroupingMasterRepository();
