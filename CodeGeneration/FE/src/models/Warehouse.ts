import {Model} from 'core';

import {Supplier} from 'models/Supplier';
import {ItemStock} from 'models/ItemStock';

export class Warehouse extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public phone?: string;
 
  public email?: string;
 
  public address?: string;
 
  public supplierId?: number;

  public supplier?: Supplier;
  
  public itemStocks?: ItemStock[];

  public constructor(warehouse?: Warehouse) {
    super(warehouse);
  }
}
