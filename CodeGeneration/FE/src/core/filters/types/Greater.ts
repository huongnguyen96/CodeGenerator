import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class Greater implements IFilterType {
  public code: string = 'greater';

  public sign: string = '>';

  public display: string = translate('filters.types.greater');
}

export default new Greater();
