
import {Search} from 'core/entities/Search';

export class OrderContentSearch extends Search {
  
  public id?: number;

  public orderId?: number;

  public itemName?: string;

  public firstVersion?: string;

  public secondVersion?: string;

  public thirdVersion?: string;

  public price?: number;

  public discountPrice?: number;
;
}
