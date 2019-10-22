
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';


export class PartnerMasterRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/partner/partner-master');
  }

  public count = (partnerSearch: PartnerSearch): Observable<number> => {
    return this.httpService.post('/count',partnerSearch)
      .pipe(
        map((response: AxiosResponse<number>) => response.data),
      );
  };

  public list = (partnerSearch: PartnerSearch): Observable<Partner[]> => {
    return this.httpService.post('/list',partnerSearch)
      .pipe(
        map((response: AxiosResponse<Partner[]>) => response.data),
      );
  };

  public get = (id: number): Observable<Partner> => {
    return this.httpService.post<Partner>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
    
  public delete = (partner: Partner): Observable<Partner> => {
    return this.httpService.post<Partner>(`/delete`, partner)
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  
}

export default new PartnerMasterRepository();