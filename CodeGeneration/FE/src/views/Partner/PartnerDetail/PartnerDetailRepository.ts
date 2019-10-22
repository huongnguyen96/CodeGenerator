
import {AxiosResponse} from 'axios';
import {Repository} from 'core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';


export class PartnerDetailRepository extends Repository {
  public constructor() {
    super();
    this.httpService.setBasePath('/api/partner/partner-detail');
  }

  public get = (id: number): Observable<Partner> => {
    return this.httpService.post<Partner>('/get', { id })
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  
  public create = (partner: Partner): Observable<Partner> => {
    return this.httpService.post<Partner>(`/create`, partner)
      .pipe(
        map((response: AxiosResponse<Partner>) => response.data),
      );
  };
  public update = (partner: Partner): Observable<Partner> => {
    return this.httpService.post<Partner>(`/update`, partner)
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
  
  public save = (partner: Partner): Observable<Partner> => {
    return partner.id ? this.update(partner) : this.create(partner);
  };
  
}

export default new PartnerDetailRepository();