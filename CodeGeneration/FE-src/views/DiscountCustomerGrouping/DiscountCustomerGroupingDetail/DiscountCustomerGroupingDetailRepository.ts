
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {DiscountCustomerGrouping} from 'models/DiscountCustomerGrouping';
import {DiscountCustomerGroupingSearch} from 'models/DiscountCustomerGroupingSearch';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';

export class DiscountCustomerGroupingDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount-customer-grouping/discount-customer-grouping-detail');
  }

  public get = (id: number): Observable<DiscountCustomerGrouping> => {
    return this.httpService.post<DiscountCustomerGrouping>('/get', { id })
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping>) => response.data),
      );
  };
  
  public create = (discountCustomerGrouping: DiscountCustomerGrouping): Observable<DiscountCustomerGrouping> => {
    return this.httpService.post<DiscountCustomerGrouping>(`/create`, discountCustomerGrouping)
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping>) => response.data),
      );
  };
  public update = (discountCustomerGrouping: DiscountCustomerGrouping): Observable<DiscountCustomerGrouping> => {
    return this.httpService.post<DiscountCustomerGrouping>(`/update`, discountCustomerGrouping)
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
  
  public save = (discountCustomerGrouping: DiscountCustomerGrouping): Observable<DiscountCustomerGrouping> => {
    return discountCustomerGrouping.id ? this.update(discountCustomerGrouping) : this.create(discountCustomerGrouping);
  };
  
  public singleListDiscount = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/single-list-discount',discountSearch)
      .pipe(
        map((response: AxiosResponse<Discount[]>) => response.data),
      );
  };
}

export default new DiscountCustomerGroupingDetailRepository();