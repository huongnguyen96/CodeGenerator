
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {EVoucherContent} from 'models/EVoucherContent';
import {EVoucherContentSearch} from 'models/EVoucherContentSearch';

import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';

export class EVoucherContentDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/e-voucher-content/e-voucher-content-detail');
  }

  public get = (id: number): Observable<EVoucherContent> => {
    return this.httpService.post<EVoucherContent>('/get', { id })
      .pipe(
        map((response: AxiosResponse<EVoucherContent>) => response.data),
      );
  };
  
  public create = (eVoucherContent: EVoucherContent): Observable<EVoucherContent> => {
    return this.httpService.post<EVoucherContent>(`/create`, eVoucherContent)
      .pipe(
        map((response: AxiosResponse<EVoucherContent>) => response.data),
      );
  };
  public update = (eVoucherContent: EVoucherContent): Observable<EVoucherContent> => {
    return this.httpService.post<EVoucherContent>(`/update`, eVoucherContent)
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
  
  public save = (eVoucherContent: EVoucherContent): Observable<EVoucherContent> => {
    return eVoucherContent.id ? this.update(eVoucherContent) : this.create(eVoucherContent);
  };
  
  public singleListEVoucher = (eVoucherSearch: EVoucherSearch): Observable<EVoucher[]> => {
    return this.httpService.post('/single-list-e-voucher',eVoucherSearch)
      .pipe(
        map((response: AxiosResponse<EVoucher[]>) => response.data),
      );
  };
}

export default new EVoucherContentDetailRepository();