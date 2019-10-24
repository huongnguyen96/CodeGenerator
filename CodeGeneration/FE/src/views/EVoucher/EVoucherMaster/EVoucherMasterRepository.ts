
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

export class EVoucherMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/e-voucher/e-voucher-master');
  }

  public count = (eVoucherSearch: EVoucherSearch): Observable<number> => {
    return this.httpService.post('/count', eVoucherSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (eVoucherSearch: EVoucherSearch): Observable<EVoucher[]> => {
    return this.httpService.post('/list', eVoucherSearch)
      .pipe(
        map((response: AxiosResponse<EVoucher[]>) => response.data),
      );
  };

  public get = (id: number): Observable<EVoucher> => {
    return this.httpService.post<EVoucher>('/get', { id })
      .pipe(
        map((response: AxiosResponse<EVoucher>) => response.data),
      );
  };

  public delete = (eVoucher: EVoucher): Observable<EVoucher> => {
    return this.httpService.post<EVoucher>(`/delete`, eVoucher)
      .pipe(
        map((response: AxiosResponse<EVoucher>) => response.data),
      );
  };

  public singleListCustomer = (customerSearch: CustomerSearch): Observable<Customer[]> => {
    return this.httpService.post('/single-list-customer', customerSearch)
      .pipe(
        map((response: AxiosResponse<Customer[]>) => response.data),
      );
  };
  public singleListProduct = (productSearch: ProductSearch): Observable<Product[]> => {
    return this.httpService.post('/single-list-product', productSearch)
      .pipe(
        map((response: AxiosResponse<Product[]>) => response.data),
      );
  };
}

export default new EVoucherMasterRepository();
