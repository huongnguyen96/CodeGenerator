import {Model} from 'core';

import {MerchantAddress} from 'models/MerchantAddress';
import {Product} from 'models/Product';
import {Warehouse} from 'models/Warehouse';

export class Merchant extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public phone?: string;
 
  public contactPerson?: string;
 
  public address?: string;
  
  public merchantAddresses?: MerchantAddress[];
  
  public products?: Product[];
  
  public warehouses?: Warehouse[];

  public constructor(merchant?: Merchant) {
    super(merchant);
  }
}
