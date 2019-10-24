
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ProductType} from 'models/ProductType';
import {ProductTypeSearch} from 'models/ProductTypeSearch';


export class ProductTypeDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/product-type/product-type-detail');
  }

  public get = (id: number): Observable<ProductType> => {
    return this.httpService.post<ProductType>('/get', { id })
      .pipe(
        map((response: AxiosResponse<ProductType>) => response.data),
      );
  };
  
  public create = (productType: ProductType): Observable<ProductType> => {
    return this.httpService.post<ProductType>(`/create`, productType)
      .pipe(
        map((response: AxiosResponse<ProductType>) => response.data),
      );
  };
  public update = (productType: ProductType): Observable<ProductType> => {
    return this.httpService.post<ProductType>(`/update`, productType)
      .pipe(
        map((response: AxiosResponse<ProductType>) => response.data),
      );
  };
  public delete = (productType: ProductType): Observable<ProductType> => {
    return this.httpService.post<ProductType>(`/delete`, productType)
      .pipe(
        map((response: AxiosResponse<ProductType>) => response.data),
      );
  };
  
  public save = (productType: ProductType): Observable<ProductType> => {
    return productType.id ? this.update(productType) : this.create(productType);
  };
  
}

export default new ProductTypeDetailRepository();