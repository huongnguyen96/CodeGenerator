
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class ItemStatusDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-status/item-status-detail');
  }

  public get = (id: number): Observable<ItemStatus> => {
    return this.httpService.post<ItemStatus>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemStatus>) => response.data),
      );
  };
  
  public create = (itemStatus: ItemStatus): Observable<ItemStatus> => {
    return this.httpService.post<ItemStatus>(`/create`, itemStatus)
      .pipe(
        map((response: AxiosResponse<ItemStatus>) => response.data),
      );
  };
  public update = (itemStatus: ItemStatus): Observable<ItemStatus> => {
    return this.httpService.post<ItemStatus>(`/update`, itemStatus)
      .pipe(
        map((response: AxiosResponse<ItemStatus>) => response.data),
      );
  };
  public delete = (itemStatus: ItemStatus): Observable<ItemStatus> => {
    return this.httpService.post<ItemStatus>(`/delete`, itemStatus)
      .pipe(
        map((response: AxiosResponse<ItemStatus>) => response.data),
      );
  };
  
  public save = (itemStatus: ItemStatus): Observable<ItemStatus> => {
    return itemStatus.id ? this.update(itemStatus) : this.create(itemStatus);
  };
  
  public singleList = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new ItemStatusDetailRepository();