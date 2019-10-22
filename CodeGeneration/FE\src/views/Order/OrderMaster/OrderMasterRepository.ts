
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Order} from 'models/Order';
import {OrderSearch} from 'models/OrderSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';

export class OrderMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/order/order-master');
  }

  public count = (orderSearch: OrderSearch): Observable<number> => {
    return this.httpService.post('/count',orderSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (orderSearch: OrderSearch): Observable<Order[]> => {
    return this.httpService.post('/list',orderSearch)
      .pipe(
        map((response: AxiosResponse<Order[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Order> => {
    return this.httpService.post<Order>('/get', { id })
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
  
  public singleListCustomer = (customerSearch: CustomerSearch): Observable<Customer[]> => {
    return this.httpService.post('/single-list-customer',customerSearch)
      .pipe(
        map((response: AxiosResponse<Customer[]>) => response.data),
      );
  };
}

export default new OrderMasterRepository();