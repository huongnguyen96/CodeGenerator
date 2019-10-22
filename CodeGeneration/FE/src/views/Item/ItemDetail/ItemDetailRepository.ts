
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';
import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';

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
  
  public singleListBrand = (brandSearch: BrandSearch): Observable<Brand[]> => {
    return this.httpService.post('/single-list-brand',brandSearch)
      .pipe(
        map((response: AxiosResponse<Brand[]>) => response.data),
      );
  };
  public singleListCategory = (categorySearch: CategorySearch): Observable<Category[]> => {
    return this.httpService.post('/single-list-category',categorySearch)
      .pipe(
        map((response: AxiosResponse<Category[]>) => response.data),
      );
  };
  public singleListPartner = (partnerSearch: PartnerSearch): Observable<Partner[]> => {
    return this.httpService.post('/single-list-partner',partnerSearch)
      .pipe(
        map((response: AxiosResponse<Partner[]>) => response.data),
      );
  };
  public singleListItemStatus = (itemStatusSearch: ItemStatusSearch): Observable<ItemStatus[]> => {
    return this.httpService.post('/single-list-item-status',itemStatusSearch)
      .pipe(
        map((response: AxiosResponse<ItemStatus[]>) => response.data),
      );
  };
  public singleListItemType = (itemTypeSearch: ItemTypeSearch): Observable<ItemType[]> => {
    return this.httpService.post('/single-list-item-type',itemTypeSearch)
      .pipe(
        map((response: AxiosResponse<ItemType[]>) => response.data),
      );
  };
}

export default new ItemDetailRepository();