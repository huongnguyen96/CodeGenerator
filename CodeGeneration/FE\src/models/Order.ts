import {Model} from 'core';

import {Customer} from 'models/Customer';
import {OrderContent} from 'models/OrderContent';

export class Order extends Model {
   
  public id?: number;
 
  public customerId?: number;
 
  public createdDate?: string | Date;
 
  public voucherCode?: string;
 
  public total?: number;
 
  public voucherDiscount?: number;
 
  public campaignDiscount?: number;

  public customer?: Customer;
  
  public orderContents?: OrderContent[];

  public constructor(order?: Order) {
    super(order);
  }
}
