
import {Search} from 'core/entities/Search';

export class EVoucherContentSearch extends Search {
  
  public id?: number;

  public eVourcherId?: number;

  public usedCode?: string;

  public merchantCode?: string;

  public usedDate?: string | Date;
;
}
