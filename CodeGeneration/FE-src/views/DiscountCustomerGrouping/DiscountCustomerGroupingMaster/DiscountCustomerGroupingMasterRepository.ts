
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {DiscountCustomerGrouping} from 'models/DiscountCustomerGrouping';
import {DiscountCustomerGroupingSearch} from 'models/DiscountCustomerGroupingSearch';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';

export class DiscountCustomerGroupingMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount-customer-grouping/discount-customer-grouping-master');
  }

  public count = (discountCustomerGroupingSearch: DiscountCustomerGroupingSearch): Observable<number> => {
    return this.httpService.post('/count',discountCustomerGroupingSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (discountCustomerGroupingSearch: DiscountCustomerGroupingSearch): Observable<DiscountCustomerGrouping[]> => {
    return this.httpService.post('/list',discountCustomerGroupingSearch)
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping[]>) => response.data),
      );
  };

  public get = (id: number): Observable<DiscountCustomerGrouping> => {
    return this.httpService.post<DiscountCustomerGrouping>('/get', { id })
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping>) => response.data),
      );
  };
    
  public delete = (discountCustomerGrouping: DiscountCustomerGrouping): Observable<DiscountCustomerGrouping> => {
    return this.httpService.post<DiscountCustomerGrouping>(`/delete`, discountCustomerGrouping)
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping>) => response.data),
      );
  };
  
  public singleListDiscount = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/single-list-discount',discountSearch)
      .pipe(
        map((response: AxiosResponse<Discount[]>) => response.data),
      );
  };
}

export default new DiscountCustomerGroupingMasterRepository();