import {Model} from 'core';

import {Unit} from 'models/Unit';
import {Warehouse} from 'models/Warehouse';

export class Stock extends Model {
   
  public id?: number;
 
  public unitId?: number;
 
  public warehouseId?: number;
 
  public quantity?: number;

  public unit?: Unit;

  public warehouse?: Warehouse;

  public constructor(stock?: Stock) {
    super(stock);
  }
}
