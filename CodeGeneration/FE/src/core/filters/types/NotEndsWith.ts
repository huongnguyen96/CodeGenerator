import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class NotEndsWith implements IFilterType {
  public code: string = 'notEndsWith';

  public sign: string = 'NE';

  public display: string = translate('filters.types.notEndsWith');
}

export default new NotEndsWith();
