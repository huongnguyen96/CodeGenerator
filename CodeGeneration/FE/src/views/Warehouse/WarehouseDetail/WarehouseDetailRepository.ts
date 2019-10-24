
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';

export class WarehouseDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/warehouse/warehouse-detail');
  }

  public get = (id: number): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Warehouse>) => response.data),
      );
  };
  
  public create = (warehouse: Warehouse): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>(`/create`, warehouse)
      .pipe(
        map((response: AxiosResponse<Warehouse>) => response.data),
      );
  };
  public update = (warehouse: Warehouse): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>(`/update`, warehouse)
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
  
  public save = (warehouse: Warehouse): Observable<Warehouse> => {
    return warehouse.id ? this.update(warehouse) : this.create(warehouse);
  };
  
  public singleListMerchant = (merchantSearch: MerchantSearch): Observable<Merchant[]> => {
    return this.httpService.post('/single-list-merchant',merchantSearch)
      .pipe(
        map((response: AxiosResponse<Merchant[]>) => response.data),
      );
  };
}

export default new WarehouseDetailRepository();