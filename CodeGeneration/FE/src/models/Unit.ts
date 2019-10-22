import {Model} from 'core';

import {Variation} from 'models/Variation';
import {DiscountItem} from 'models/DiscountItem';
import {Stock} from 'models/Stock';

export class Unit extends Model {
   
  public id?: number;
 
  public firstVariationId?: number;
 
  public secondVariationId?: number;
 
  public thirdVariationId?: number;
 
  public sKU?: string;
 
  public price?: number;

  public firstVariation?: Variation;

  public secondVariation?: Variation;

  public thirdVariation?: Variation;
  
  public discountItems?: DiscountItem[];
  
  public stocks?: Stock[];

  public constructor(unit?: Unit) {
    super(unit);
  }
}
