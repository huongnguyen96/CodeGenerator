
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class PartnerDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/partner/partner-detail');
  }

  public get = (id: number): Observable<Partner> => {
    return this.httpService.post<Partner>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  
  public create = (partner: Partner): Observable<Partner> => {
    return this.httpService.post<Partner>(`/create`, partner)
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  public update = (partner: Partner): Observable<Partner> => {
    return this.httpService.post<Partner>(`/update`, partner)
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  public delete = (partner: Partner): Observable<Partner> => {
    return this.httpService.post<Partner>(`/delete`, partner)
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  
  public save = (partner: Partner): Observable<Partner> => {
    return partner.id ? this.update(partner) : this.create(partner);
  };
  
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
  public singleListWarehouse = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/single-list-warehouse',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };
}

export default new PartnerDetailRepository();