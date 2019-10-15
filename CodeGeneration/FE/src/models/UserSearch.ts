
import {Search} from 'core/entities/Search';

import {NumberFilter} from 'filters/NumberFilter';

import {TextFilter} from 'filters/TextFilter';


export class UserSearch extends Search {
  
  public id: NumberFilter = new NumberFilter();

  public username: TextFilter = new TextFilter();

  public password: TextFilter = new TextFilter();
;
}
