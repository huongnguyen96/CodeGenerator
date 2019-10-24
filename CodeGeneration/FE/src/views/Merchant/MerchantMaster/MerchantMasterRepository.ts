
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class MerchantMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/merchant/merchant-master');
  }

  public count = (merchantSearch: MerchantSearch): Observable<number> => {
    return this.httpService.post('/count', merchantSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (merchantSearch: MerchantSearch): Observable<Merchant[]> => {
    return this.httpService.post('/list', merchantSearch)
      .pipe(
        map((response: AxiosResponse<Merchant[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Merchant> => {
    return this.httpService.post<Merchant>('/get', { id })
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

}

export default new MerchantMasterRepository();
