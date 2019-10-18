
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemStock} from 'models/ItemStock';
import {ItemStockSearch} from 'models/ItemStockSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class ItemStockMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-stock/item-stock-master');
  }

  public count = (itemStockSearch: ItemStockSearch): Observable<number> => {
    return this.httpService.post('/count',itemStockSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemStockSearch: ItemStockSearch): Observable<ItemStock[]> => {
    return this.httpService.post('/list',itemStockSearch)
      .pipe(
        map((response: AxiosResponse<ItemStock[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ItemStock> => {
    return this.httpService.post<ItemStock>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemStock>) => response.data),
      );
  };
  
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
  public singleListItemUnitOfMeasure = (itemUnitOfMeasureSearch: ItemUnitOfMeasureSearch): Observable<ItemUnitOfMeasure[]> => {
    return this.httpService.post('/single-list-item-unit-of-measure',itemUnitOfMeasureSearch)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure[]>) => response.data),
      );
  };
  public singleListWarehouse = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/single-list-warehouse',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };
}

export default new ItemStockMasterRepository();