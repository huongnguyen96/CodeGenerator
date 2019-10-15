import {Model} from 'core';


export class Item extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;

  public constructor(item?: Item) {
    super(item);
  }
}
