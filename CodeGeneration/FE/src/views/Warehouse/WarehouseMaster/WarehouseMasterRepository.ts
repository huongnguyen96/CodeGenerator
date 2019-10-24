
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';

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
    
  public delete = (warehouse: Warehouse): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>(`/delete`, warehouse)
      .pipe(
        map((response: AxiosResponse<Warehouse>) => response.data),
      );
  };
  
  public singleListMerchant = (merchantSearch: MerchantSearch): Observable<Merchant[]> => {
    return this.httpService.post('/single-list-merchant',merchantSearch)
      .pipe(
        map((response: AxiosResponse<Merchant[]>) => response.data),
      );
  };
}

export default new WarehouseMasterRepository();