import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Province} from 'models/Province';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class ProvinceDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/provinces');
  }

  public get = (id: string): Observable<Province> => {
    return this.httpService.get(`/${id}`)
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
    return this.httpService.patch<Province>(`/${province.id}`, province)
      .pipe(
        map((response: AxiosResponse<Province>) => response.data),
      );
  };

  public save = (province: Province): Observable<Province> => {
    return province.id ? this.update(province) : this.create(province);
  };
}

export default new ProvinceDetailRepository();
