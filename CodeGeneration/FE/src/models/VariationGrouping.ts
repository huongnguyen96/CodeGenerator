import {Model} from 'core';

import {Product} from 'models/Product';
import {Variation} from 'models/Variation';

export class VariationGrouping extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public productId?: number;

  public product?: Product;
  
  public variations?: Variation[];

  public constructor(variationGrouping?: VariationGrouping) {
    super(variationGrouping);
  }
}
