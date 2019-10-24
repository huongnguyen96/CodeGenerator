import {Model} from 'core';

import {Category} from 'models/Category';
import {Item} from 'models/Item';

export class Brand extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public categoryId?: number;

  public category?: Category;
  
  public items?: Item[];

  public constructor(brand?: Brand) {
    super(brand);
  }
}
