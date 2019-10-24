
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ProductType} from 'models/ProductType';
import {ProductTypeSearch} from 'models/ProductTypeSearch';


export class ProductTypeMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/product-type/product-type-master');
  }

  public count = (productTypeSearch: ProductTypeSearch): Observable<number> => {
    return this.httpService.post('/count',productTypeSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (productTypeSearch: ProductTypeSearch): Observable<ProductType[]> => {
    return this.httpService.post('/list',productTypeSearch)
      .pipe(
        map((response: AxiosResponse<ProductType[]>) => response.data),
      );
  };

  public get = (id: number): Observable<ProductType> => {
    return this.httpService.post<ProductType>('/get', { id })
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
  
}

export default new ProductTypeMasterRepository();