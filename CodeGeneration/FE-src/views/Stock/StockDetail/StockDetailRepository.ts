
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Stock} from 'models/Stock';
import {StockSearch} from 'models/StockSearch';

import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class StockDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/stock/stock-detail');
  }

  public get = (id: number): Observable<Stock> => {
    return this.httpService.post<Stock>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Stock>) => response.data),
      );
  };
  
  public create = (stock: Stock): Observable<Stock> => {
    return this.httpService.post<Stock>(`/create`, stock)
      .pipe(
        map((response: AxiosResponse<Stock>) => response.data),
      );
  };
  public update = (stock: Stock): Observable<Stock> => {
    return this.httpService.post<Stock>(`/update`, stock)
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
  
  public save = (stock: Stock): Observable<Stock> => {
    return stock.id ? this.update(stock) : this.create(stock);
  };
  
  public singleListUnit = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
  public singleListWarehouse = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/single-list-warehouse',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };
}

export default new StockDetailRepository();