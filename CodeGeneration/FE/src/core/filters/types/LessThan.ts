import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class LessThan implements IFilterType {
  public code: string = 'lessThan';

  public sign: string = '<';

  public display: string = translate('filters.types.lessThan');
}

export default new LessThan();
