import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class ProvinceListRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/provinces');
  }

  public list = (provinceSearch: ProvinceSearch): Observable<Province[]> => {
    return this.httpService.get('/', {
      params: provinceSearch,
    })
      .pipe(
        map((response: AxiosResponse<Province[]>) => response.data),
      );
  };

  public count = (provinceSearch: ProvinceSearch): Observable<number> => {
    return this.httpService.get('/count', {
      params: provinceSearch,
    })
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };
}

export default new ProvinceListRepository();
