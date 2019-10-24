
import {Search} from 'core/entities/Search';

export class OrderSearch extends Search {
  
  public id?: number;

  public customerId?: number;

  public createdDate?: string | Date;

  public voucherCode?: string;

  public total?: number;

  public voucherDiscount?: number;

  public campaignDiscount?: number;

  public statusId?: number;
;
}
