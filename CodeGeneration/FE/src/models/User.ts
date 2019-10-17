import {Model} from 'core/entities/Model';

export class User extends Model {
  public username?: string;

  public password?: string;

  public email?: string;
}
