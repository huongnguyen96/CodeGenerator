
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';

import {DiscountCustomerGrouping} from 'models/DiscountCustomerGrouping';
import {DiscountCustomerGroupingSearch} from 'models/DiscountCustomerGroupingSearch';
import {DiscountItem} from 'models/DiscountItem';
import {DiscountItemSearch} from 'models/DiscountItemSearch';

export class DiscountDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount/discount-detail');
  }

  public get = (id: number): Observable<Discount> => {
    return this.httpService.post<Discount>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Discount>) => response.data),
      );
  };
  
  public create = (discount: Discount): Observable<Discount> => {
    return this.httpService.post<Discount>(`/create`, discount)
      .pipe(
        map((response: AxiosResponse<Discount>) => response.data),
      );
  };
  public update = (discount: Discount): Observable<Discount> => {
    return this.httpService.post<Discount>(`/update`, discount)
      .pipe(
        map((response: AxiosResponse<Discount>) => response.data),
      );
  };
  public delete = (discount: Discount): Observable<Discount> => {
    return this.httpService.post<Discount>(`/delete`, discount)
      .pipe(
        map((response: AxiosResponse<Discount>) => response.data),
      );
  };
  
  public save = (discount: Discount): Observable<Discount> => {
    return discount.id ? this.update(discount) : this.create(discount);
  };
  
  public singleListDiscountCustomerGrouping = (discountCustomerGroupingSearch: DiscountCustomerGroupingSearch): Observable<DiscountCustomerGrouping[]> => {
    return this.httpService.post('/single-list-discount-customer-grouping',discountCustomerGroupingSearch)
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping[]>) => response.data),
      );
  };
  public singleListDiscountItem = (discountItemSearch: DiscountItemSearch): Observable<DiscountItem[]> => {
    return this.httpService.post('/single-list-discount-item',discountItemSearch)
      .pipe(
        map((response: AxiosResponse<DiscountItem[]>) => response.data),
      );
  };
}

export default new DiscountDetailRepository();