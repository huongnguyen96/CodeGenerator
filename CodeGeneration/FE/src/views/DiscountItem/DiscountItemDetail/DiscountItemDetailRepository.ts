
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {DiscountItem} from 'models/DiscountItem';
import {DiscountItemSearch} from 'models/DiscountItemSearch';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

export class DiscountItemDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount-item/discount-item-detail');
  }

  public get = (id: number): Observable<DiscountItem> => {
    return this.httpService.post<DiscountItem>('/get', { id })
      .pipe(
        map((response: AxiosResponse<DiscountItem>) => response.data),
      );
  };
  
  public create = (discountItem: DiscountItem): Observable<DiscountItem> => {
    return this.httpService.post<DiscountItem>(`/create`, discountItem)
      .pipe(
        map((response: AxiosResponse<DiscountItem>) => response.data),
      );
  };
  public update = (discountItem: DiscountItem): Observable<DiscountItem> => {
    return this.httpService.post<DiscountItem>(`/update`, discountItem)
      .pipe(
        map((response: AxiosResponse<DiscountItem>) => response.data),
      );
  };
  public delete = (discountItem: DiscountItem): Observable<DiscountItem> => {
    return this.httpService.post<DiscountItem>(`/delete`, discountItem)
      .pipe(
        map((response: AxiosResponse<DiscountItem>) => response.data),
      );
  };
  
  public save = (discountItem: DiscountItem): Observable<DiscountItem> => {
    return discountItem.id ? this.update(discountItem) : this.create(discountItem);
  };
  
  public singleListDiscount = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/single-list-discount',discountSearch)
      .pipe(
        map((response: AxiosResponse<Discount[]>) => response.data),
      );
  };
  public singleListUnit = (unitSearch: UnitSearch): Observable<Unit[]> => {
    return this.httpService.post('/single-list-unit',unitSearch)
      .pipe(
        map((response: AxiosResponse<Unit[]>) => response.data),
      );
  };
}

export default new DiscountItemDetailRepository();