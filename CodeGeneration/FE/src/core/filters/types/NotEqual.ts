import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class NotEqual implements IFilterType {
  public code: string = 'notEqual';

  public sign: string = '!=';

  public display: string = translate('filters.types.notEqual');
}

export default new NotEqual();
