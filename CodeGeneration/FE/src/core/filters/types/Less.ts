import {IFilterType} from 'core';
import {marker as translate} from 'helpers/translate';

class Less implements IFilterType {
  public code: string = 'less';

  public sign: string = '<';

  public display: string = translate('filters.types.less');
}

export default new Less();
