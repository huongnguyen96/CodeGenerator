import {Model} from 'core';

import {User} from 'models/User';

export class Warehouse extends Model {
   
  public id?: number;
 
  public managerId?: number;
 
  public code?: string;
 
  public name?: string;

  public manager?: User;

  public constructor(warehouse?: Warehouse) {
    super(warehouse);
  }
}
