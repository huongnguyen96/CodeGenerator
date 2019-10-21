
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';

import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {ShippingAddress} from 'models/ShippingAddress';
import {ShippingAddressSearch} from 'models/ShippingAddressSearch';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

export class DistrictDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/district/district-detail');
  }

  public get = (id: number): Observable<District> => {
    return this.httpService.post<District>('/get', { id })
      .pipe(
        map((response: AxiosResponse<District>) => response.data),
      );
  };
  
  public create = (district: District): Observable<District> => {
    return this.httpService.post<District>(`/create`, district)
      .pipe(
        map((response: AxiosResponse<District>) => response.data),
      );
  };
  public update = (district: District): Observable<District> => {
    return this.httpService.post<District>(`/update`, district)
      .pipe(
        map((response: AxiosResponse<District>) => response.data),
      );
  };
  public delete = (district: District): Observable<District> => {
    return this.httpService.post<District>(`/delete`, district)
      .pipe(
        map((response: AxiosResponse<District>) => response.data),
      );
  };
  
  public save = (district: District): Observable<District> => {
    return district.id ? this.update(district) : this.create(district);
  };
  
  public singleListProvince = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.post('/single-list-province',provinceSearch)
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };
  public singleListShippingAddress = (shippingAddressSearch: ShippingAddressSearch): Observable<ShippingAddress[]> => {
    return this.httpService.post('/single-list-shipping-address',shippingAddressSearch)
      .pipe(
        map((response: AxiosResponse<ShippingAddress[]>) => response.data),
      );
  };
  public singleListWard = (wardSearch: WardSearch): Observable<Ward[]> => {
    return this.httpService.post('/single-list-ward',wardSearch)
      .pipe(
        map((response: AxiosResponse<Ward[]>) => response.data),
      );
  };
}

export default new DistrictDetailRepository();