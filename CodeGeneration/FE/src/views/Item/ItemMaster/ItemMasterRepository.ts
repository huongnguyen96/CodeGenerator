
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';
import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

export class ItemMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/item/item-master');
  }

  public count = (itemSearch: ItemSearch): Observable<number> => {
    return this.httpService.post('/count', itemSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (itemSearch: ItemSearch): Observable<Item[]> => {
    return this.httpService.post('/list', itemSearch)
      .pipe(
        map((response: AxiosResponse<Item[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Item> => {
    return this.httpService.post<Item>('/get', { id })
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

  public singleListVariation = (variationSearch: VariationSearch): Observable<Variation[]> => {
    return this.httpService.post('/single-list-variation', variationSearch)
      .pipe(
        map((response: AxiosResponse<Variation[]>) => response.data),
      );
  };
  public singleListProduct = (productSearch: ProductSearch): Observable<Product[]> => {
    return this.httpService.post('/single-list-product', productSearch)
      .pipe(
        map((response: AxiosResponse<Product[]>) => response.data),
      );
  };
}

export default new ItemMasterRepository();
