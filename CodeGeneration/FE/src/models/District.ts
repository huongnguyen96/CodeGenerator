import {Model} from 'core';
import {Province} from 'models/Province';
import {Ward} from './Ward';

export class District extends Model {
  public id: string;

  public code: string;

  public name: string;

  public province?: Province;

  public provinceId: string;

  public wards?: Ward[];

  public constructor(district?: District) {
    super(district);
  }
}
