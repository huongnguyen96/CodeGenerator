import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class Contains implements IFilterType {
  public code: string = 'notContains';

  public sign: string = 'NC';

  public display: string = translate('filters.types.notContains');
}

export default new Contains();
