
import {Search} from 'core/entities/Search';

export class ItemSearch extends Search {
  
  public id?: number;

  public productId?: number;

  public firstVariationId?: number;

  public secondVariationId?: number;

  public sKU?: string;

  public price?: number;

  public minPrice?: number;
;
}
