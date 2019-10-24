
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

export class UnitDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/unit/unit-detail');
  }

  public get = (id: number): Observable<Unit> => {
    return this.httpService.post<Unit>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Unit>) => response.data),
      );
  };
  
  public create = (unit: Unit): Observable<Unit> => {
    return this.httpService.post<Unit>(`/create`, unit)
      .pipe(
        map((response: AxiosResponse<Unit>) => response.data),
      );
  };
  public update = (unit: Unit): Observable<Unit> => {
    return this.httpService.post<Unit>(`/update`, unit)
      .pipe(
        map((response: AxiosResponse<Unit>) => response.data),
      );
  };
  public delete = (unit: Unit): Observable<Unit> => {
    return this.httpService.post<Unit>(`/delete`, unit)
      .pipe(
        map((response: AxiosResponse<Unit>) => response.data),
      );
  };
  
  public save = (unit: Unit): Observable<Unit> => {
    return unit.id ? this.update(unit) : this.create(unit);
  };
  
  public singleListVariation = (variationSearch: VariationSearch): Observable<Variation[]> => {
    return this.httpService.post('/single-list-variation',variationSearch)
      .pipe(
        map((response: AxiosResponse<Variation[]>) => response.data),
      );
  };
}

export default new UnitDetailRepository();