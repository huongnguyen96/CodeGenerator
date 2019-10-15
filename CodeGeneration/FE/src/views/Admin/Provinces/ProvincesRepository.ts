import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class ProvincesRepository extends Repository {
  public constructor() {
    super();
  }

  public list = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.get('/api/provinces', {
      params: provinceSearch,
    })
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };

  public count = (provinceSearch: ProvinceSearch): Observable<number> => {
    return this.httpService.get('/api/provinces/count', {
      params: provinceSearch,
    })
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public get = (id: string): Observable<Province> => {
    return this.httpService.get(`/api/provinces/${id}`)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };

  public create = (province: Province): Observable<Province> => {
    return this.httpService.post<Province>(`/api/provinces/create`, province)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };

  public update = (province: Province): Observable<Province> => {
    return this.httpService.patch<Province>(`/api/provinces/${province.id}`, province)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };

  public save = (province: Province): Observable<Province> => {
    return province.id ? this.update(province) : this.create(province);
  };
}

export default new ProvincesRepository();
