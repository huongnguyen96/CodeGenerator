import {Search} from 'core/entities/Search';

export class DistrictSearch extends Search {
  public id: string;

  public code: string;

  public name: string;

  public provinceId?: string;
}
