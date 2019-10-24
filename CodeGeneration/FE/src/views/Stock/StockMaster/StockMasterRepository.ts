
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Stock} from 'models/Stock';
import {StockSearch} from 'models/StockSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class StockMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/stock/stock-master');
  }

  public count = (stockSearch: StockSearch): Observable<number> => {
    return this.httpService.post('/count', stockSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (stockSearch: StockSearch): Observable<Stock[]> => {
    return this.httpService.post('/list', stockSearch)
      .pipe(
        map((response: AxiosResponse<Stock[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Stock> => {
    return this.httpService.post<Stock>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Stock>) => response.data),
      );
  };

  public delete = (stock: Stock): Observable<Stock> => {
    return this.httpService.post<Stock>(`/delete`, stock)
      .pipe(
        map((response: AxiosResponse<Stock>) => response.data),
      );
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

export default new StockMasterRepository();
