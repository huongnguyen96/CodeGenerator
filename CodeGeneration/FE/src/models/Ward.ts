import {Model} from 'core/entities/Model';
import {District} from 'models/District';

export class Ward extends Model {
  public id: string;

  public code: string;

  public name: string;

  public district?: District;

  public constructor(ward?: Ward) {
    super(ward);
  }
}
