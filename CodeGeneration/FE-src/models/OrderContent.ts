import {Model} from 'core';

import {Order} from 'models/Order';

export class OrderContent extends Model {
   
  public id?: number;
 
  public orderId?: number;
 
  public itemName?: string;
 
  public firstVersion?: string;
 
  public secondVersion?: string;
 
  public thirdVersion?: string;
 
  public price?: number;
 
  public discountPrice?: number;

  public order?: Order;

  public constructor(orderContent?: OrderContent) {
    super(orderContent);
  }
}
