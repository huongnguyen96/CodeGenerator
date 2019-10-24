
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';
import {ProductStatus} from 'models/ProductStatus';
import {ProductStatusSearch} from 'models/ProductStatusSearch';
import {ProductType} from 'models/ProductType';
import {ProductTypeSearch} from 'models/ProductTypeSearch';

export class ProductMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/product/product-master');
  }

  public count = (productSearch: ProductSearch): Observable<number> => {
    return this.httpService.post('/count', productSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (productSearch: ProductSearch): Observable<Product[]> => {
    return this.httpService.post('/list', productSearch)
      .pipe(
        map((response: AxiosResponse<Product[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Product> => {
    return this.httpService.post<Product>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Product>) => response.data),
      );
  };

  public delete = (product: Product): Observable<Product> => {
    return this.httpService.post<Product>(`/delete`, product)
      .pipe(
        map((response: AxiosResponse<Product>) => response.data),
      );
  };

  public singleListBrand = (brandSearch: BrandSearch): Observable<Brand[]> => {
    return this.httpService.post('/single-list-brand', brandSearch)
      .pipe(
        map((response: AxiosResponse<Brand[]>) => response.data),
      );
  };
  public singleListCategory = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/single-list-category', categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };
  public singleListMerchant = (merchantSearch: MerchantSearch): Observable<Merchant[]> => {
    return this.httpService.post('/single-list-merchant', merchantSearch)
      .pipe(
        map((response: AxiosResponse<Merchant[]>) => response.data),
      );
  };
  public singleListProductStatus = (productStatusSearch: ProductStatusSearch): Observable<ProductStatus[]> => {
    return this.httpService.post('/single-list-product-status', productStatusSearch)
      .pipe(
        map((response: AxiosResponse<ProductStatus[]>) => response.data),
      );
  };
  public singleListProductType = (productTypeSearch: ProductTypeSearch): Observable<ProductType[]> => {
    return this.httpService.post('/single-list-product-type', productTypeSearch)
      .pipe(
        map((response: AxiosResponse<ProductType[]>) => response.data),
      );
  };
}

export default new ProductMasterRepository();
