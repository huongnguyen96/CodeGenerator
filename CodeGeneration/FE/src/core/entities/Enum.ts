import {Model} from './Model';

export class Enum extends Model {
  public code?: string;

  public name?: string;

  public display?: string;

  public constructor() {
    super();
  }
}
