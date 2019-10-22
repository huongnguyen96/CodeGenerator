
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

export class UnitMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/unit/unit-master');
  }

  public count = (unitSearch: UnitSearch): Observable<number> => {
    return this.httpService.post('/count',unitSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/list',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Unit> => {
    return this.httpService.post<Unit>('/get', { id })
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
  
  public singleListVariation = (variationSearch: VariationSearch): Observable<Variation[]> => {
    return this.httpService.post('/single-list-variation',variationSearch)
      .pipe(
        map((response: AxiosResponse<Variation[]>) => response.data),
      );
  };
}

export default new UnitMasterRepository();