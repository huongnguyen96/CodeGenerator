
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';

import {ItemStock} from 'models/ItemStock';
import {ItemStockSearch} from 'models/ItemStockSearch';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class ItemUnitOfMeasureMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item-unit-of-measure/item-unit-of-measure-master');
  }

  public count = (itemUnitOfMeasureSearch: ItemUnitOfMeasureSearch): Observable<number> => {
    return this.httpService.post('/count',itemUnitOfMeasureSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemUnitOfMeasureSearch: ItemUnitOfMeasureSearch): Observable<ItemUnitOfMeasure[]> => {
    return this.httpService.post('/list',itemUnitOfMeasureSearch)
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ItemUnitOfMeasure> => {
    return this.httpService.post<ItemUnitOfMeasure>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ItemUnitOfMeasure>) => response.data),
      );
  };
  
  public singleList = (itemStockSearch: ItemStockSearch): Observable<ItemStock[]> => {
    return this.httpService.post('/single-list-item-stock',itemStockSearch)
      .pipe(
        map((response: AxiosResponse<ItemStock[]>) => response.data),
      );
  };
  public singleList = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new ItemUnitOfMeasureMasterRepository();