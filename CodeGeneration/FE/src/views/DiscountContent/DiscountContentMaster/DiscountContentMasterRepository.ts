
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {DiscountContent} from 'models/DiscountContent';
import {DiscountContentSearch} from 'models/DiscountContentSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class DiscountContentMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount-content/discount-content-master');
  }

  public count = (discountContentSearch: DiscountContentSearch): Observable<number> => {
    return this.httpService.post('/count', discountContentSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (discountContentSearch: DiscountContentSearch): Observable<DiscountContent[]> => {
    return this.httpService.post('/list', discountContentSearch)
      .pipe(
        map((response: AxiosResponse<DiscountContent[]>) => response.data),
      );
  };

  public get = (id: number): Observable<DiscountContent> => {
    return this.httpService.post<DiscountContent>('/get', { id })
      .pipe(
        map((response: AxiosResponse<DiscountContent>) => response.data),
      );
  };

  public delete = (discountContent: DiscountContent): Observable<DiscountContent> => {
    return this.httpService.post<DiscountContent>(`/delete`, discountContent)
      .pipe(
        map((response: AxiosResponse<DiscountContent>) => response.data),
      );
  };

  public singleListDiscount = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/single-list-discount', discountSearch)
      .pipe(
        map((response: AxiosResponse<Discount[]>) => response.data),
      );
  };
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item', itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new DiscountContentMasterRepository();
