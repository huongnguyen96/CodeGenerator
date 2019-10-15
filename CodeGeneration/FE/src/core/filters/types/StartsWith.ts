import {IFilterType} from 'core/index';
import {marker as translate} from 'helpers/translate';

class StartsWith implements IFilterType {
  public code: string = 'startsWith';

  public sign: string = 'SW';

  public display: string = translate('filters.types.startsWith');
}

export default new StartsWith();
