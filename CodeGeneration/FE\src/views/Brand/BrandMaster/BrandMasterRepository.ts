
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';

import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';

export class BrandMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/brand/brand-master');
  }

  public count = (brandSearch: BrandSearch): Observable<number> => {
    return this.httpService.post('/count',brandSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (brandSearch: BrandSearch): Observable<Brand[]> => {
    return this.httpService.post('/list',brandSearch)
      .pipe(
        map((response: AxiosResponse<Brand[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Brand> => {
    return this.httpService.post<Brand>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Brand>) => response.data),
      );
  };
    
  public delete = (brand: Brand): Observable<Brand> => {
    return this.httpService.post<Brand>(`/delete`, brand)
      .pipe(
        map((response: AxiosResponse<Brand>) => response.data),
      );
  };
  
  public singleListCategory = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/single-list-category',categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };
}

export default new BrandMasterRepository();