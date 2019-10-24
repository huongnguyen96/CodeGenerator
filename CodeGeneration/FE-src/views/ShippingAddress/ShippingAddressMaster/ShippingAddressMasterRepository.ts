
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ShippingAddress} from 'models/ShippingAddress';
import {ShippingAddressSearch} from 'models/ShippingAddressSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

export class ShippingAddressMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/shipping-address/shipping-address-master');
  }

  public count = (shippingAddressSearch: ShippingAddressSearch): Observable<number> => {
    return this.httpService.post('/count',shippingAddressSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (shippingAddressSearch: ShippingAddressSearch): Observable<ShippingAddress[]> => {
    return this.httpService.post('/list',shippingAddressSearch)
      .pipe(
        map((response: AxiosResponse<ShippingAddress[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ShippingAddress> => {
    return this.httpService.post<ShippingAddress>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ShippingAddress>) => response.data),
      );
  };
    
  public delete = (shippingAddress: ShippingAddress): Observable<ShippingAddress> => {
    return this.httpService.post<ShippingAddress>(`/delete`, shippingAddress)
      .pipe(
        map((response: AxiosResponse<ShippingAddress>) => response.data),
      );
  };
  
  public singleListCustomer = (customerSearch: CustomerSearch): Observable<Customer[]> => {
    return this.httpService.post('/single-list-customer',customerSearch)
      .pipe(
        map((response: AxiosResponse<Customer[]>) => response.data),
      );
  };
  public singleListDistrict = (districtSearch: DistrictSearch): Observable<District[]> => {
    return this.httpService.post('/single-list-district',districtSearch)
      .pipe(
        map((response: AxiosResponse<District[]>) => response.data),
      );
  };
  public singleListProvince = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.post('/single-list-province',provinceSearch)
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };
  public singleListWard = (wardSearch: WardSearch): Observable<Ward[]> => {
    return this.httpService.post('/single-list-ward',wardSearch)
      .pipe(
        map((response: AxiosResponse<Ward[]>) => response.data),
      );
  };
}

export default new ShippingAddressMasterRepository();