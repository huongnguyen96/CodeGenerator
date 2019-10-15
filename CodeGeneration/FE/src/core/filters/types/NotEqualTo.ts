import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class NotEqualTo implements IFilterType {
  public code: string = 'notEqualTo';

  public sign: string = '!=';

  public display: string = translate('filters.types.notEqualTo');
}

export default new NotEqualTo();
