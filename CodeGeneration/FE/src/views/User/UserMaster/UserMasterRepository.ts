
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {User} from 'models/User';
import {UserSearch} from 'models/UserSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class UserMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/user/user-master');
  }

  public count = (userSearch: UserSearch): Observable<number> => {
    return this.httpService.post('/count', userSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (userSearch: UserSearch): Observable<User[]> => {
    return this.httpService.post('/list', userSearch)
      .pipe(
        map((response: AxiosResponse<User[]>) => response.data),
      );
  };

  public get = (id: number): Observable<User> => {
    return this.httpService.post<User>('/get', { id })
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

}

export default new UserMasterRepository();
