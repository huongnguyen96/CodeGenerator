
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Version} from 'models/Version';
import {VersionSearch} from 'models/VersionSearch';

import {VersionGrouping} from 'models/VersionGrouping';
import {VersionGroupingSearch} from 'models/VersionGroupingSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

export class VersionDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/version/version-detail');
  }

  public get = (id: number): Observable<Version> => {
    return this.httpService.post<Version>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Version>) => response.data),
      );
  };
  
  public create = (version: Version): Observable<Version> => {
    return this.httpService.post<Version>(`/create`, version)
      .pipe(
        map((response: AxiosResponse<Version>) => response.data),
      );
  };
  public update = (version: Version): Observable<Version> => {
    return this.httpService.post<Version>(`/update`, version)
      .pipe(
        map((response: AxiosResponse<Version>) => response.data),
      );
  };
  public delete = (version: Version): Observable<Version> => {
    return this.httpService.post<Version>(`/delete`, version)
      .pipe(
        map((response: AxiosResponse<Version>) => response.data),
      );
  };
  
  public save = (version: Version): Observable<Version> => {
    return version.id ? this.update(version) : this.create(version);
  };
  
  public singleListVersionGrouping = (versionGroupingSearch: VersionGroupingSearch): Observable<VersionGrouping[]> => {
    return this.httpService.post('/single-list-version-grouping',versionGroupingSearch)
      .pipe(
        map((response: AxiosResponse<VersionGrouping[]>) => response.data),
      );
  };
  public singleListUnit = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
  public singleListUnit = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
  public singleListUnit = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
}

export default new VersionDetailRepository();