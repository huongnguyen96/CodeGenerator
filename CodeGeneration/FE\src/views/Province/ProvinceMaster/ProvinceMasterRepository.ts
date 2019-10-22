
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';


export class ProvinceMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/province/province-master');
  }

  public count = (provinceSearch: ProvinceSearch): Observable<number> => {
    return this.httpService.post('/count',provinceSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.post('/list',provinceSearch)
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Province> => {
    return this.httpService.post<Province>('/get', { id })
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
  
}

export default new ProvinceMasterRepository();