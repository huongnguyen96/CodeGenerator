
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';

export class WardMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/ward/ward-master');
  }

  public count = (wardSearch: WardSearch): Observable<number> => {
    return this.httpService.post('/count',wardSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (wardSearch: WardSearch): Observable<Ward[]> => {
    return this.httpService.post('/list',wardSearch)
      .pipe(
        map((response: AxiosResponse<Ward[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Ward> => {
    return this.httpService.post<Ward>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Ward>) => response.data),
      );
  };
    
  public delete = (ward: Ward): Observable<Ward> => {
    return this.httpService.post<Ward>(`/delete`, ward)
      .pipe(
        map((response: AxiosResponse<Ward>) => response.data),
      );
  };
  
  public singleListDistrict = (districtSearch: DistrictSearch): Observable<District[]> => {
    return this.httpService.post('/single-list-district',districtSearch)
      .pipe(
        map((response: AxiosResponse<District[]>) => response.data),
      );
  };
}

export default new WardMasterRepository();