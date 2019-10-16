import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class GreaterEqual implements IFilterType {
  public code: string = 'greaterEqual';

  public sign: string = '>=';

  public display: string = translate('filters.types.greaterEqual');
}

export default new GreaterEqual();
