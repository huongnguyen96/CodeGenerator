import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class Contains implements IFilterType {
  public code: string = 'contains';

  public sign: string = 'CT';

  public display: string = translate('filters.types.contains');
}

export default new Contains();
