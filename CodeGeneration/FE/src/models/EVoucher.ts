import {Model} from 'core';

import {Customer} from 'models/Customer';
import {Product} from 'models/Product';
import {EVoucherContent} from 'models/EVoucherContent';

export class EVoucher extends Model {
   
  public id?: number;
 
  public customerId?: number;
 
  public productId?: number;
 
  public name?: string;
 
  public start?: string | Date;
 
  public end?: string | Date;
 
  public quantity?: number;

  public customer?: Customer;

  public product?: Product;
  
  public eVoucherContents?: EVoucherContent[];

  public constructor(eVoucher?: EVoucher) {
    super(eVoucher);
  }
}
