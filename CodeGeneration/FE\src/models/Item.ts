import {Model} from 'core';

import {Brand} from 'models/Brand';
import {Category} from 'models/Category';
import {Partner} from 'models/Partner';
import {ItemStatus} from 'models/ItemStatus';
import {ItemType} from 'models/ItemType';
import {VariationGrouping} from 'models/VariationGrouping';

export class Item extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;
 
  public sKU?: string;
 
  public description?: string;
 
  public typeId?: number;
 
  public statusId?: number;
 
  public partnerId?: number;
 
  public categoryId?: number;
 
  public brandId?: number;

  public brand?: Brand;

  public category?: Category;

  public partner?: Partner;

  public status?: ItemStatus;

  public type?: ItemType;
  
  public variationGroupings?: VariationGrouping[];

  public constructor(item?: Item) {
    super(item);
  }
}
