import {Search} from 'core/entities/Search';
import {TextFilter} from 'filters/TextFilter';

export class UserSearch extends Search {
  public username: TextFilter = new TextFilter();

  public email: TextFilter = new TextFilter();
}
