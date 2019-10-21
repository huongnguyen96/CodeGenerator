
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';


export class DiscountMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount/discount-master');
  }

  public count = (discountSearch: DiscountSearch): Observable<number> => {
    return this.httpService.post('/count',discountSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/list',discountSearch)
      .pipe(
        map((response: AxiosResponse<Discount[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Discount> => {
    return this.httpService.post<Discount>('/get', { id })
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
  
  public singleList = (discountCustomerGroupingSearch: DiscountCustomerGroupingSearch): Observable<DiscountCustomerGrouping[]> => {
    return this.httpService.post('/single-list-discount-customer-grouping',discountCustomerGroupingSearch)
      .pipe(
        map((response: AxiosResponse<DiscountCustomerGrouping[]>) => response.data),
      );
  };
  public singleList = (discountItemSearch: DiscountItemSearch): Observable<DiscountItem[]> => {
    return this.httpService.post('/single-list-discount-item',discountItemSearch)
      .pipe(
        map((response: AxiosResponse<DiscountItem[]>) => response.data),
      );
  };
}

export default new DiscountMasterRepository();