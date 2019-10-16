import {Model} from 'core/entities/Model';
import {District} from 'models/District';

export class Province extends Model {
  public id: string;

  public code: string;

  public name: string;

  public districts?: District[];

  public constructor(province?: Province) {
    super(province);
  }
}
