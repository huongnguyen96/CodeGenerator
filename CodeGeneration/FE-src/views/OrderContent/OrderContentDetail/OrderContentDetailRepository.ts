
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {OrderContent} from 'models/OrderContent';
import {OrderContentSearch} from 'models/OrderContentSearch';

import {Order} from 'models/Order';
import {OrderSearch} from 'models/OrderSearch';

export class OrderContentDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/order-content/order-content-detail');
  }

  public get = (id: number): Observable<OrderContent> => {
    return this.httpService.post<OrderContent>('/get', { id })
      .pipe(
        map((response: AxiosResponse<OrderContent>) => response.data),
      );
  };
  
  public create = (orderContent: OrderContent): Observable<OrderContent> => {
    return this.httpService.post<OrderContent>(`/create`, orderContent)
      .pipe(
        map((response: AxiosResponse<OrderContent>) => response.data),
      );
  };
  public update = (orderContent: OrderContent): Observable<OrderContent> => {
    return this.httpService.post<OrderContent>(`/update`, orderContent)
      .pipe(
        map((response: AxiosResponse<OrderContent>) => response.data),
      );
  };
  public delete = (orderContent: OrderContent): Observable<OrderContent> => {
    return this.httpService.post<OrderContent>(`/delete`, orderContent)
      .pipe(
        map((response: AxiosResponse<OrderContent>) => response.data),
      );
  };
  
  public save = (orderContent: OrderContent): Observable<OrderContent> => {
    return orderContent.id ? this.update(orderContent) : this.create(orderContent);
  };
  
  public singleListOrder = (orderSearch: OrderSearch): Observable<Order[]> => {
    return this.httpService.post('/single-list-order',orderSearch)
      .pipe(
        map((response: AxiosResponse<Order[]>) => response.data),
      );
  };
}

export default new OrderContentDetailRepository();