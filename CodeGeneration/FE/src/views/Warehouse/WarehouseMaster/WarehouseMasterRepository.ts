
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';

export class WarehouseMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/warehouse/warehouse-master');
  }

  public count = (warehouseSearch: WarehouseSearch): Observable<number> => {
    return this.httpService.post('/count',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/list',warehouseSearch)
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
  
  public singleListPartner = (partnerSearch: PartnerSearch): Observable<Partner[]> => {
    return this.httpService.post('/single-list-partner',partnerSearch)
      .pipe(
        map((response: AxiosResponse<Partner[]>) => response.data),
      );
  };
  public singleList = (stockSearch: StockSearch): Observable<Stock[]> => {
    return this.httpService.post('/single-list-stock',stockSearch)
      .pipe(
        map((response: AxiosResponse<Stock[]>) => response.data),
      );
  };
}

export default new WarehouseMasterRepository();