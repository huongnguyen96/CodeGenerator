
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Supplier} from 'models/Supplier';
import {SupplierSearch} from 'models/SupplierSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class SupplierDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/supplier/supplier-detail');
  }

  public get = (id: number): Observable<Supplier> => {
    return this.httpService.post<Supplier>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Supplier>) => response.data),
      );
  };

  public create = (supplier: Supplier): Observable<Supplier> => {
    return this.httpService.post<Supplier>(`/create`, supplier)
      .pipe(
        map((response: AxiosResponse<Supplier>) => response.data),
      );
  };
  public update = (supplier: Supplier): Observable<Supplier> => {
    return this.httpService.post<Supplier>(`/update`, supplier)
      .pipe(
        map((response: AxiosResponse<Supplier>) => response.data),
      );
  };
  public delete = (supplier: Supplier): Observable<Supplier> => {
    return this.httpService.post<Supplier>(`/delete`, supplier)
      .pipe(
        map((response: AxiosResponse<Supplier>) => response.data),
      );
  };

  public save = (supplier: Supplier): Observable<Supplier> => {
    return supplier.id ? this.update(supplier) : this.create(supplier);
  };

  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item', itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
  public singleListWarehouse = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/single-list-warehouse', warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };
}

export default new SupplierDetailRepository();
