
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class VariationGroupingDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/variation-grouping/variation-grouping-detail');
  }

  public get = (id: number): Observable<VariationGrouping> => {
    return this.httpService.post<VariationGrouping>('/get', { id })
      .pipe(
        map((response: AxiosResponse<VariationGrouping>) => response.data),
      );
  };
  
  public create = (variationGrouping: VariationGrouping): Observable<VariationGrouping> => {
    return this.httpService.post<VariationGrouping>(`/create`, variationGrouping)
      .pipe(
        map((response: AxiosResponse<VariationGrouping>) => response.data),
      );
  };
  public update = (variationGrouping: VariationGrouping): Observable<VariationGrouping> => {
    return this.httpService.post<VariationGrouping>(`/update`, variationGrouping)
      .pipe(
        map((response: AxiosResponse<VariationGrouping>) => response.data),
      );
  };
  public delete = (variationGrouping: VariationGrouping): Observable<VariationGrouping> => {
    return this.httpService.post<VariationGrouping>(`/delete`, variationGrouping)
      .pipe(
        map((response: AxiosResponse<VariationGrouping>) => response.data),
      );
  };
  
  public save = (variationGrouping: VariationGrouping): Observable<VariationGrouping> => {
    return variationGrouping.id ? this.update(variationGrouping) : this.create(variationGrouping);
  };
  
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new VariationGroupingDetailRepository();