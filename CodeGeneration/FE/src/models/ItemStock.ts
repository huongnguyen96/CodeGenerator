import {Model} from 'core';

import {Item} from 'models/Item';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {Warehouse} from 'models/Warehouse';

export class ItemStock extends Model {
   
  public id?: number;
 
  public itemId?: number;
 
  public warehouseId?: number;
 
  public unitOfMeasureId?: number;
 
  public quantity?: number;

  public item?: Item;

  public unitOfMeasure?: ItemUnitOfMeasure;

  public warehouse?: Warehouse;

  public constructor(itemStock?: ItemStock) {
    super(itemStock);
  }
}
