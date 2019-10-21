
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

export class VariationDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/variation/variation-detail');
  }

  public get = (id: number): Observable<Variation> => {
    return this.httpService.post<Variation>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Variation>) => response.data),
      );
  };
  
  public create = (variation: Variation): Observable<Variation> => {
    return this.httpService.post<Variation>(`/create`, variation)
      .pipe(
        map((response: AxiosResponse<Variation>) => response.data),
      );
  };
  public update = (variation: Variation): Observable<Variation> => {
    return this.httpService.post<Variation>(`/update`, variation)
      .pipe(
        map((response: AxiosResponse<Variation>) => response.data),
      );
  };
  public delete = (variation: Variation): Observable<Variation> => {
    return this.httpService.post<Variation>(`/delete`, variation)
      .pipe(
        map((response: AxiosResponse<Variation>) => response.data),
      );
  };
  
  public save = (variation: Variation): Observable<Variation> => {
    return variation.id ? this.update(variation) : this.create(variation);
  };
  
  public singleListVariationGrouping = (variationGroupingSearch: VariationGroupingSearch): Observable<VariationGrouping[]> => {
    return this.httpService.post('/single-list-variation-grouping',variationGroupingSearch)
      .pipe(
        map((response: AxiosResponse<VariationGrouping[]>) => response.data),
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

export default new VariationDetailRepository();