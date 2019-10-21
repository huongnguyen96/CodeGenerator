
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';
import {Stock} from 'models/Stock';
import {StockSearch} from 'models/StockSearch';

export class WarehouseDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/warehouse/warehouse-detail');
  }

  public get = (id: number): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Warehouse>) => response.data),
      );
  };
  
  public create = (warehouse: Warehouse): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>(`/create`, warehouse)
      .pipe(
        map((response: AxiosResponse<Warehouse>) => response.data),
      );
  };
  public update = (warehouse: Warehouse): Observable<Warehouse> => {
    return this.httpService.post<Warehouse>(`/update`, warehouse)
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
  
  public save = (warehouse: Warehouse): Observable<Warehouse> => {
    return warehouse.id ? this.update(warehouse) : this.create(warehouse);
  };
  
  public singleListPartner = (partnerSearch: PartnerSearch): Observable<Partner[]> => {
    return this.httpService.post('/single-list-partner',partnerSearch)
      .pipe(
        map((response: AxiosResponse<Partner[]>) => response.data),
      );
  };
  public singleListStock = (stockSearch: StockSearch): Observable<Stock[]> => {
    return this.httpService.post('/single-list-stock',stockSearch)
      .pipe(
        map((response: AxiosResponse<Stock[]>) => response.data),
      );
  };
}

export default new WarehouseDetailRepository();