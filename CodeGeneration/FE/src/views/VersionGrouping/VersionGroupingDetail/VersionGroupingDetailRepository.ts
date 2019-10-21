
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {VersionGrouping} from 'models/VersionGrouping';
import {VersionGroupingSearch} from 'models/VersionGroupingSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Version} from 'models/Version';
import {VersionSearch} from 'models/VersionSearch';

export class VersionGroupingDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/version-grouping/version-grouping-detail');
  }

  public get = (id: number): Observable<VersionGrouping> => {
    return this.httpService.post<VersionGrouping>('/get', { id })
      .pipe(
        map((response: AxiosResponse<VersionGrouping>) => response.data),
      );
  };
  
  public create = (versionGrouping: VersionGrouping): Observable<VersionGrouping> => {
    return this.httpService.post<VersionGrouping>(`/create`, versionGrouping)
      .pipe(
        map((response: AxiosResponse<VersionGrouping>) => response.data),
      );
  };
  public update = (versionGrouping: VersionGrouping): Observable<VersionGrouping> => {
    return this.httpService.post<VersionGrouping>(`/update`, versionGrouping)
      .pipe(
        map((response: AxiosResponse<VersionGrouping>) => response.data),
      );
  };
  public delete = (versionGrouping: VersionGrouping): Observable<VersionGrouping> => {
    return this.httpService.post<VersionGrouping>(`/delete`, versionGrouping)
      .pipe(
        map((response: AxiosResponse<VersionGrouping>) => response.data),
      );
  };
  
  public save = (versionGrouping: VersionGrouping): Observable<VersionGrouping> => {
    return versionGrouping.id ? this.update(versionGrouping) : this.create(versionGrouping);
  };
  
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
  public singleListVersion = (versionSearch: VersionSearch): Observable<Version[]> => {
    return this.httpService.post('/single-list-version',versionSearch)
      .pipe(
        map((response: AxiosResponse<Version[]>) => response.data),
      );
  };
}

export default new VersionGroupingDetailRepository();