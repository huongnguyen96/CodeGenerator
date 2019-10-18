import {Model} from 'core';


export class User extends Model {
   
  public id?: number;
 
  public username?: string;
 
  public password?: string;

  public constructor(user?: User) {
    super(user);
  }
}
