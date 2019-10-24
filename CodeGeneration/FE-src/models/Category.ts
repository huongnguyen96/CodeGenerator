import {Model} from 'core';

import {Brand} from 'models/Brand';
import {Item} from 'models/Item';

export class Category extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;
 
  public parentId?: number;
 
  public icon?: string;

  public parent?: Category;
  
  public brands?: Brand[];
  
  public inverseParent?: Category[];
  
  public items?: Item[];

  public constructor(category?: Category) {
    super(category);
  }
}
