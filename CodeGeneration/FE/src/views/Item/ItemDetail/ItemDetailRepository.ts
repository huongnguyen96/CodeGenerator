
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

export class ItemDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item/item-detail');
  }

  public get = (id: number): Observable<Item> => {
    return this.httpService.post<Item>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  
  public create = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/create`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  public update = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/update`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  public delete = (item: Item): Observable<Item> => {
    return this.httpService.post<Item>(`/delete`, item)
      .pipe(
        map((response: AxiosResponse<Item>) => response.data),
      );
  };
  
  public save = (item: Item): Observable<Item> => {
    return item.id ? this.update(item) : this.create(item);
  };
  
  public singleListVariation = (variationSearch: VariationSearch): Observable<Variation[]> => {
    return this.httpService.post('/single-list-variation',variationSearch)
      .pipe(
        map((response: AxiosResponse<Variation[]>) => response.data),
      );
  };
  public singleListProduct = (productSearch: ProductSearch): Observable<Product[]> => {
    return this.httpService.post('/single-list-product',productSearch)
      .pipe(
        map((response: AxiosResponse<Product[]>) => response.data),
      );
  };
}

export default new ItemDetailRepository();