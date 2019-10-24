import {Model} from 'core';

import {Order} from 'models/Order';
import {ShippingAddress} from 'models/ShippingAddress';

export class Customer extends Model {
   
  public id?: number;
 
  public username?: string;
 
  public displayName?: string;
  
  public orders?: Order[];
  
  public shippingAddresses?: ShippingAddress[];

  public constructor(customer?: Customer) {
    super(customer);
  }
}
