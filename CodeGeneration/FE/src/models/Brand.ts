import {Model} from 'core';

import {Category} from 'models/Category';
import {Product} from 'models/Product';

export class Brand extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public categoryId?: number;

  public category?: Category;
  
  public products?: Product[];

  public constructor(brand?: Brand) {
    super(brand);
  }
}
