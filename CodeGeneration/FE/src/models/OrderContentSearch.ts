
import {Search} from 'core/entities/Search';

export class OrderContentSearch extends Search {
  
  public id?: number;

  public orderId?: number;

  public itemId?: number;

  public productName?: string;

  public firstVersion?: string;

  public secondVersion?: string;

  public price?: number;

  public discountPrice?: number;

  public quantity?: number;
;
}
