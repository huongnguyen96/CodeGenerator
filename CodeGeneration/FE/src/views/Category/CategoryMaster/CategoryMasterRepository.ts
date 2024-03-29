
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class CategoryMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/category/category-master');
  }

  public count = (categorySearch: CategorySearch): Observable<number> => {
    return this.httpService.post('/count', categorySearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/list', categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Category> => {
    return this.httpService.post<Category>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Category>) => response.data),
      );
  };

  public delete = (category: Category): Observable<Category> => {
    return this.httpService.post<Category>(`/delete`, category)
      .pipe(
        map((response: AxiosResponse<Category>) => response.data),
      );
  };

  public singleListCategory = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/single-list-category', categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };
}

export default new CategoryMasterRepository();
