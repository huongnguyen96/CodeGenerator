
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';

export class WardDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/ward/ward-detail');
  }

  public get = (id: number): Observable<Ward> => {
    return this.httpService.post<Ward>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Ward>) => response.data),
      );
  };
  
  public create = (ward: Ward): Observable<Ward> => {
    return this.httpService.post<Ward>(`/create`, ward)
      .pipe(
        map((response: AxiosResponse<Ward>) => response.data),
      );
  };
  public update = (ward: Ward): Observable<Ward> => {
    return this.httpService.post<Ward>(`/update`, ward)
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
  
  public save = (ward: Ward): Observable<Ward> => {
    return ward.id ? this.update(ward) : this.create(ward);
  };
  
  public singleListDistrict = (districtSearch: DistrictSearch): Observable<District[]> => {
    return this.httpService.post('/single-list-district',districtSearch)
      .pipe(
        map((response: AxiosResponse<District[]>) => response.data),
      );
  };
}

export default new WardDetailRepository();