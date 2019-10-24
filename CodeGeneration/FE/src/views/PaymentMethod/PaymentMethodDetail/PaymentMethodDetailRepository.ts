
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {PaymentMethod} from 'models/PaymentMethod';
import {PaymentMethodSearch} from 'models/PaymentMethodSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class PaymentMethodDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/payment-method/payment-method-detail');
  }

  public get = (id: number): Observable<PaymentMethod> => {
    return this.httpService.post<PaymentMethod>('/get', { id })
      .pipe(
        map((response: AxiosResponse<PaymentMethod>) => response.data),
      );
  };

  public create = (paymentMethod: PaymentMethod): Observable<PaymentMethod> => {
    return this.httpService.post<PaymentMethod>(`/create`, paymentMethod)
      .pipe(
        map((response: AxiosResponse<PaymentMethod>) => response.data),
      );
  };
  public update = (paymentMethod: PaymentMethod): Observable<PaymentMethod> => {
    return this.httpService.post<PaymentMethod>(`/update`, paymentMethod)
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

  public save = (paymentMethod: PaymentMethod): Observable<PaymentMethod> => {
    return paymentMethod.id ? this.update(paymentMethod) : this.create(paymentMethod);
  };

}

export default new PaymentMethodDetailRepository();
