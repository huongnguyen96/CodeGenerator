
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class ItemTypeMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-type/item-type-master');
  }

  public count = (itemTypeSearch: ItemTypeSearch): Observable<number> => {
    return this.httpService.post('/count',itemTypeSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemTypeSearch: ItemTypeSearch): Observable<ItemType[]> => {
    return this.httpService.post('/list',itemTypeSearch)
      .pipe(
        map((response: AxiosResponse<ItemType[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ItemType> => {
    return this.httpService.post<ItemType>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemType>) => response.data),
      );
  };
  
  public singleList = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new ItemTypeMasterRepository();