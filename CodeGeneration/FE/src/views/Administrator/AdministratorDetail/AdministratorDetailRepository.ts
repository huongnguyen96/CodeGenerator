
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Administrator} from 'models/Administrator';
import {AdministratorSearch} from 'models/AdministratorSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class AdministratorDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/administrator/administrator-detail');
  }

  public get = (id: number): Observable<Administrator> => {
    return this.httpService.post<Administrator>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Administrator>) => response.data),
      );
  };

  public create = (administrator: Administrator): Observable<Administrator> => {
    return this.httpService.post<Administrator>(`/create`, administrator)
      .pipe(
        map((response: AxiosResponse<Administrator>) => response.data),
      );
  };
  public update = (administrator: Administrator): Observable<Administrator> => {
    return this.httpService.post<Administrator>(`/update`, administrator)
      .pipe(
        map((response: AxiosResponse<Administrator>) => response.data),
      );
  };
  public delete = (administrator: Administrator): Observable<Administrator> => {
    return this.httpService.post<Administrator>(`/delete`, administrator)
      .pipe(
        map((response: AxiosResponse<Administrator>) => response.data),
      );
  };

  public save = (administrator: Administrator): Observable<Administrator> => {
    return administrator.id ? this.update(administrator) : this.create(administrator);
  };

}

export default new AdministratorDetailRepository();
