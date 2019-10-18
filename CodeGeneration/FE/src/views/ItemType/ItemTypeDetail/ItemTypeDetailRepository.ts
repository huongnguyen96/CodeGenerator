
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class ItemTypeDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-type/item-type-detail');
  }

  public get = (id: number): Observable<ItemType> => {
    return this.httpService.post<ItemType>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemType>) => response.data),
      );
  };
  
  public create = (itemType: ItemType): Observable<ItemType> => {
    return this.httpService.post<ItemType>(`/create`, itemType)
      .pipe(
        map((response: AxiosResponse<ItemType>) => response.data),
      );
  };
  public update = (itemType: ItemType): Observable<ItemType> => {
    return this.httpService.post<ItemType>(`/update`, itemType)
      .pipe(
        map((response: AxiosResponse<ItemType>) => response.data),
      );
  };
  public delete = (itemType: ItemType): Observable<ItemType> => {
    return this.httpService.post<ItemType>(`/delete`, itemType)
      .pipe(
        map((response: AxiosResponse<ItemType>) => response.data),
      );
  };
  
  public save = (itemType: ItemType): Observable<ItemType> => {
    return itemType.id ? this.update(itemType) : this.create(itemType);
  };
  
  public singleList = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new ItemTypeDetailRepository();