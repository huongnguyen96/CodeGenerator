import {AxiosResponse} from 'axios';
import {IGlobalData} from 'config/global';
import i18next from 'i18next';
import {setGlobal} from 'reactn';
import {finalize} from 'rxjs/operators';
import {httpService} from './HttpService';
import {spinnerService} from './SpinnerService';

class TranslateService {

  public async useLanguage(lang: string) {
    await spinnerService.show();
    httpService.get(`${window.location.origin}/assets/i18n/${lang}.json`)
      .pipe(
        finalize(() => {
          spinnerService.hide();
        }),
      )
      .subscribe(async (response: AxiosResponse<any>) => {
        await i18next.addResource(lang, '', '', response.data);
        await i18next.changeLanguage(lang);
        await setGlobal<IGlobalData>({
          lang,
        });
      });
  }
}

export const translateService: TranslateService = new TranslateService();
