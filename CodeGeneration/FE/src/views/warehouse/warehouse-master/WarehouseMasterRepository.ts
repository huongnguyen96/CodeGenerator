
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

import {User} from 'models/User';
import {UserSearch} from 'models/UserSearch';

export class WarehouseMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/warehouse/warehouse-master');
  }

  public count = (warehouseSearch: WarehouseSearch): Observable<number> => {
    return this.httpService.post('/count',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/list',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Warehouse>) => response.data),
      );
  };
  
  public singleListUser = (userSearch: UserSearch): Observable<User[]> => {
    return this.httpService.post('/single-list-user',userSearch)
      .pipe(
        map((response: AxiosResponse<User[]>) => response.data),
      );
  };
}

export default new WarehouseMasterRepository();