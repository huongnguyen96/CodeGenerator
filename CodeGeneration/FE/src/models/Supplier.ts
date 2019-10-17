import {Model} from 'core';

import {Item} from 'models/Item';
import {Warehouse} from 'models/Warehouse';

export class Supplier extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public phone?: string;
 
  public contactPerson?: string;
 
  public address?: string;
  
  public items?: Item[];
  
  public warehouses?: Warehouse[];

  public constructor(supplier?: Supplier) {
    super(supplier);
  }
}
