
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';


export class CategoryDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/category/category-detail');
  }

  public get = (id: number): Observable<Category> => {
    return this.httpService.post<Category>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Category>) => response.data),
      );
  };
  
  public create = (category: Category): Observable<Category> => {
    return this.httpService.post<Category>(`/create`, category)
      .pipe(
        map((response: AxiosResponse<Category>) => response.data),
      );
  };
  public update = (category: Category): Observable<Category> => {
    return this.httpService.post<Category>(`/update`, category)
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
  
  public save = (category: Category): Observable<Category> => {
    return category.id ? this.update(category) : this.create(category);
  };
  
  public singleListCategory = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/single-list-category',categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };
}

export default new CategoryDetailRepository();