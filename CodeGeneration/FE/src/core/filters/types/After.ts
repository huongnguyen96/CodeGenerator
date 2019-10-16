import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class After implements IFilterType {
  public code: string = 'after';

  public sign: string = '>';

  public display: string = translate('filters.types.after');
}

export default new After();
