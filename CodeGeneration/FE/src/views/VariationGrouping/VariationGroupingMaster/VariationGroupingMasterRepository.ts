
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

export class VariationGroupingMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/variation-grouping/variation-grouping-master');
  }

  public count = (variationGroupingSearch: VariationGroupingSearch): Observable<number> => {
    return this.httpService.post('/count', variationGroupingSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (variationGroupingSearch: VariationGroupingSearch): Observable<VariationGrouping[]> => {
    return this.httpService.post('/list', variationGroupingSearch)
      .pipe(
        map((response: AxiosResponse<VariationGrouping[]>) => response.data),
      );
  };

  public get = (id: number): Observable<VariationGrouping> => {
    return this.httpService.post<VariationGrouping>('/get', { id })
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

  public singleListProduct = (productSearch: ProductSearch): Observable<Product[]> => {
    return this.httpService.post('/single-list-product', productSearch)
      .pipe(
        map((response: AxiosResponse<Product[]>) => response.data),
      );
  };
}

export default new VariationGroupingMasterRepository();
