import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {District} from 'models/District';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class DistrictDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/districts');
  }

  public get = (id: string): Observable<District> => {
    return this.httpService.get<District>(`/${id}`)
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
    return this.httpService.patch<District>(`/${district.id}`, district)
      .pipe(
        map((response: AxiosResponse<District>) => response.data),
      );
  };

  public save = (district: District): Observable<District> => {
    return district.id ? this.update(district) : this.create(district);
  };

  public listProvince = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.get<Province[]>(`/provinces`, {
      params: provinceSearch,
    })
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };
}

export default new DistrictDetailRepository();
