
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {OrderStatus} from 'models/OrderStatus';
import {OrderStatusSearch} from 'models/OrderStatusSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class OrderStatusDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/order-status/order-status-detail');
  }

  public get = (id: number): Observable<OrderStatus> => {
    return this.httpService.post<OrderStatus>('/get', { id })
      .pipe(
        map((response: AxiosResponse<OrderStatus>) => response.data),
      );
  };

  public create = (orderStatus: OrderStatus): Observable<OrderStatus> => {
    return this.httpService.post<OrderStatus>(`/create`, orderStatus)
      .pipe(
        map((response: AxiosResponse<OrderStatus>) => response.data),
      );
  };
  public update = (orderStatus: OrderStatus): Observable<OrderStatus> => {
    return this.httpService.post<OrderStatus>(`/update`, orderStatus)
      .pipe(
        map((response: AxiosResponse<OrderStatus>) => response.data),
      );
  };
  public delete = (orderStatus: OrderStatus): Observable<OrderStatus> => {
    return this.httpService.post<OrderStatus>(`/delete`, orderStatus)
      .pipe(
        map((response: AxiosResponse<OrderStatus>) => response.data),
      );
  };

  public save = (orderStatus: OrderStatus): Observable<OrderStatus> => {
    return orderStatus.id ? this.update(orderStatus) : this.create(orderStatus);
  };

}

export default new OrderStatusDetailRepository();
