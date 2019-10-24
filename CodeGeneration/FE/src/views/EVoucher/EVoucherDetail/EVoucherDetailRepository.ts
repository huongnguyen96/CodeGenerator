
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

export class EVoucherDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/e-voucher/e-voucher-detail');
  }

  public get = (id: number): Observable<EVoucher> => {
    return this.httpService.post<EVoucher>('/get', { id })
      .pipe(
        map((response: AxiosResponse<EVoucher>) => response.data),
      );
  };
  
  public create = (eVoucher: EVoucher): Observable<EVoucher> => {
    return this.httpService.post<EVoucher>(`/create`, eVoucher)
      .pipe(
        map((response: AxiosResponse<EVoucher>) => response.data),
      );
  };
  public update = (eVoucher: EVoucher): Observable<EVoucher> => {
    return this.httpService.post<EVoucher>(`/update`, eVoucher)
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
  
  public save = (eVoucher: EVoucher): Observable<EVoucher> => {
    return eVoucher.id ? this.update(eVoucher) : this.create(eVoucher);
  };
  
  public singleListCustomer = (customerSearch: CustomerSearch): Observable<Customer[]> => {
    return this.httpService.post('/single-list-customer',customerSearch)
      .pipe(
        map((response: AxiosResponse<Customer[]>) => response.data),
      );
  };
  public singleListProduct = (productSearch: ProductSearch): Observable<Product[]> => {
    return this.httpService.post('/single-list-product',productSearch)
      .pipe(
        map((response: AxiosResponse<Product[]>) => response.data),
      );
  };
}

export default new EVoucherDetailRepository();