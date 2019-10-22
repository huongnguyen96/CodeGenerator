
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Order} from 'models/Order';
import {OrderSearch} from 'models/OrderSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';

export class OrderDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/order/order-detail');
  }

  public get = (id: number): Observable<Order> => {
    return this.httpService.post<Order>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Order>) => response.data),
      );
  };
  
  public create = (order: Order): Observable<Order> => {
    return this.httpService.post<Order>(`/create`, order)
      .pipe(
        map((response: AxiosResponse<Order>) => response.data),
      );
  };
  public update = (order: Order): Observable<Order> => {
    return this.httpService.post<Order>(`/update`, order)
      .pipe(
        map((response: AxiosResponse<Order>) => response.data),
      );
  };
  public delete = (order: Order): Observable<Order> => {
    return this.httpService.post<Order>(`/delete`, order)
      .pipe(
        map((response: AxiosResponse<Order>) => response.data),
      );
  };
  
  public save = (order: Order): Observable<Order> => {
    return order.id ? this.update(order) : this.create(order);
  };
  
  public singleListCustomer = (customerSearch: CustomerSearch): Observable<Customer[]> => {
    return this.httpService.post('/single-list-customer',customerSearch)
      .pipe(
        map((response: AxiosResponse<Customer[]>) => response.data),
      );
  };
}

export default new OrderDetailRepository();