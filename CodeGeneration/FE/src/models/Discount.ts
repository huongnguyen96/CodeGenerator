import {Model} from 'core';

import {DiscountContent} from 'models/DiscountContent';

export class Discount extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public start?: string | Date;
 
  public end?: string | Date;
 
  public type?: string;
  
  public discountContents?: DiscountContent[];

  public constructor(discount?: Discount) {
    super(discount);
  }
}
