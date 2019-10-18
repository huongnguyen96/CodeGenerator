
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class ItemStatusMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-status/item-status-master');
  }

  public count = (itemStatusSearch: ItemStatusSearch): Observable<number> => {
    return this.httpService.post('/count',itemStatusSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemStatusSearch: ItemStatusSearch): Observable<ItemStatus[]> => {
    return this.httpService.post('/list',itemStatusSearch)
      .pipe(
        map((response: AxiosResponse<ItemStatus[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ItemStatus> => {
    return this.httpService.post<ItemStatus>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemStatus>) => response.data),
      );
  };
  
  public singleList = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new ItemStatusMasterRepository();