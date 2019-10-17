import {Model} from 'core';

import {ItemStock} from 'models/ItemStock';
import {Item} from 'models/Item';

export class ItemUnitOfMeasure extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;
  
  public itemStocks?: ItemStock[];
  
  public items?: Item[];

  public constructor(itemUnitOfMeasure?: ItemUnitOfMeasure) {
    super(itemUnitOfMeasure);
  }
}
