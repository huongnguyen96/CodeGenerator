
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';
import {Supplier} from 'models/Supplier';
import {SupplierSearch} from 'models/SupplierSearch';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';
import {ItemStock} from 'models/ItemStock';
import {ItemStockSearch} from 'models/ItemStockSearch';

export class ItemDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item/item-detail');
  }

  public get = (id: number): Observable<Item> => {
    return this.httpService.post<Item>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  
  public create = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/create`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  public update = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/update`, item)
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
  
  public save = (item: Item): Observable<Item> => {
    return item.id ? this.update(item) : this.create(item);
  };
  
  public singleListItemStatus = (itemStatusSearch: ItemStatusSearch): Observable<ItemStatus[]> => {
    return this.httpService.post('/single-list-item-status',itemStatusSearch)
      .pipe(
        map((response: AxiosResponse<ItemStatus[]>) => response.data),
      );
  };
  public singleListSupplier = (supplierSearch: SupplierSearch): Observable<Supplier[]> => {
    return this.httpService.post('/single-list-supplier',supplierSearch)
      .pipe(
        map((response: AxiosResponse<Supplier[]>) => response.data),
      );
  };
  public singleListItemType = (itemTypeSearch: ItemTypeSearch): Observable<ItemType[]> => {
    return this.httpService.post('/single-list-item-type',itemTypeSearch)
      .pipe(
        map((response: AxiosResponse<ItemType[]>) => response.data),
      );
  };
  public singleListItemUnitOfMeasure = (itemUnitOfMeasureSearch: ItemUnitOfMeasureSearch): Observable<ItemUnitOfMeasure[]> => {
    return this.httpService.post('/single-list-item-unit-of-measure',itemUnitOfMeasureSearch)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure[]>) => response.data),
      );
  };
  public singleListItemStock = (itemStockSearch: ItemStockSearch): Observable<ItemStock[]> => {
    return this.httpService.post('/single-list-item-stock',itemStockSearch)
      .pipe(
        map((response: AxiosResponse<ItemStock[]>) => response.data),
      );
  };
}

export default new ItemDetailRepository();