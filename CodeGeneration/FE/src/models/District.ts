import {Model} from 'core';

import {Province} from 'models/Province';
import {ShippingAddress} from 'models/ShippingAddress';
import {Ward} from 'models/Ward';

export class District extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public orderNumber?: number;
 
  public provinceId?: number;

  public province?: Province;
  
  public shippingAddresses?: ShippingAddress[];
  
  public wards?: Ward[];

  public constructor(district?: District) {
    super(district);
  }
}
