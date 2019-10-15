import {Model} from 'core';

import {Warehouse} from 'models/Warehouse';

export class User extends Model {
   
  public id?: number;
 
  public username?: string;
 
  public password?: string;
  
  public warehouses?: Warehouse[];

  public constructor(user?: User) {
    super(user);
  }
}
