
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';


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
    
  public delete = (itemStatus: ItemStatus): Observable<ItemStatus> => {
    return this.httpService.post<ItemStatus>(`/delete`, itemStatus)
      .pipe(
        map((response: AxiosResponse<ItemStatus>) => response.data),
      );
  };
  
}

export default new ItemStatusMasterRepository();