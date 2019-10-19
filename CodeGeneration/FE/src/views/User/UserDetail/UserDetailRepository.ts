
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {User} from 'models/User';
import {UserSearch} from 'models/UserSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

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
    return this.httpService.post<User>(`/update`, user)
      .pipe(
        map((response: AxiosResponse<User>) => response.data),
      );
  };
  public delete = (user: User): Observable<User> => {
    return this.httpService.post<User>(`/delete`, user)
      .pipe(
        map((response: AxiosResponse<User>) => response.data),
      );
  };

  public save = (user: User): Observable<User> => {
    return user.id ? this.update(user) : this.create(user);
  };

}

export default new UserDetailRepository();
