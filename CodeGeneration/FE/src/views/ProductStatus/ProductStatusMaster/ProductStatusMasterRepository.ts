
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {ProductStatus} from 'models/ProductStatus';
import {ProductStatusSearch} from 'models/ProductStatusSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class ProductStatusMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/product-status/product-status-master');
  }

  public count = (productStatusSearch: ProductStatusSearch): Observable<number> => {
    return this.httpService.post('/count', productStatusSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (productStatusSearch: ProductStatusSearch): Observable<ProductStatus[]> => {
    return this.httpService.post('/list', productStatusSearch)
      .pipe(
        map((response: AxiosResponse<ProductStatus[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ProductStatus> => {
    return this.httpService.post<ProductStatus>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ProductStatus>) => response.data),
      );
  };

  public delete = (productStatus: ProductStatus): Observable<ProductStatus> => {
    return this.httpService.post<ProductStatus>(`/delete`, productStatus)
      .pipe(
        map((response: AxiosResponse<ProductStatus>) => response.data),
      );
  };

}

export default new ProductStatusMasterRepository();
