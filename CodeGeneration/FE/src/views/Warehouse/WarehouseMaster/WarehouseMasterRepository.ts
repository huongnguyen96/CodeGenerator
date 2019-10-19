
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {ItemStock} from 'models/ItemStock';
import {ItemStockSearch} from 'models/ItemStockSearch';
import {Supplier} from 'models/Supplier';
import {SupplierSearch} from 'models/SupplierSearch';

export class WarehouseMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/warehouse/warehouse-master');
  }

  public count = (warehouseSearch: WarehouseSearch): Observable<number> => {
    return this.httpService.post('/count', warehouseSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/list', warehouseSearch)
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

  public singleListSupplier = (supplierSearch: SupplierSearch): Observable<Supplier[]> => {
    return this.httpService.post('/single-list-supplier', supplierSearch)
      .pipe(
        map((response: AxiosResponse<Supplier[]>) => response.data),
      );
  };
  public singleList = (itemStockSearch: ItemStockSearch): Observable<ItemStock[]> => {
    return this.httpService.post('/single-list-item-stock', itemStockSearch)
      .pipe(
        map((response: AxiosResponse<ItemStock[]>) => response.data),
      );
  };
}

export default new WarehouseMasterRepository();
