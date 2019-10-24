
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {ProductStatus} from 'models/ProductStatus';
import {ProductStatusSearch} from 'models/ProductStatusSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

export class ProductStatusDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/product-status/product-status-detail');
  }

  public get = (id: number): Observable<ProductStatus> => {
    return this.httpService.post<ProductStatus>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ProductStatus>) => response.data),
      );
  };

  public create = (productStatus: ProductStatus): Observable<ProductStatus> => {
    return this.httpService.post<ProductStatus>(`/create`, productStatus)
      .pipe(
        map((response: AxiosResponse<ProductStatus>) => response.data),
      );
  };
  public update = (productStatus: ProductStatus): Observable<ProductStatus> => {
    return this.httpService.post<ProductStatus>(`/update`, productStatus)
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

  public save = (productStatus: ProductStatus): Observable<ProductStatus> => {
    return productStatus.id ? this.update(productStatus) : this.create(productStatus);
  };

}

export default new ProductStatusDetailRepository();
