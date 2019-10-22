
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';

import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';

export class DistrictMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/district/district-master');
  }

  public count = (districtSearch: DistrictSearch): Observable<number> => {
    return this.httpService.post('/count',districtSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (districtSearch: DistrictSearch): Observable<District[]> => {
    return this.httpService.post('/list',districtSearch)
      .pipe(
        map((response: AxiosResponse<District[]>) => response.data),
      );
  };

  public get = (id: number): Observable<District> => {
    return this.httpService.post<District>('/get', { id })
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
  
  public singleListProvince = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.post('/single-list-province',provinceSearch)
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };
}

export default new DistrictMasterRepository();