import {Model} from 'core';


export class Category extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;

  public constructor(category?: Category) {
    super(category);
  }
}
