
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class MerchantDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/merchant/merchant-detail');
  }

  public get = (id: number): Observable<Merchant> => {
    return this.httpService.post<Merchant>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Merchant>) => response.data),
      );
  };

  public create = (merchant: Merchant): Observable<Merchant> => {
    return this.httpService.post<Merchant>(`/create`, merchant)
      .pipe(
        map((response: AxiosResponse<Merchant>) => response.data),
      );
  };
  public update = (merchant: Merchant): Observable<Merchant> => {
    return this.httpService.post<Merchant>(`/update`, merchant)
      .pipe(
        map((response: AxiosResponse<Merchant>) => response.data),
      );
  };
  public delete = (merchant: Merchant): Observable<Merchant> => {
    return this.httpService.post<Merchant>(`/delete`, merchant)
      .pipe(
        map((response: AxiosResponse<Merchant>) => response.data),
      );
  };

  public save = (merchant: Merchant): Observable<Merchant> => {
    return merchant.id ? this.update(merchant) : this.create(merchant);
  };

}

export default new MerchantDetailRepository();
