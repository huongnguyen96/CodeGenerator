import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class Before implements IFilterType {
  public code: string = 'before';

  public sign: string = '<';

  public display: string = translate('filters.types.before');
}

export default new Before();
