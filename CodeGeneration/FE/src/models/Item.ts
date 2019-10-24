import {Model} from 'core';

import {Variation} from 'models/Variation';
import {Product} from 'models/Product';
import {DiscountContent} from 'models/DiscountContent';
import {OrderContent} from 'models/OrderContent';
import {Stock} from 'models/Stock';

export class Item extends Model {
   
  public id?: number;
 
  public productId?: number;
 
  public firstVariationId?: number;
 
  public secondVariationId?: number;
 
  public sKU?: string;
 
  public price?: number;
 
  public minPrice?: number;

  public firstVariation?: Variation;

  public product?: Product;

  public secondVariation?: Variation;
  
  public discountContents?: DiscountContent[];
  
  public orderContents?: OrderContent[];
  
  public stocks?: Stock[];

  public constructor(item?: Item) {
    super(item);
  }
}
