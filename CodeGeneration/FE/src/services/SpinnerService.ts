import {IGlobalData} from 'config/global';
import {setGlobal} from 'reactn';

class SpinnerService {

  public show(): Promise<IGlobalData> {
    return setGlobal<IGlobalData>({
      spinning: true,
    });
  }

  public hide(): Promise<IGlobalData> {
    return setGlobal<IGlobalData>({
      spinning: false,
    });
  }
}

export const spinnerService: SpinnerService = new SpinnerService();
