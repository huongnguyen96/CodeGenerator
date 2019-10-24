import {Model} from 'core';

import {DiscountCustomerGrouping} from 'models/DiscountCustomerGrouping';
import {DiscountItem} from 'models/DiscountItem';

export class Discount extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public start?: string | Date;
 
  public end?: string | Date;
 
  public type?: string;
  
  public discountCustomerGroupings?: DiscountCustomerGrouping[];
  
  public discountItems?: DiscountItem[];

  public constructor(discount?: Discount) {
    super(discount);
  }
}
