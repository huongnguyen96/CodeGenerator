
import {Search} from 'core/entities/Search';

export class EVoucherSearch extends Search {
  
  public id?: number;

  public customerId?: number;

  public productId?: number;

  public name?: string;

  public start?: string | Date;

  public end?: string | Date;

  public quantity?: number;
;
}
