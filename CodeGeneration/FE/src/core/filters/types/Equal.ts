import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class Equal implements IFilterType {
  public code: string = 'equal';

  public sign: string = '=';

  public display: string = translate('filters.types.equal');
}

export default new Equal();
