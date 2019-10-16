import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class NotStartsWith implements IFilterType {
  public code: string = 'notStartsWith';

  public sign: string = 'NS';

  public display: string = translate('filters.types.notStartsWith');
}

export default new NotStartsWith();
