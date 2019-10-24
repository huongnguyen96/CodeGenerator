
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {EVoucherContent} from 'models/EVoucherContent';
import {EVoucherContentSearch} from 'models/EVoucherContentSearch';

import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';

export class EVoucherContentMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/e-voucher-content/e-voucher-content-master');
  }

  public count = (eVoucherContentSearch: EVoucherContentSearch): Observable<number> => {
    return this.httpService.post('/count',eVoucherContentSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (eVoucherContentSearch: EVoucherContentSearch): Observable<EVoucherContent[]> => {
    return this.httpService.post('/list',eVoucherContentSearch)
      .pipe(
        map((response: AxiosResponse<EVoucherContent[]>) => response.data),
      );
  };

  public get = (id: number): Observable<EVoucherContent> => {
    return this.httpService.post<EVoucherContent>('/get', { id })
      .pipe(
        map((response: AxiosResponse<EVoucherContent>) => response.data),
      );
  };
    
  public delete = (eVoucherContent: EVoucherContent): Observable<EVoucherContent> => {
    return this.httpService.post<EVoucherContent>(`/delete`, eVoucherContent)
      .pipe(
        map((response: AxiosResponse<EVoucherContent>) => response.data),
      );
  };
  
  public singleListEVoucher = (eVoucherSearch: EVoucherSearch): Observable<EVoucher[]> => {
    return this.httpService.post('/single-list-e-voucher',eVoucherSearch)
      .pipe(
        map((response: AxiosResponse<EVoucher[]>) => response.data),
      );
  };
}

export default new EVoucherContentMasterRepository();