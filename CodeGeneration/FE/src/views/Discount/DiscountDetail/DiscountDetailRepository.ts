
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

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

}

export default new DiscountDetailRepository();
