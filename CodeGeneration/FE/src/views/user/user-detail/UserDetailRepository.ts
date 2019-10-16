
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {User} from 'models/User';
import {UserSearch} from 'models/UserSearch';

import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

export class UserDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/user/user-detail');
  }

  public get = (id: number): Observable<User> => {
    return this.httpService.post<User>('/get', { id })
      .pipe(
        map((response: AxiosResponse<User>) => response.data),
      );
  };
  
  public create = (user: User): Observable<User> => {
    return this.httpService.post<User>(`/create`, user)
      .pipe(
        map((response: AxiosResponse<User>) => response.data),
      );
  };
  public update = (user: User): Observable<User> => {
    return this.httpService.post<User>(`/create`, user)
      .pipe(
        map((response: AxiosResponse<User>) => response.data),
      );
  };
  public delete = (user: User): Observable<User> => {
    return this.httpService.post<User>(`/create`, user)
      .pipe(
        map((response: AxiosResponse<User>) => response.data),
      );
  };
  
  public save = (user: User): Observable<User> => {
    return user.id ? this.update(user) : this.create(user);
  };
  
  public singleList = (warehouseSearch: WarehouseSearch): Observable<Warehouse[]> => {
    return this.httpService.post('/single-list-warehouse',warehouseSearch)
      .pipe(
        map((response: AxiosResponse<Warehouse[]>) => response.data),
      );
  };
}

export default new UserDetailRepository();