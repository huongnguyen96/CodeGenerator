
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';


export class CustomerMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/customer/customer-master');
  }

  public count = (customerSearch: CustomerSearch): Observable<number> => {
    return this.httpService.post('/count',customerSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (customerSearch: CustomerSearch): Observable<Customer[]> => {
    return this.httpService.post('/list',customerSearch)
      .pipe(
        map((response: AxiosResponse<Customer[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Customer> => {
    return this.httpService.post<Customer>('/get', { id })
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
  
}

export default new CustomerMasterRepository();