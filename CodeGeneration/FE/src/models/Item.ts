import {Model} from 'core';

import {ItemStatus} from 'models/ItemStatus';
import {Supplier} from 'models/Supplier';
import {ItemType} from 'models/ItemType';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import {ItemStock} from 'models/ItemStock';

export class Item extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;
 
  public sKU?: string;
 
  public typeId?: number;
 
  public purchasePrice?: number;
 
  public salePrice?: number;
 
  public description?: string;
 
  public statusId?: number;
 
  public unitOfMeasureId?: number;
 
  public supplierId?: number;

  public status?: ItemStatus;

  public supplier?: Supplier;

  public type?: ItemType;

  public unitOfMeasure?: ItemUnitOfMeasure;
  
  public itemStocks?: ItemStock[];

  public constructor(item?: Item) {
    super(item);
  }
}
