import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class GreaterThan implements IFilterType {
  public code: string = 'greaterThan';

  public sign: string = '>';

  public display: string = translate('filters.types.greaterThan');
}

export default new GreaterThan();
