
import {Search} from 'core/entities/Search';

import {NumberFilter} from 'filters/NumberFilter';

import {TextFilter} from 'filters/TextFilter';


export class ItemSearch extends Search {
  
  public id: NumberFilter = new NumberFilter();

  public code: TextFilter = new TextFilter();

  public name: TextFilter = new TextFilter();
;
}
