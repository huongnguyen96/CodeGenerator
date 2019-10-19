
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {ItemStock} from 'models/ItemStock';
import {ItemStockSearch} from 'models/ItemStockSearch';

export class ItemUnitOfMeasureDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-unit-of-measure/item-unit-of-measure-detail');
  }

  public get = (id: number): Observable<ItemUnitOfMeasure> => {
    return this.httpService.post<ItemUnitOfMeasure>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure>) => response.data),
      );
  };

  public create = (itemUnitOfMeasure: ItemUnitOfMeasure): Observable<ItemUnitOfMeasure> => {
    return this.httpService.post<ItemUnitOfMeasure>(`/create`, itemUnitOfMeasure)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure>) => response.data),
      );
  };
  public update = (itemUnitOfMeasure: ItemUnitOfMeasure): Observable<ItemUnitOfMeasure> => {
    return this.httpService.post<ItemUnitOfMeasure>(`/update`, itemUnitOfMeasure)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure>) => response.data),
      );
  };
  public delete = (itemUnitOfMeasure: ItemUnitOfMeasure): Observable<ItemUnitOfMeasure> => {
    return this.httpService.post<ItemUnitOfMeasure>(`/delete`, itemUnitOfMeasure)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure>) => response.data),
      );
  };

  public save = (itemUnitOfMeasure: ItemUnitOfMeasure): Observable<ItemUnitOfMeasure> => {
    return itemUnitOfMeasure.id ? this.update(itemUnitOfMeasure) : this.create(itemUnitOfMeasure);
  };

  public singleListItemStock = (itemStockSearch: ItemStockSearch): Observable<ItemStock[]> => {
    return this.httpService.post('/single-list-item-stock', itemStockSearch)
      .pipe(
        map((response: AxiosResponse<ItemStock[]>) => response.data),
      );
  };
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item', itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new ItemUnitOfMeasureDetailRepository();
