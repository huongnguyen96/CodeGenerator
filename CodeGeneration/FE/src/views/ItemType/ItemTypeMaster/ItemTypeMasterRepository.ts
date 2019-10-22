
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';


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
    
  public delete = (itemType: ItemType): Observable<ItemType> => {
    return this.httpService.post<ItemType>(`/delete`, itemType)
      .pipe(
        map((response: AxiosResponse<ItemType>) => response.data),
      );
  };
  
}

export default new ItemTypeMasterRepository();