
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {DiscountContent} from 'models/DiscountContent';
import {DiscountContentSearch} from 'models/DiscountContentSearch';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

export class DiscountContentDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount-content/discount-content-detail');
  }

  public get = (id: number): Observable<DiscountContent> => {
    return this.httpService.post<DiscountContent>('/get', { id })
      .pipe(
        map((response: AxiosResponse<DiscountContent>) => response.data),
      );
  };
  
  public create = (discountContent: DiscountContent): Observable<DiscountContent> => {
    return this.httpService.post<DiscountContent>(`/create`, discountContent)
      .pipe(
        map((response: AxiosResponse<DiscountContent>) => response.data),
      );
  };
  public update = (discountContent: DiscountContent): Observable<DiscountContent> => {
    return this.httpService.post<DiscountContent>(`/update`, discountContent)
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
  
  public save = (discountContent: DiscountContent): Observable<DiscountContent> => {
    return discountContent.id ? this.update(discountContent) : this.create(discountContent);
  };
  
  public singleListDiscount = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/single-list-discount',discountSearch)
      .pipe(
        map((response: AxiosResponse<Discount[]>) => response.data),
      );
  };
  public singleListItem = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/single-list-item',itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };
}

export default new DiscountContentDetailRepository();