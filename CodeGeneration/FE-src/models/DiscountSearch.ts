
import {Search} from 'core/entities/Search';

export class DiscountSearch extends Search {
  
  public id?: number;

  public name?: string;

  public start?: string | Date;

  public end?: string | Date;

  public type?: string;
;
}
