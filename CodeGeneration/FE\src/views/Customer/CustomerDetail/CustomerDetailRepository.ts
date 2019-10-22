
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';


export class CustomerDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/customer/customer-detail');
  }

  public get = (id: number): Observable<Customer> => {
    return this.httpService.post<Customer>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Customer>) => response.data),
      );
  };
  
  public create = (customer: Customer): Observable<Customer> => {
    return this.httpService.post<Customer>(`/create`, customer)
      .pipe(
        map((response: AxiosResponse<Customer>) => response.data),
      );
  };
  public update = (customer: Customer): Observable<Customer> => {
    return this.httpService.post<Customer>(`/update`, customer)
      .pipe(
        map((response: AxiosResponse<Customer>) => response.data),
      );
  };
  public delete = (customer: Customer): Observable<Customer> => {
    return this.httpService.post<Customer>(`/delete`, customer)
      .pipe(
        map((response: AxiosResponse<Customer>) => response.data),
      );
  };
  
  public save = (customer: Customer): Observable<Customer> => {
    return customer.id ? this.update(customer) : this.create(customer);
  };
  
}

export default new CustomerDetailRepository();