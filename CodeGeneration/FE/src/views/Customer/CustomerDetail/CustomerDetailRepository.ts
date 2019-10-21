
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';

import {Order} from 'models/Order';
import {OrderSearch} from 'models/OrderSearch';
import {ShippingAddress} from 'models/ShippingAddress';
import {ShippingAddressSearch} from 'models/ShippingAddressSearch';

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
  
  public singleListOrder = (orderSearch: OrderSearch): Observable<Order[]> => {
    return this.httpService.post('/single-list-order',orderSearch)
      .pipe(
        map((response: AxiosResponse<Order[]>) => response.data),
      );
  };
  public singleListShippingAddress = (shippingAddressSearch: ShippingAddressSearch): Observable<ShippingAddress[]> => {
    return this.httpService.post('/single-list-shipping-address',shippingAddressSearch)
      .pipe(
        map((response: AxiosResponse<ShippingAddress[]>) => response.data),
      );
  };
}

export default new CustomerDetailRepository();