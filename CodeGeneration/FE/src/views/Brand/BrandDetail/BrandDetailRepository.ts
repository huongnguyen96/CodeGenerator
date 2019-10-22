
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';

import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';

export class BrandDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/brand/brand-detail');
  }

  public get = (id: number): Observable<Brand> => {
    return this.httpService.post<Brand>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Brand>) => response.data),
      );
  };
  
  public create = (brand: Brand): Observable<Brand> => {
    return this.httpService.post<Brand>(`/create`, brand)
      .pipe(
        map((response: AxiosResponse<Brand>) => response.data),
      );
  };
  public update = (brand: Brand): Observable<Brand> => {
    return this.httpService.post<Brand>(`/update`, brand)
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
  
  public save = (brand: Brand): Observable<Brand> => {
    return brand.id ? this.update(brand) : this.create(brand);
  };
  
  public singleListCategory = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/single-list-category',categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };
}

export default new BrandDetailRepository();