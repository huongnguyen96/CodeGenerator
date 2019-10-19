
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';
import {ItemStock} from 'models/ItemStock';
import {ItemStockSearch} from 'models/ItemStockSearch';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';
import {Supplier} from 'models/Supplier';
import {SupplierSearch} from 'models/SupplierSearch';

export class ItemMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item/item-master');
  }

  public count = (itemSearch: ItemSearch): Observable<number> => {
    return this.httpService.post('/count', itemSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/list', itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Item> => {
    return this.httpService.post<Item>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };

  public delete = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/delete`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };

  public singleListItemStatus = (itemStatusSearch: ItemStatusSearch): Observable<ItemStatus[]> => {
    return this.httpService.post('/single-list-item-status', itemStatusSearch)
      .pipe(
        map((response: AxiosResponse<ItemStatus[]>) => response.data),
      );
  };
  public singleListSupplier = (supplierSearch: SupplierSearch): Observable<Supplier[]> => {
    return this.httpService.post('/single-list-supplier', supplierSearch)
      .pipe(
        map((response: AxiosResponse<Supplier[]>) => response.data),
      );
  };
  public singleListItemType = (itemTypeSearch: ItemTypeSearch): Observable<ItemType[]> => {
    return this.httpService.post('/single-list-item-type', itemTypeSearch)
      .pipe(
        map((response: AxiosResponse<ItemType[]>) => response.data),
      );
  };
  public singleListItemUnitOfMeasure = (itemUnitOfMeasureSearch: ItemUnitOfMeasureSearch): Observable<ItemUnitOfMeasure[]> => {
    return this.httpService.post('/single-list-item-unit-of-measure', itemUnitOfMeasureSearch)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure[]>) => response.data),
      );
  };
  public singleList = (itemStockSearch: ItemStockSearch): Observable<ItemStock[]> => {
    return this.httpService.post('/single-list-item-stock', itemStockSearch)
      .pipe(
        map((response: AxiosResponse<ItemStock[]>) => response.data),
      );
  };
}

export default new ItemMasterRepository();
