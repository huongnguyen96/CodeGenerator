
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';


export class ItemDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item/item-detail');
  }

  public get = (id: number): Observable<Item> => {
    return this.httpService.post<Item>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  
  public create = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/create`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  public update = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/create`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  public delete = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/create`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  
  public save = (item: Item): Observable<Item> => {
    return item.id ? this.update(item) : this.create(item);
  };
  
}

export default new ItemDetailRepository();