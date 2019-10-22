
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';

export class VariationMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/variation/variation-master');
  }

  public count = (variationSearch: VariationSearch): Observable<number> => {
    return this.httpService.post('/count',variationSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (variationSearch: VariationSearch): Observable<Variation[]> => {
    return this.httpService.post('/list',variationSearch)
      .pipe(
        map((response: AxiosResponse<Variation[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Variation> => {
    return this.httpService.post<Variation>('/get', { id })
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
  
  public singleListVariationGrouping = (variationGroupingSearch: VariationGroupingSearch): Observable<VariationGrouping[]> => {
    return this.httpService.post('/single-list-variation-grouping',variationGroupingSearch)
      .pipe(
        map((response: AxiosResponse<VariationGrouping[]>) => response.data),
      );
  };
}

export default new VariationMasterRepository();