import {Model} from 'core';


export class CustomerGrouping extends Model {
   
  public id?: number;
 
  public name?: string;

  public constructor(customerGrouping?: CustomerGrouping) {
    super(customerGrouping);
  }
}
