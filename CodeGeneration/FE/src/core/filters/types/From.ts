import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class From implements IFilterType {
  public code: string = 'from';

  public sign: string = '>=';

  public display: string = translate('filters.types.from');
}

export default new From();
