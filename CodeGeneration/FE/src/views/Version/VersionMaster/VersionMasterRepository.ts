
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

export class VersionMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/version/version-master');
  }

  public count = (versionSearch: VersionSearch): Observable<number> => {
    return this.httpService.post('/count',versionSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (versionSearch: VersionSearch): Observable<Version[]> => {
    return this.httpService.post('/list',versionSearch)
      .pipe(
        map((response: AxiosResponse<Version[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Version> => {
    return this.httpService.post<Version>('/get', { id })
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
  
  public singleListVersionGrouping = (versionGroupingSearch: VersionGroupingSearch): Observable<VersionGrouping[]> => {
    return this.httpService.post('/single-list-version-grouping',versionGroupingSearch)
      .pipe(
        map((response: AxiosResponse<VersionGrouping[]>) => response.data),
      );
  };
  public singleList = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
  public singleList = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
  public singleList = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
}

export default new VersionMasterRepository();