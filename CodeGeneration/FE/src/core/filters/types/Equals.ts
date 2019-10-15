import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class Equals implements IFilterType {
  public code: string = 'equals';

  public sign: string = '=';

  public display: string = translate('filters.types.equals');
}

export default new Equals();
