import {Model} from 'core';


export class PaymentMethod extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;
 
  public description?: string;

  public constructor(paymentMethod?: PaymentMethod) {
    super(paymentMethod);
  }
}
