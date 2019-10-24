
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class DiscountMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/discount/discount-master');
  }

  public count = (discountSearch: DiscountSearch): Observable<number> => {
    return this.httpService.post('/count', discountSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (discountSearch: DiscountSearch): Observable<Discount[]> => {
    return this.httpService.post('/list', discountSearch)
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

}

export default new DiscountMasterRepository();
