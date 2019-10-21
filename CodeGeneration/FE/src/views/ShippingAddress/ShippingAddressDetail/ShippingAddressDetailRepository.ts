
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ShippingAddress} from 'models/ShippingAddress';
import {ShippingAddressSearch} from 'models/ShippingAddressSearch';

import {Bool} from 'models/Bool';
import {BoolSearch} from 'models/BoolSearch';
import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

export class ShippingAddressDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/shipping-address/shipping-address-detail');
  }

  public get = (id: number): Observable<ShippingAddress> => {
    return this.httpService.post<ShippingAddress>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ShippingAddress>) => response.data),
      );
  };
  
  public create = (shippingAddress: ShippingAddress): Observable<ShippingAddress> => {
    return this.httpService.post<ShippingAddress>(`/create`, shippingAddress)
      .pipe(
        map((response: AxiosResponse<ShippingAddress>) => response.data),
      );
  };
  public update = (shippingAddress: ShippingAddress): Observable<ShippingAddress> => {
    return this.httpService.post<ShippingAddress>(`/update`, shippingAddress)
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
  
  public save = (shippingAddress: ShippingAddress): Observable<ShippingAddress> => {
    return shippingAddress.id ? this.update(shippingAddress) : this.create(shippingAddress);
  };
  
  public singleListBool = (boolSearch: BoolSearch): Observable<Bool[]> => {
    return this.httpService.post('/single-list-bool',boolSearch)
      .pipe(
        map((response: AxiosResponse<Bool[]>) => response.data),
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

export default new ShippingAddressDetailRepository();