import {Model} from 'core';

import {District} from 'models/District';
import {ShippingAddress} from 'models/ShippingAddress';

export class Province extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public orderNumber?: number;
  
  public districts?: District[];
  
  public shippingAddresses?: ShippingAddress[];

  public constructor(province?: Province) {
    super(province);
  }
}
