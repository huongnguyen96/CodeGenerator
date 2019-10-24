
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {CustomerGrouping} from 'models/CustomerGrouping';
import {CustomerGroupingSearch} from 'models/CustomerGroupingSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class CustomerGroupingDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/customer-grouping/customer-grouping-detail');
  }

  public get = (id: number): Observable<CustomerGrouping> => {
    return this.httpService.post<CustomerGrouping>('/get', { id })
      .pipe(
        map((response: AxiosResponse<CustomerGrouping>) => response.data),
      );
  };

  public create = (customerGrouping: CustomerGrouping): Observable<CustomerGrouping> => {
    return this.httpService.post<CustomerGrouping>(`/create`, customerGrouping)
      .pipe(
        map((response: AxiosResponse<CustomerGrouping>) => response.data),
      );
  };
  public update = (customerGrouping: CustomerGrouping): Observable<CustomerGrouping> => {
    return this.httpService.post<CustomerGrouping>(`/update`, customerGrouping)
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

  public save = (customerGrouping: CustomerGrouping): Observable<CustomerGrouping> => {
    return customerGrouping.id ? this.update(customerGrouping) : this.create(customerGrouping);
  };

}

export default new CustomerGroupingDetailRepository();
