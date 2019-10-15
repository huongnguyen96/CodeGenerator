import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class GreaterThanOrEqualTo implements IFilterType {
  public code: string = 'greaterThanOrEqualTo';

  public sign: string = '>=';

  public display: string = translate('filters.types.greaterThanOrEqualTo');
}

export default new GreaterThanOrEqualTo();
