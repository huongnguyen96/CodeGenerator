
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {OrderStatus} from 'models/OrderStatus';
import {OrderStatusSearch} from 'models/OrderStatusSearch';


export class OrderStatusMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/order-status/order-status-master');
  }

  public count = (orderStatusSearch: OrderStatusSearch): Observable<number> => {
    return this.httpService.post('/count',orderStatusSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (orderStatusSearch: OrderStatusSearch): Observable<OrderStatus[]> => {
    return this.httpService.post('/list',orderStatusSearch)
      .pipe(
        map((response: AxiosResponse<OrderStatus[]>) => response.data),
      );
  };

  public get = (id: number): Observable<OrderStatus> => {
    return this.httpService.post<OrderStatus>('/get', { id })
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
  
}

export default new OrderStatusMasterRepository();