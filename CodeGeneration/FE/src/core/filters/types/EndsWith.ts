import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class EndsWith implements IFilterType {
  public code: string = 'endsWith';

  public sign: string = 'EW';

  public display: string = translate('filters.types.endsWith');
}

export default new EndsWith();
