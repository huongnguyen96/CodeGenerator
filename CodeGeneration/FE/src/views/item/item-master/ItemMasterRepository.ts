
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';


export class ItemMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item/item-master');
  }

  public count = (itemSearch: ItemSearch): Observable<number> => {
    return this.httpService.post('/count',itemSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/list',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Item> => {
    return this.httpService.post<Item>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  
}

export default new ItemMasterRepository();