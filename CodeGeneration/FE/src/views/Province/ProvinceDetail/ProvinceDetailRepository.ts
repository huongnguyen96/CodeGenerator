
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';

import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import {ShippingAddress} from 'models/ShippingAddress';
import {ShippingAddressSearch} from 'models/ShippingAddressSearch';

export class ProvinceDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/province/province-detail');
  }

  public get = (id: number): Observable<Province> => {
    return this.httpService.post<Province>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };
  
  public create = (province: Province): Observable<Province> => {
    return this.httpService.post<Province>(`/create`, province)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };
  public update = (province: Province): Observable<Province> => {
    return this.httpService.post<Province>(`/update`, province)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };
  public delete = (province: Province): Observable<Province> => {
    return this.httpService.post<Province>(`/delete`, province)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };
  
  public save = (province: Province): Observable<Province> => {
    return province.id ? this.update(province) : this.create(province);
  };
  
  public singleListDistrict = (districtSearch: DistrictSearch): Observable<District[]> => {
    return this.httpService.post('/single-list-district',districtSearch)
      .pipe(
        map((response: AxiosResponse<District[]>) => response.data),
      );
  };
  public singleListShippingAddress = (shippingAddressSearch: ShippingAddressSearch): Observable<ShippingAddress[]> => {
    return this.httpService.post('/single-list-shipping-address',shippingAddressSearch)
      .pipe(
        map((response: AxiosResponse<ShippingAddress[]>) => response.data),
      );
  };
}

export default new ProvinceDetailRepository();