
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {MerchantAddress} from 'models/MerchantAddress';
import {MerchantAddressSearch} from 'models/MerchantAddressSearch';

import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';

export class MerchantAddressMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/merchant-address/merchant-address-master');
  }

  public count = (merchantAddressSearch: MerchantAddressSearch): Observable<number> => {
    return this.httpService.post('/count',merchantAddressSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (merchantAddressSearch: MerchantAddressSearch): Observable<MerchantAddress[]> => {
    return this.httpService.post('/list',merchantAddressSearch)
      .pipe(
        map((response: AxiosResponse<MerchantAddress[]>) => response.data),
      );
  };

  public get = (id: number): Observable<MerchantAddress> => {
    return this.httpService.post<MerchantAddress>('/get', { id })
      .pipe(
        map((response: AxiosResponse<MerchantAddress>) => response.data),
      );
  };
    
  public delete = (merchantAddress: MerchantAddress): Observable<MerchantAddress> => {
    return this.httpService.post<MerchantAddress>(`/delete`, merchantAddress)
      .pipe(
        map((response: AxiosResponse<MerchantAddress>) => response.data),
      );
  };
  
  public singleListMerchant = (merchantSearch: MerchantSearch): Observable<Merchant[]> => {
    return this.httpService.post('/single-list-merchant',merchantSearch)
      .pipe(
        map((response: AxiosResponse<Merchant[]>) => response.data),
      );
  };
}

export default new MerchantAddressMasterRepository();