import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class To implements IFilterType {
  public code: string = 'to';

  public sign: string = '<=';

  public display: string = translate('filters.types.to');
}

export default new To();
