
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {PaymentMethod} from 'models/PaymentMethod';
import {PaymentMethodSearch} from 'models/PaymentMethodSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class PaymentMethodMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/payment-method/payment-method-master');
  }

  public count = (paymentMethodSearch: PaymentMethodSearch): Observable<number> => {
    return this.httpService.post('/count', paymentMethodSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (paymentMethodSearch: PaymentMethodSearch): Observable<PaymentMethod[]> => {
    return this.httpService.post('/list', paymentMethodSearch)
      .pipe(
        map((response: AxiosResponse<PaymentMethod[]>) => response.data),
      );
  };

  public get = (id: number): Observable<PaymentMethod> => {
    return this.httpService.post<PaymentMethod>('/get', { id })
      .pipe(
        map((response: AxiosResponse<PaymentMethod>) => response.data),
      );
  };

  public delete = (paymentMethod: PaymentMethod): Observable<PaymentMethod> => {
    return this.httpService.post<PaymentMethod>(`/delete`, paymentMethod)
      .pipe(
        map((response: AxiosResponse<PaymentMethod>) => response.data),
      );
  };

}

export default new PaymentMethodMasterRepository();
