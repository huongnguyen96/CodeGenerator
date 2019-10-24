
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

export class DiscountItemMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount-item/discount-item-master');
  }

  public count = (discountItemSearch: DiscountItemSearch): Observable<number> => {
    return this.httpService.post('/count',discountItemSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (discountItemSearch: DiscountItemSearch): Observable<DiscountItem[]> => {
    return this.httpService.post('/list',discountItemSearch)
      .pipe(
        map((response: AxiosResponse<DiscountItem[]>) => response.data),
      );
  };

  public get = (id: number): Observable<DiscountItem> => {
    return this.httpService.post<DiscountItem>('/get', { id })
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

export default new DiscountItemMasterRepository();