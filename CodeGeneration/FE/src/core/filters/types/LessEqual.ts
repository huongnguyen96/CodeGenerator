import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class LessEqual implements IFilterType {
  public code: string = 'lessEqual';

  public sign: string = '<=';

  public display: string = translate('filters.types.lessEqual');
}

export default new LessEqual();
