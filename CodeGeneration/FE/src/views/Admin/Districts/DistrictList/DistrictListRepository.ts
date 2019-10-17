import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class DistrictListRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/districts');
  }

  public list = (districtSearch: DistrictSearch): Observable<District[]> => {
    return this.httpService.get<District[]>(`/`, {
      params: districtSearch,
    })
      .pipe(
        map((response: AxiosResponse<District[]>) => response.data),
      );
  };

  public count = (districtSearch: DistrictSearch): Observable<number> => {
    return this.httpService.get<number>(`/count`, {
      params: districtSearch,
    })
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public delete = (id: string): Observable<District> => {
    return this.httpService.delete<District>(`/${id}`)
      .pipe(
        map((response: AxiosResponse<District>) => response.data),
      );
  };
}

export default new DistrictListRepository();
