
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Administrator} from 'models/Administrator';
import {AdministratorSearch} from 'models/AdministratorSearch';


export class AdministratorMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/administrator/administrator-master');
  }

  public count = (administratorSearch: AdministratorSearch): Observable<number> => {
    return this.httpService.post('/count',administratorSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (administratorSearch: AdministratorSearch): Observable<Administrator[]> => {
    return this.httpService.post('/list',administratorSearch)
      .pipe(
        map((response: AxiosResponse<Administrator[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Administrator> => {
    return this.httpService.post<Administrator>('/get', { id })
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
  
}

export default new AdministratorMasterRepository();