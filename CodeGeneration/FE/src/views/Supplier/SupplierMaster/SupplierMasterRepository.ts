
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Supplier} from 'models/Supplier';
import {SupplierSearch} from 'models/SupplierSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class SupplierMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/supplier/supplier-master');
  }

  public count = (supplierSearch: SupplierSearch): Observable<number> => {
    return this.httpService.post('/count',supplierSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (supplierSearch: SupplierSearch): Observable<Supplier[]> => {
    return this.httpService.post('/list',supplierSearch)
      .pipe(
        map((response: AxiosResponse<Supplier[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Supplier> => {
    return this.httpService.post<Supplier>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Supplier>) => response.data),
      );
  };
  
  public singleList = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
  public singleList = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/single-list-warehouse',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };
}

export default new SupplierMasterRepository();