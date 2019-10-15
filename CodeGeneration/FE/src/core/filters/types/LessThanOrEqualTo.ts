import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class LessThanOrEqualTo implements IFilterType {
  public code: string = 'lessThanOrEqualTo';

  public sign: string = '<=';

  public display: string = translate('filters.types.lessThanOrEqualTo');
}

export default new LessThanOrEqualTo();
